using Project.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Models.Common
{
    public class AddressModel : BaseEntityModel
    {
        public AddressModel()
        {
            AvailableStates = new List<SelectListItem>();
        }
        public int? StateProvinceId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public IList<SelectListItem> AvailableStates { get; set; }
    }
}
