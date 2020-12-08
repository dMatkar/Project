using Project.Core.Configuration;
using Project.Core.Domain.Configuration;
using System.Collections.Generic;

namespace Project.Service.Configuration
{
    public interface ISettingService
    {
        IEnumerable<Setting> GetAllSettings();
        void InsertSetting(Setting setting, bool clearCache = true);
        void UpdateSetting(Setting setting, bool clearCache = true);
        void DeleteSetting(Setting setting);
        Setting GetSettingById(int Id, int storeId = 0);
        T GetSettingByKey<T>(string key, T defaultValue = default, int storeId = 0, bool loadSharedValueIfNotFound = false);
        T LoadSettings<T>(int storeId = 0) where T : ISettings, new();
        void SetSetting<T>(string name, T value, int stored = 0,bool clearCache = true);
        void SaveSettings<T>(T settings, int storeId = 0) where T : ISettings, new();
    }
}
