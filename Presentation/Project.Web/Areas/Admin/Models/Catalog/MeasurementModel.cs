using Project.Web.Framework.Models;
using System;

namespace Project.Web.Areas.Admin.Models.Catalog
{
    public class MeasurementModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string AdminComment  { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
