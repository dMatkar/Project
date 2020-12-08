using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Models.Catalog
{
    public class ProductListModel
    {
        public ProductListModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }
        public string SearchProductName { get; set; }
        public int SearchCategoryId { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}
