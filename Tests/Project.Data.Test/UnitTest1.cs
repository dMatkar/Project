using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Domain.Customers;

namespace Project.Data.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           using(ProjectDataContext pdc=new ProjectDataContext())
            {
                var customerTable =  pdc.Set<Customer>();
                customerTable.Add(new Customer() { Name = "dinesh", City = "pune", State = "maharashtra", Email = "matkardinu@gmail.com" });
                pdc.SaveChanges();
                Assert.IsTrue(customerTable != null);
                Assert.IsTrue(customerTable.Count() == 1);
            }
        }
    }
}
