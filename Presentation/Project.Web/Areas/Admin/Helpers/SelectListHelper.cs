using Project.Service.Catalog;
using Project.Service.Directory;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Helpers
{
    public static class SelectListHelper
    {
        public static IList<SelectListItem> GetMeasurements(IMeasurementService measurementService, int selectedId = 0, bool showHidden = false)
        {
            if (measurementService is null)
                return default;

            return measurementService.GetMesurementsList(showHidden).Select(m =>
               {
                   var selectListItem = new SelectListItem() { Text = m.Name, Value = m.Id.ToString() };
                   if (m.Id == selectedId)
                       selectListItem.Selected = true;
                   return selectListItem;
               }).ToList();
        }

        public static IList<SelectListItem> GetCategories(ICategoryService categoryService, IList<int> selectedIds, bool showHidden = false)
        {
            if (categoryService is null)
                return default;

            return categoryService.GetCategories(showHidden).Select(c =>
             {
                 var selectListItem = new SelectListItem() { Text = c.Name, Value = c.Id.ToString() };
                 if (selectedIds != null && selectedIds.Any())
                     selectListItem.Selected = (bool)selectedIds?.Contains(c.Id);
                 return selectListItem;
             }).ToList();
        }

        public static IList<SelectListItem> GetAvailableStates(IStateService stateService,int? selectedState)
        {
            if (stateService == null)
                return default;

           return stateService.GetAllStates().Select(s =>
            {
                var item = new SelectListItem() { Text = s.Name, Value = s.Id.ToString() };
                if (selectedState.HasValue)
                    item.Selected = selectedState.Value == s.Id;
                return item;
            }).ToList();
        }
    }
}
