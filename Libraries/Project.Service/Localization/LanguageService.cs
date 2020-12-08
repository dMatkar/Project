using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Localization
{
    public partial class LanguageService : ILanguageService
    {
        #region Constants 

        private const string LANGUAGES_ALL_KEY = "project.language.all.{0}";
        private const string LANGUAGES_BY_ID_KEY = "project.language.Id.{0}";
        private const string LANGUAGE_PATTERN_KEY = "project.language";

        #endregion

        #region Fields

        private readonly IRepository<Language> _languageRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public LanguageService(IRepository<Language> languageRepository,ICacheManager cacheManager)
        {
            _languageRepository = languageRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteLanguage(Language language)
        {
            if (language == null)
                return;

            _languageRepository.Delete(language);

            _cacheManager.RemoveByPattern(LANGUAGE_PATTERN_KEY);
        }

        public IList<Language> GetAllLanguages(bool showHidden = false)
        {
            string Key = string.Format(LANGUAGES_ALL_KEY, showHidden);
            return _cacheManager.Get<IList<Language>>(Key, () =>
             {
                 var query = _languageRepository.Table;
                 if (!showHidden)
                     query = query.Where(l => l.Published);

                 query = query.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
                 return query.ToList();
             });
        }

        public Language GetLanguageById(int languageId)
        {
            if (languageId == 0)
                return null;

            string Key = string.Format(LANGUAGES_BY_ID_KEY, languageId);
            return _cacheManager.Get(Key, () =>
             {
                 var query = _languageRepository.Table;
                 return query.Where(l => l.Id == languageId).FirstOrDefault();
             });
        }

        public void InsertLanguage(Language language)
        {
            if (language == null)
                return;

            _languageRepository.Insert(language);

            _cacheManager.RemoveByPattern(LANGUAGE_PATTERN_KEY);
        }

        public void UpdateLanguage(Language language)
        {
            if (language == null)
                return;

            _languageRepository.Update(language);

            _cacheManager.RemoveByPattern(LANGUAGE_PATTERN_KEY);
        }

        #endregion
    }
}
