using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Models.Catalog
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }
        public int SearchCategoryId { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}
