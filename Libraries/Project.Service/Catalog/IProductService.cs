using Project.Core;
using Project.Core.Domain.Catalog;
using System.Collections.Generic;

namespace Project.Service.Catalog
{
    public interface IProductService
    {
        IPagedList<Product> GetAllProducts(
            int categoryId = 0, string productName = "",
            int pageIndex = 0, int pageSize = int.MaxValue);
        IDictionary<int, string> SearchProductByName(string name);
        bool IsProductExists(int Id);
        Product GetProductById(int Id);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
