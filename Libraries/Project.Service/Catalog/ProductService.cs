using Project.Core;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Catalog
{
    public class ProductService : IProductService
    {
        #region Constants

        private const string SEARCH_PRODUCT_BY_NAME = "project.products.product";
        private const string PRODUCT_REMOVE_PATTERN = "project.products";

        #endregion

        #region Fields
        private readonly IRepository<Product> _productRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public ProductService(IRepository<Product> productRepository, ICacheManager cacheManager)
        {
            _productRepository = productRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteProduct(Product product)
        {
            if (product is null)
                throw new ArgumentException("product");

            product.Deleted = true;
            _productRepository.Update(product);
            _cacheManager.RemoveByPattern(PRODUCT_REMOVE_PATTERN);
        }
        public IPagedList<Product> GetAllProducts(int categoryId = 0,
            string productName = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _productRepository.Table;
            //if (categoryId != 0)
            //    query = query.Where(product => product.CategoryId == categoryId);
            if (!string.IsNullOrWhiteSpace(productName))
                query = query.Where(product => product.Name.Contains(productName));

            query = query.Where(product => !product.Deleted);
            query = query.OrderByDescending(product => product.CreatedOnUtc);
            return new PagedList<Product>(list: query, pageIndex: pageIndex, pageSize: pageSize);
        }
        public Product GetProductById(int Id)
        {
            if (Id == 0)
                return default;

            return _productRepository.GetById(Id);
        }
        public void InsertProduct(Product product)
        {
            if (product is null)
                return;

            _productRepository.Insert(product);
            _cacheManager.RemoveByPattern(PRODUCT_REMOVE_PATTERN);
        }
        public void UpdateProduct(Product product)
        {
            if (product is null)
                return;

            _productRepository.Update(product);
            _cacheManager.RemoveByPattern(PRODUCT_REMOVE_PATTERN);
        }
        public bool IsProductExists(int Id)
        {
            var query = _productRepository.AsNoTracking;
            query = query.Where(product => product.Id == Id && !product.Deleted);
            return query.Count() > 0;
        }
        public IDictionary<int, string> SearchProductByName(string name)
        {
            var list = _cacheManager.Get(SEARCH_PRODUCT_BY_NAME, () =>
               {
                   var query = from item in _productRepository.AsNoTracking
                               where !item.Deleted && item.Published
                               select new
                               {
                                   item.Id,
                                   item.Name
                               };
                   return query.ToDictionary(k => k.Id, v => v.Name);
               });
            return list.Where(c => c.Value.IndexOf(name,StringComparison.InvariantCultureIgnoreCase) > -1).ToDictionary(c => c.Key, v => v.Value);
        }

        #endregion
    }
}
