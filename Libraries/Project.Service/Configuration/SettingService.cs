using Project.Core;
using Project.Core.Caching;
using Project.Core.Configuration;
using Project.Core.Data;
using Project.Core.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Project.Service.Configuration
{
    public partial class SettingService : ISettingService
    {
        #region Constant

        private const string SETTINGS_ALL_KEY = "project.settings.all";
        private const string REMOVE_SETTINGS_PATTERN = "project.settings";

        #endregion

        #region Fields

        private readonly IRepository<Setting> _settingRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public SettingService(IRepository<Setting> settingRepository, ICacheManager cacheManager)
        {
            _settingRepository = settingRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        private IDictionary<string, IList<CachedSettings>> GetAllSetingsCached()
        {
            string key = string.Format(SETTINGS_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = from setting in _settingRepository.AsNoTracking
                            orderby setting.Name, setting.Id
                            select setting;

                IDictionary<string, IList<CachedSettings>> allSettings = new Dictionary<string, IList<CachedSettings>>();
                foreach (var setting in query)
                {
                    CachedSettings cachedSettings = new CachedSettings()
                    {
                        Id = setting.Id,
                        StoreId = setting.StoreId,
                        Name = setting.Name.ToLowerInvariant(),
                        Value = setting.Value
                    };
                    if (!allSettings.ContainsKey(cachedSettings.Name))
                    {
                        allSettings.Add(cachedSettings.Name, new List<CachedSettings>()
                        {
                            cachedSettings
                        });
                    }
                    else
                    {
                        allSettings[cachedSettings.Name].Add(cachedSettings);
                    }
                }
                return allSettings;
            });
        }

        #endregion

        #region Methods

        public IEnumerable<Setting> GetAllSettings()
        {
            var query = from setting in _settingRepository.Table
                        orderby setting.Name, setting.Id
                        select setting;
            return query.ToList();
        }

        public virtual void SaveSettings<T>(T settings, int storeId = 0) where T : ISettings, new()
        {
            foreach (var property in typeof(T).GetProperties())
            {
                if (!property.CanRead && !property.CanWrite)
                    continue;
                if (!TypeDescriptor.GetConverter(property.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = $"{typeof(T).Name}.{property.Name}";
                dynamic value = property.GetValue(settings, null);
                if (value != null)
                    SetSetting(key, value, storeId, false);
                else
                    SetSetting(key, string.Empty, storeId, false);
            }
            ClearCache();
        }

        public virtual void SetSetting<T>(string name, T value, int stored = 0, bool clearCache = true)
        {
            if (name is null)
                throw new ArgumentNullException("key");

            string valueStr = TypeDescriptor.GetConverter(typeof(T)).ConvertToInvariantString(value);

            if (valueStr is null)
                throw new ArgumentNullException("value");

            string key = name.ToLowerInvariant();
            var allSettings = GetAllSetingsCached();
            var setting = allSettings.ContainsKey(key) ? allSettings[key].FirstOrDefault() : null;
            if (setting != null)
            {
                Setting settingForUpdate = GetSettingById(setting.Id);
                settingForUpdate.Value = valueStr;
                UpdateSetting(settingForUpdate, false);
            }
            else
            {
                Setting settingforInsert = new Setting() { Name = key, Value = valueStr, StoreId = stored };
                InsertSetting(settingforInsert, false);
            }
            ClearCache();
        }

        public virtual T GetSettingByKey<T>(string key, T defaultValue = default, int storeId = 0, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrWhiteSpace(key))
                return defaultValue;

            var cachedSettings = GetAllSetingsCached();
            string settingsKey = key.Trim().ToLower();
            if (cachedSettings.ContainsKey(settingsKey))
            {
                var settings = cachedSettings[settingsKey].Where(s => s.StoreId == storeId).FirstOrDefault();
                if (settings != null && storeId > 0 && loadSharedValueIfNotFound)
                    settings = cachedSettings[settingsKey].Where(s => s.StoreId == 0).FirstOrDefault();

                return CommonHelper.To<T>(settings.Value);
            }
            return defaultValue;
        }

        public virtual T LoadSettings<T>(int storeId = 0) where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (!property.CanRead && !property.CanWrite)
                    continue;

                string key = $"{typeof(T).Name}.{property.Name}";
                var settingValue = GetSettingByKey<string>(key, storeId: storeId, loadSharedValueIfNotFound: true);
                var destinationTypeConverter = TypeDescriptor.GetConverter(property.PropertyType);

                if (!destinationTypeConverter.CanConvertFrom(typeof(string)))
                    continue;
                if (!destinationTypeConverter.IsValid(settingValue))
                    continue;

                property.SetValue(settings, destinationTypeConverter.ConvertFromInvariantString(settingValue), null);
            }
            return settings;
        }

        public virtual void ClearCache()
        {
            _cacheManager.RemoveByPattern(REMOVE_SETTINGS_PATTERN);
        }

        public void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting is null)
                throw new ArgumentNullException("settings");

            _settingRepository.Insert(setting);

            if (clearCache)
                _cacheManager.RemoveByPattern(REMOVE_SETTINGS_PATTERN);
        }

        public void UpdateSetting(Setting setting, bool clearCache = true)
        {
            if (setting is null)
                throw new ArgumentNullException("settings");

            _settingRepository.Update(setting);

            if (clearCache)
                _cacheManager.RemoveByPattern(REMOVE_SETTINGS_PATTERN);
        }

        public Setting GetSettingById(int Id, int storeId = 0)
        {
            if (Id == 0)
                return null;

            return _settingRepository.GetById(Id);
        }

        public void DeleteSetting(Setting setting)
        {
            if (setting is null)
                throw new ArgumentNullException("settings");

            _settingRepository.Delete(setting);

            _cacheManager.RemoveByPattern(REMOVE_SETTINGS_PATTERN);
        }

        #endregion

        #region Nested Classes

        class CachedSettings
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int StoreId { get; set; }
        }

        #endregion
    }
}
