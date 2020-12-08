using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Localization
{
    public partial class LocalizationService : ILocalizationService
    {
        #region Constants

        private const string LCR_ALL = "project.lcr-{0}";
        private const string LCR_PATTERN = "project.lcr";

        #endregion

        #region Fields

        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region  Constructor

        public LocalizationService(IRepository<LocaleStringResource> lsrRepository, ICacheManager cacheManager)
        {
            _lsrRepository = lsrRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        protected Dictionary<string, KeyValuePair<int, string>> GetAllResourcesValues(int langaugeId)
        {
            if (langaugeId == 0)
                return null;

            string Key = string.Format(LCR_ALL, langaugeId);
            return _cacheManager.Get(Key, () =>
             {
                 var query = from lcr in _lsrRepository.AsNoTracking
                             where lcr.LanguageId == langaugeId
                             orderby lcr.ResourceName
                             select lcr;

                 return query.ToDictionary(key => key.ResourceName.ToLowerInvariant(), value => new KeyValuePair<int, string>(value.Id, value.ResourceValue));
             });
        }

        #endregion

        #region Methods

        public virtual void DeleteLocalStringResources(LocaleStringResource localeStringResource)
        {
            if (localeStringResource is null)
                throw new ArgumentNullException("localStringResources");

            _lsrRepository.Delete(localeStringResource);

            _cacheManager.RemoveByPattern(LCR_PATTERN);
        }

        public virtual LocaleStringResource GetLocaleStringResource(int Id)
        {
            if (Id == 0)
                return null;

            return _lsrRepository.GetById(Id);
        }

        public virtual void InsertLocalStringResources(LocaleStringResource localeStringResource)
        {
            if (localeStringResource is null)
                throw new ArgumentNullException("localStringResources");

            _lsrRepository.Insert(localeStringResource);

            _cacheManager.RemoveByPattern(LCR_PATTERN);
        }

        public virtual void UpdateLocalStringResources(LocaleStringResource localeStringResource)
        {
            if (localeStringResource is null)
                throw new ArgumentNullException("localStringResourcs");

            _lsrRepository.Update(localeStringResource);

            _cacheManager.RemoveByPattern(LCR_PATTERN);
        }

        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourcesName, int languageId, bool logIfNotFound = true)
        {
            if (string.IsNullOrEmpty(resourcesName) || languageId == 0)
                return null;

            var data = (from lcr in _lsrRepository.Table
                        orderby lcr
                        where lcr.LanguageId == languageId && lcr.ResourceName.Equals(resourcesName)
                        select lcr).FirstOrDefault();
            return data;
        }

        public string GetLocaleStringResource(string resourcesKey, int languageId, bool logIfNotFound = true, string defaultValue = "")
        {
            string result = defaultValue;
            if (string.IsNullOrEmpty(resourcesKey) || languageId == 0)
                return result;

            resourcesKey = resourcesKey.Trim().ToLowerInvariant();
            var dictionary = GetAllResourcesValues(languageId);
            if (dictionary.ContainsKey(resourcesKey))
            {
                result = dictionary[resourcesKey].Value;
            }
            return result;
        }

        public IEnumerable<LocaleStringResource> GetLocaleStringResources(int languageId)
        {
            if (languageId == 0)
                return null;

            var query = from lcr in _lsrRepository.Table
                        where lcr.LanguageId == languageId
                        orderby lcr.ResourceValue
                        select lcr;

            return query.ToList();
        }

        #endregion
    }
}