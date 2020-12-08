using Project.Core.Domain.Localization;
using System.Collections.Generic;

namespace Project.Service.Localization
{
    public partial interface ILanguageService
    {
        /// <summary>
        /// Get language id.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Language GetLanguageById(int languageId);

        /// <summary>
        /// Get all languages.
        /// </summary>
        /// <param name="showHidden">Display hidden language.</param>
        /// <returns></returns>
        IList<Language> GetAllLanguages(bool showHidden = false);

        /// <summary>
        /// Insert language.
        /// </summary>
        /// <param name="language"></param>
        void InsertLanguage(Language language);

        /// <summary>
        /// Update language.
        /// </summary>
        /// <param name="language"></param>
        void UpdateLanguage(Language language);

        /// <summary>
        /// Delete language.
        /// </summary>
        /// <param name="language"></param>
        void DeleteLanguage(Language language);
    }
}
