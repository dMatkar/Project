using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using Project.Core.Domain.Customers;
using Project.Core.Domain.Localization;
using Project.Core.Domain.Orders;
using Project.Core.Infrastructure;
using Project.Data;
using Project.Service.Catalog;
using Project.Service.Customers;
using Project.Service.Localization;
using Project.Service.Orders;
using Project.Web.Areas.Admin.Controllers;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Models.Orders;
using Project.Web.Controllers;
using Project.Web.Framework.kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.We.Admin.Tests
{
    [TestClass]
    public class OrderControllerTest 
    {
        public static OrderController GetInstance()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<ShoppingCart> shoppingRepo = new EfRepository<ShoppingCart>(dbContext);
            IShoppingCartService shoppingCartService = new ShoppingCartService(shoppingRepo, new MemoryCacheManager());

            IRepository<LocaleStringResource> localizaitonRepo = new EfRepository<LocaleStringResource>(dbContext);
            ILocalizationService localizationService = new LocalizationService(localizaitonRepo, new MemoryCacheManager());

            IRepository<Customer> customerRepository = new EfRepository<Customer>(dbContext);
            ICustomerService customerService = new CustomerService(customerRepository, new MemoryCacheManager());

            IRepository<Product> productRepository = new EfRepository<Product>(dbContext);
            IProductService productService = new ProductService(productRepository, new MemoryCacheManager());

            OrderController orderController = new OrderController(productService, customerService, shoppingCartService, localizationService);
            return orderController;
        }

        [TestMethod]
        public void ShouldAddData()
        {
            EngineContext.Current.Initialize();

            Measurement measurement = new Measurement() { ShortName = "KG" };
            Product product = new Product() { Name = "product 1", Price = 500,Measurement=measurement };
            ShoppingCart shoppingCart = new ShoppingCart() { Product = product,Quantity=5 };
            ShoppingCartModel   shoppingCartModel = shoppingCart.ToModel();
            Assert.IsTrue(shoppingCart != null);
        }
    }
}
