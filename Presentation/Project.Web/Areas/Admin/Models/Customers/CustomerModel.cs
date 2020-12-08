using Project.Web.Areas.Admin.Models.Common;
using Project.Web.Framework;
using Project.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Models.Customers
{
    public class CustomerModel : BaseEntityModel
    {
        public CustomerModel()
        {
            Address = new AddressModel();
        }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.FirstName")]
        public string FirstName { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.LastName")]
        public string LastName { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.FullName")]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.MobileNumber")]
        public string MobileNumber { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.Email")]
        public string Email { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.Active")]
        public bool Active { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.Deleted")]
        public bool Deleted { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.CreatedOn")]
        public DateTime CreatedOnUtc { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.Fields.UpdatedOnUtc")]
        public DateTime UpdatedOnUtc { get; set; }

        public AddressModel Address { get; set; }
    }
}
