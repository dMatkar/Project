using Project.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Models.Customers
{
    public class CustomerList
    {
        [ProjectResourcesDisplayName("Admin.Customers.Customers.List.SearchFirstName")]
        public string SearchFirstName  { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.List.SearchLastName")]
        public string SearchLastName  { get; set; }
        
        [ProjectResourcesDisplayName("Admin.Customers.Customers.List.SearchEmail")]
        public string SearchEmail { get; set; }

        [ProjectResourcesDisplayName("Admin.Customers.Customers.List.SearchMobileNumber")] 
        public string SearchMobileNumber  { get; set; }
    }
}
