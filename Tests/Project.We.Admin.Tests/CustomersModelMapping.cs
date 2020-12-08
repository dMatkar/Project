using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Domain.Customers;
using Project.Core.Infrastructure;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.We.Admin.Tests
{
    [TestClass]
    public class CustomersModelMapping
    {
        [TestMethod]
        public void ShouldMapToCustomer()
        {
            CustomerModel customerModel = new CustomerModel()
            {
                Id=1,
                Address=new Web.Areas.Admin.Models.Common.AddressModel() { Address1="Address line 1",Address2="Address line 2",CreatedOnUtc=DateTime.Now}
            };

            EngineContext.Current.Initialize();
            Customer customer = customerModel.ToEntity();
            Assert.IsTrue(customer != null);
        }
    }
}
