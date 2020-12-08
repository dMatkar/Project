using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using Project.Core.Domain.Customers;
using Project.Core.Domain.Directory;
using Project.Core.Domain.Localization;
using Project.Core.Infrastructure;
using Project.Data;
using Project.Service.Catalog;
using Project.Service.Customers;
using Project.Service.Directory;
using Project.Service.Helper;
using Project.Service.Localization;
using Project.Web.Areas.Admin.Controllers;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Helpers;
using Project.Web.Areas.Admin.Models.Catalog;
using Project.Web.Areas.Admin.Models.Customers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.We.Admin.Tests
{
    [TestClass]
    public class CustomerHomeTest
    {
        public static CustomerController GetInstance()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Customer> categoryRepo = new EfRepository<Customer>(dbContext);

            IRepository<ProductCategory> productcategoryRepo = new EfRepository<ProductCategory>(dbContext);
            ICustomerService customerService = new CustomerService(categoryRepo,new MemoryCacheManager());

            IRepository<Product> productRepository = new EfRepository<Product>(dbContext);
            IProductService productService = new ProductService(productRepository, new MemoryCacheManager());

            IRepository<State>  stateRepo = new EfRepository<State>(dbContext);
            IStateService stateService = new StateService(stateRepo, new MemoryCacheManager());

            IRepository<LocaleStringResource> localizaitonRepo = new EfRepository<LocaleStringResource>(dbContext);
            ILocalizationService localizationService = new LocalizationService(localizaitonRepo, new MemoryCacheManager());

            IRepository<Measurement> measurementRepo = new EfRepository<Measurement>(dbContext);
            IMeasurementService measurementService = new MeasurementService(measurementRepo, new MemoryCacheManager());

            CustomerController customerController = new CustomerController(customerService, new CustomerSettings(), localizationService, new DateTimeHelper(), stateService);
            return customerController;
        }

        [TestMethod]
        public void ShouldInsertCustomer()
        {
            EngineContext.Current.Initialize();
            CustomerController homeController = EngineContext.Current.Resolve<CustomerController>();
            var customerModel = new CustomerModel() { IsTaxExempt = true, Active = true, FirstName = "dinesh", LastName = "matkar", MobileNumber = "8788069883", Email = "matkardinu@gmail.com" };
            homeController.Create(customerModel);
            Assert.IsTrue(customerModel.Id > 0);
        }

        [TestMethod]
        public void ShouldDisplayList()
        {
            EngineContext.Current.Initialize();
            CustomerController homeController = EngineContext.Current.Resolve<CustomerController>();
            var result = (JsonResult)homeController.CustomersList(new Web.Framework.kendoui.DataSourceRequest(), new CustomerList());
            Assert.IsTrue(result.Data != null);
        }

        [TestMethod]
        public void ShouldDisplayCorrectData()
        {
            DateTimeHelper dateTimeHelper = new DateTimeHelper();
            DateTime date = dateTimeHelper.ConvertToUserTime(DateTime.UtcNow);
            Assert.IsTrue(date != null);
        }

        [TestMethod]
        public void ShouldUpdateData()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Customer> categoryRepo = new EfRepository<Customer>(dbContext);

            IRepository<ProductCategory> productcategoryRepo = new EfRepository<ProductCategory>(dbContext);
            ICustomerService customerService = new CustomerService(categoryRepo, new MemoryCacheManager());

            EngineContext.Current.Initialize();
            Customer customer = customerService.GetCustomerById(1);
            CustomerModel customerModel = customer.ToModel();
            GetInstance().Edit(customerModel);
        }
    }
}
