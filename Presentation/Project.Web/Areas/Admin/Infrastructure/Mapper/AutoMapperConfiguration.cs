using AutoMapper;
using Project.Core.Domain.Catalog;
using Project.Core.Domain.Common;
using Project.Core.Domain.Customers;
using Project.Core.Domain.Orders;
using Project.Core.Infrastructure.Mapper;
using Project.Web.Areas.Admin.Models.Catalog;
using Project.Web.Areas.Admin.Models.Common;
using Project.Web.Areas.Admin.Models.Customers;
using Project.Web.Areas.Admin.Models.Orders;
using System;
using System.Linq;

namespace Project.Web.Areas.Admin.Infrastructure.Mapper
{
    public class AutoMapperConfiguration : IMapperConfiguration
    {
        public int Order => 0;

        public void Register(IMapperConfigurationExpression configurationExpression)
        {
            #region Customers

            configurationExpression.CreateMap<CustomerModel, Customer>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore());

            configurationExpression.CreateMap<Customer, CustomerModel>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.FullName, mo => mo.Ignore());

            #endregion

            #region Categories

            configurationExpression.CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore());

            configurationExpression.CreateMap<CategoryModel, Category>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore());

            #endregion

            #region Products

            configurationExpression.CreateMap<Product, ProductModel>()
                .ForMember(product => product.AvailableCategories, mo => mo.Ignore())
                .ForMember(product => product.CategoryIds, mo => mo.Ignore())
                .ForMember(product => product.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(product => product.UpdatedOnUtc, mo => mo.Ignore());

            configurationExpression.CreateMap<ProductModel, Product>()
                .ForMember(product => product.LowStockActivity, mo => mo.Ignore())
                .ForMember(product => product.ManageInventoryMethod, mo => mo.Ignore())
                .ForMember(product => product.Categories, mo => mo.Ignore())
                .ForMember(product => product.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(product => product.UpdatedOnUtc, mo => mo.Ignore());

            #endregion

            #region Measurements

            configurationExpression.CreateMap<Measurement, MeasurementModel>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore());

            configurationExpression.CreateMap<MeasurementModel, Measurement>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore());

            #endregion

            #region Addresses

            configurationExpression.CreateMap<AddressModel, Address>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore());

            configurationExpression.CreateMap<Address, AddressModel>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.AvailableStates, mo => mo.Ignore());

            #endregion

            #region Shopping Cart

            configurationExpression.CreateMap<ShoppingCart, ShoppingCartModel>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(p => p.Product.Name))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(p => p.Quantity))
                .ForMember(dest => dest.Rate, src => src.MapFrom(p => p.Product.Price))
                .ForMember(dest => dest.Measurement, src => src.MapFrom(p => p.Product.Measurement.ShortName));

            #endregion
        }
    }
}
