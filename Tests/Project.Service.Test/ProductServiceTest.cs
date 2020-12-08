using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using Project.Data;
using Project.Service.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        public static IProductService GetProductInstance()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Product> measurementService = new EfRepository<Product>(dbContext);
            IProductService measurementService1 = new ProductService(measurementService,new MemoryCacheManager());
            return measurementService1;
        }

        public static ICategoryService GetCategoryInstance()
        {
            //IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            //IRepository<Category> measurementService = new EfRepository<Category>(dbContext);
            //IRepository<ProductCategory> measurementService1  = new EfRepository<ProductCategory>(dbContext);
            //IRepository<Product> product  = new EfRepository<Product>(dbContext);
            //ICategoryService measurementService2 = new CategoryService(measurementService,new MemoryCacheManager(),measurementService1, product);
            //return measurementService2;
            return null;
        }

        public static IMeasurementService GetMeasurementInstance()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Measurement> measurementService = new EfRepository<Measurement>(dbContext);
            IMeasurementService measurementService1 = new  MeasurementService(measurementService, new MemoryCacheManager());
            return measurementService1;
        }

        [TestMethod]
        public void ShouldInsertData()
        {
            Product product = new Product()
            {
                Name="T-Shirt",
                ShortDescription="Good quality",
                StockQuantity=100,
                LowStockActivityId=2,
                AdminComment="none",
                CreatedOnUtc=DateTime.UtcNow,
                Deleted=false,
                DisplayOrder=1,
                InventoryMethodId=2,
                IsTaxExempt=false,
                NotifyAdminForQuantityBelow=10,
                Price=100,
               Published=true,
               OldPrice=80,
               ProductCost=70,
               UpdatedOnUtc=DateTime.UtcNow,
               MeasurementId=1
            };
            GetProductInstance().InsertProduct(product);
        }

        [TestMethod]
        public void ShouldInsertCategory()
        {
            Category category = new Category()
            {
                Deleted = false,
                DisplayOrder = 1,
                ShortDescription = "none",
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                Name = "Mens"
            };

            GetCategoryInstance().Insert(category);
        }

        [TestMethod]
        public void ShouldDisplayCorrectSearch()
        {
            var list = GetProductInstance().SearchProductByName("mona");
            Assert.IsTrue(list.Count == 1);
        }
    }
}
