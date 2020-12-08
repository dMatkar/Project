using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using Project.Core.Domain.Localization;
using Project.Data;
using Project.Service.Catalog;
using Project.Service.Helper;
using Project.Service.Localization;
using Project.Web.Areas.Admin.Controllers;
using Project.Web.Areas.Admin.Models.Catalog;
using System;
using System.Web.Mvc;

namespace Project.We.Admin.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        public static ProductController GetInstance()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Category> categoryRepo = new EfRepository<Category>(dbContext);

            IRepository<ProductCategory> productcategoryRepo = new EfRepository<ProductCategory>(dbContext);
            ICategoryService categoryService = new CategoryService(categoryRepo, new MemoryCacheManager(), productcategoryRepo);

            IRepository<Product> productRepository = new EfRepository<Product>(dbContext);
            IProductService productService = new ProductService(productRepository,new MemoryCacheManager());

            IRepository<LocaleStringResource> localizaitonRepo = new EfRepository<LocaleStringResource>(dbContext);
            ILocalizationService localizationService = new LocalizationService(localizaitonRepo, new MemoryCacheManager());

            IRepository<Measurement> measurementRepo = new EfRepository<Measurement>(dbContext);
            IMeasurementService measurementService = new MeasurementService(measurementRepo, new MemoryCacheManager());

            ProductController productController = new ProductController(new DateTimeHelper(), productService, localizationService, categoryService, measurementService);
            return productController;
        }

        [TestMethod]
        public void ShouldsaveProductCategoryMapping()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Category> categoryRepo = new EfRepository<Category>(dbContext);
            IRepository<Product> productRepository = new EfRepository<Product>(dbContext);
            IProductService productService = new ProductService(productRepository,new MemoryCacheManager());

            var data = productService.GetProductById(2);

            ProductModel productModel = new ProductModel();
            productModel.CategoryIds.Add(1);

            GetInstance().SaveProductCategoryMapping(productModel, data);
        }

        [TestMethod]
        public void ShouldGetData()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Category> categoryRepo = new EfRepository<Category>(dbContext);
            IRepository<Product> productRepository = new EfRepository<Product>(dbContext);
            IProductService productService = new ProductService(productRepository, new MemoryCacheManager());

            var data = productService.GetProductById(2);
            Assert.IsTrue(data != null);
        }

        [TestMethod]
        public void ShouldInsertProduct()
        {
            ProductModel product = new ProductModel()
            {
                Name = "T-Shirt",
                ShortDescription = "Good quality",
                StockQuantity = 100,
                LowStockActivityId = 2,
                AdminComment = "none",
                CreatedOnUtc = DateTime.UtcNow,
                Deleted = false,
                DisplayOrder = 1,
                InventoryMethodId = 2,
                IsTaxExempt = false,
                NotifyAdminForQuantityBelow = 10,
                Price = 100,
                Published = true,
                OldPrice = 80,
                ProductCost = 70,
                UpdatedOnUtc = DateTime.UtcNow,
                MeasurementId = 1
            };
            product.CategoryIds.Add(1);
            product.CategoryIds.Add(2);
            GetInstance().Create(product);
        }

        [TestMethod]
        public void ShouldCallEditAction()
        {
            ProductModel productModel = new ProductModel() { Id = 1 };
            GetInstance().PrepapreProductModel(productModel);
            Assert.IsTrue(productModel.CategoryIds.Count > 0);
        }
    }
}
