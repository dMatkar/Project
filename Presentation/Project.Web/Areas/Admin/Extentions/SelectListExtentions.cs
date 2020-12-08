using Project.Core.Infrastructure;
using Project.Service.Localization;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Extentions
{
    public static class SelectListExtentions
    {
        public static IList<SelectListItem> ToLocalizedSelectList(this IList<SelectListItem> selectListItems)
        {
            if (selectListItems is null)
                return default;

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            foreach (var item in selectListItems)
            {
                item.Text = localizationService.GetLocaleStringResource(resourcesKey: item.Text, languageId: 1, defaultValue: item.Text);
            }
            return selectListItems;
        }
    }
}
