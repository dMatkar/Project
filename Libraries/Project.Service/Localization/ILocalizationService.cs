using Project.Core.Domain.Localization;
using System.Collections.Generic;

namespace Project.Service.Localization
{
    public partial interface ILocalizationService
    {
        void DeleteLocalStringResources(LocaleStringResource localeStringResource);
        void InsertLocalStringResources(LocaleStringResource localeStringResource);
        void UpdateLocalStringResources(LocaleStringResource localeStringResource);
        LocaleStringResource GetLocaleStringResource(int Id);
        IEnumerable<LocaleStringResource> GetLocaleStringResources(int languageId);
        LocaleStringResource GetLocaleStringResourceByName(string resourcesName, int languageId, bool logIfNotFound = true);
        string GetLocaleStringResource(string resourcesKey, int languageId, bool logIfNotFound = true, string defaultValue = "");
    }
}
