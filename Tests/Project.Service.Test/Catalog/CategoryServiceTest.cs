using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using Project.Core.Infrastructure;
using Project.Data;
using Project.Service.Catalog;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Test.Catalog
{
    [TestClass]
    public class CategoryServiceTest
    {
        [TestMethod]
        public void ShouldInsertCategory()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Category> categoryRepo = new EfRepository<Category>(dbContext);
            IRepository<ProductCategory> productcategoryRepo = new EfRepository<ProductCategory>(dbContext);
            ICategoryService categoryService = new CategoryService(categoryRepo, new MemoryCacheManager(), productcategoryRepo);
            var existingProductCategoryMapping = categoryService.GetProductCategoriesByProductId(1,true);
            Assert.IsTrue(existingProductCategoryMapping.Count == 1);
        }

    }
}
