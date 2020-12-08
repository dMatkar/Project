using Project.Core;
using Project.Core.Domain.Catalog;
using System.Collections.Generic;

namespace Project.Service.Catalog
{
    public interface ICategoryService
    {
        #region Category

        IPagedList<Category> GetCategories(int categoryId, int pageIndex, int pageSize);
        IList<Category> GetCategories(bool showHidden = false);
        Category GetCategory(int Id);
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);

        #endregion

        #region ProductCategory

        void InsertProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false);
        #endregion
    }
}
