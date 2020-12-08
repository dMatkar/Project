using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Configuration;
using Project.Core.Domain.Customers;
using Project.Core.Infrastructure;
using Project.Service.Customers;
using Project.Web.Controllers;
using Project.Web.Framework.Menu;

namespace Project.Web.Framework.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            EngineContext.Initialize();

            var settings = EngineContext.Current.Resolve<CustomerSettings>();
            Assert.IsTrue(settings != null);
        }

        [TestMethod]
        public void ShouldDisplayLocalString()
        {
            ProjectResourcesDisplayNameAttribute projectResourcesDisplayNameAttribute = new ProjectResourcesDisplayNameAttribute("Admin.Customers.Customers.Fields.LastName");
            Assert.IsTrue(projectResourcesDisplayNameAttribute.DisplayName == "Last Name");

        }

        [TestMethod]
        public void GetMethod()
        {
             var data = typeof(Enumerable).GetMethod("ToArray");
            Assert.IsTrue(data != null);
        }
    }
}
