using Project.Core;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Catalog
{
    public partial class CategoryService : ICategoryService
    {
        #region Constants

        private const string CATEGORY_ALL_KEY = "project.categories.{0}";
        private const string CATEGORY_REMOVE_PATTERN = "project.categories";
        private const string PRODUCT_CATEGORY_REMOVE_PATTERN = "project.product.categories";

        #endregion

        #region Fields

        private readonly IRepository<Category> _categoryRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ProductCategory> _productCategoryRepository;

        #endregion

        #region Constructor

        public CategoryService(IRepository<Category> categoryRepository, ICacheManager cacheManager,
            IRepository<ProductCategory> productCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _cacheManager = cacheManager;
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion

        #region Categories

        public virtual IPagedList<Category> GetCategories(int categoryId, int pageIndex, int pageSize)
        {
            var query = _categoryRepository.Table;
            if (categoryId > 0)
                query = query.Where(c => c.Id == categoryId);

            query = query.Where(c => !c.Deleted);
            query = query.OrderByDescending(category => category.CreatedOnUtc);
            return new PagedList<Category>(list: query, pageIndex: pageIndex, pageSize: pageSize);
        }

        public IList<Category> GetCategories(bool showHidden = false)
        {
            string key = string.Format(CATEGORY_ALL_KEY, showHidden);
            var list = _cacheManager.Get(key, () =>
            {
                return (from c in _categoryRepository.Table
                        where !c.Deleted
                        select c).ToList();
            });
            return list;
        }

        public virtual Category GetCategory(int Id)
        {
            if (Id == 0)
                return null;

            return _categoryRepository.GetById(Id);
        }

        public virtual void Insert(Category category)
        {
            if (category is null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);
            _cacheManager.RemoveByPattern(CATEGORY_REMOVE_PATTERN);
            _cacheManager.RemoveByPattern(PRODUCT_CATEGORY_REMOVE_PATTERN);
        }

        public virtual void Update(Category category)
        {
            if (category is null)
                throw new ArgumentNullException("category");

            _categoryRepository.Update(category);
            _cacheManager.RemoveByPattern(CATEGORY_REMOVE_PATTERN);
            _cacheManager.RemoveByPattern(PRODUCT_CATEGORY_REMOVE_PATTERN);
        }

        public virtual void Delete(Category category)
        {
            if (category is null)
                throw new ArgumentNullException("category");

            category.Deleted = true;
            _categoryRepository.Delete(category);
            _cacheManager.RemoveByPattern(CATEGORY_REMOVE_PATTERN);
            _cacheManager.RemoveByPattern(PRODUCT_CATEGORY_REMOVE_PATTERN);
        }

        #endregion

        #region ProductCategories

        public virtual void InsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory is null)
                throw new ArgumentException("productCategory");

            _productCategoryRepository.Insert(productCategory);
            _cacheManager.RemoveByPattern(PRODUCT_CATEGORY_REMOVE_PATTERN);
        }

        public virtual void DeleteProductCategory(ProductCategory productCategory)
        {
            if (productCategory is null)
                throw new ArgumentException("productCategory");

            _productCategoryRepository.Delete(productCategory);
            _cacheManager.RemoveByPattern(PRODUCT_CATEGORY_REMOVE_PATTERN);
        }

        public virtual void UpdateProductCategory(ProductCategory productCategory)
        {
            if (productCategory is null)
                throw new ArgumentException("productCategory");

            _productCategoryRepository.Update(productCategory);
            _cacheManager.RemoveByPattern(PRODUCT_CATEGORY_REMOVE_PATTERN);
        }

        public virtual IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductCategory>();

            var query = from pc in _productCategoryRepository.Table
                        join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                        where pc.ProductId == productId && !c.Deleted
                        orderby pc.DisplayOrder, pc.Id
                        select pc;

            var data = query.ToList();
            return data;
        }

        #endregion

    }
}
