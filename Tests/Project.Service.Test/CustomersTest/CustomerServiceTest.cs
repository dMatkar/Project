using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Domain.Customers;
using Project.Data;
using Project.Service.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Test.CustomersTest
{
    [TestClass]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void ShouldInsertData()
        {
            var repository = new EfRepository<Customer>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            ICustomerService customerService = new CustomerService(repository,new MemoryCacheManager());
            var customer = new Customer()
            {
                Active = true,
                Deleted = false,
                FirstName = "rajesh",
                LastName = "matkar",
                CreatedOnUtc = DateTime.UtcNow,
                Email = "matkardinu@gmail.com",
                IsTaxExempt = false,
                MobileNumber = "8788069883"
            };
            customerService.InsertCustomer(customer);
            Assert.IsTrue(customer.Id > 0);
        }

        [TestMethod]
        public void ShouldGetCustomer()
        {
            var repository = new EfRepository<Customer>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            ICustomerService customerService = new CustomerService(repository,new MemoryCacheManager());
            var data = customerService.GetCustomerById(1);
            data.Address.Address1 = "address 11";
            customerService.UpdateCustomer(data);
        }

        [TestMethod]
        public void ShouldUpdateCustomer()
        {
            var repository = new EfRepository<Customer>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            ICustomerService customerService = new CustomerService(repository,new MemoryCacheManager());
           Customer customer = customerService.GetCustomerById(1);
            customer.Active = false;
            customerService.UpdateCustomer(customer);
            Assert.IsTrue(!customer.Active);
        }
    }
}
