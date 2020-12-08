using Project.Core.Domain.Catalog;
using Project.Core.Domain.Customers;
using Project.Core.Domain.Orders;
using Project.Core.Infrastructure.Mapper;
using Project.Web.Areas.Admin.Models.Catalog;
using Project.Web.Areas.Admin.Models.Customers;
using Project.Web.Areas.Admin.Models.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Project.Web.Areas.Admin.Extentions
{
    public static class MappingExtensions
    {
        public static TDestination Map<TSource, TDestination>(TSource source) => AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => AutoMapperConfiguration.Mapper.Map(source, destination);

        #region Customers

        public static Customer ToEntity(this CustomerModel model)
        {
            if (model is null)
                return default;
            return Map<CustomerModel, Customer>(model);
        }
        public static Customer ToEntity(this CustomerModel model, Customer customer)
        {
            if (customer is null)
                return default;
            return Map(model, customer);
        }
        public static CustomerModel ToModel(this Customer customer)
        {
            if (customer is null)
                return default;
            return Map<Customer, CustomerModel>(customer);
        }
   
        #endregion

        #region Category

        public static Category ToEntity(this CategoryModel model)
        {
            if (model is null)
                return default;
            return Map<CategoryModel, Category>(model);
        }
        public static Category ToEntity(this CategoryModel model,Category entity)
        {
            if (model is null)
                return default;
            return Map(model, entity);
        }
        public static CategoryModel ToModel(this Category entity)
        {
            if (entity is null)
                return default;
            return Map<Category, CategoryModel>(entity);
        }

        #endregion

        #region Products

        public static ProductModel ToModel(this Product entity)
        {
            if (entity is null)
                return default;

            return Map<Product, ProductModel>(entity);
        }
        public static Product ToEntity(this ProductModel model)
        {
            if (model is null)
                return default;

            return Map<ProductModel, Product>(model);
        }
        public static Product ToEntity(this ProductModel model,Product product)
        {
            if (model is null)
                return null;

            return Map(model, product);
        }

        #endregion

        #region Measurements

        public static Measurement ToEntity(this MeasurementModel model)
        {
            if (model is null)
                return default;

            return Map<MeasurementModel,Measurement>(model);
        }

        public static Measurement ToEntity(this MeasurementModel model,Measurement entity)
        {
            if (model is null)
                return default;

            return Map(model, entity);
        }

        public static MeasurementModel ToModel(this Measurement entity)
        {
            if (entity is null)
                return default;

            return Map<Measurement, MeasurementModel>(entity);
        }

        #endregion

        #region Shopping Cart

        public static ShoppingCartModel ToModel(this ShoppingCart entity)
        {
            return Map<ShoppingCart, ShoppingCartModel>(entity);
        }

        #endregion
    }
}
