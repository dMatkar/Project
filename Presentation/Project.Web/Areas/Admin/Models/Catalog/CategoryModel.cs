using Project.Web.Framework.Models;
using System;

namespace Project.Web.Areas.Admin.Models.Catalog 
{
    public class CategoryModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public int DisplayOrder { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
