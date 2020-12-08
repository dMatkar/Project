using Project.Core.Domain.Orders;
using Project.Service.Catalog;
using Project.Service.Customers;
using Project.Service.Localization;
using Project.Service.Orders;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Models.Orders;
using Project.Web.Framework.kendoui;
using Project.Web.Framework.Mvc.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructor

        public OrderController(IProductService productService, ICustomerService customerService, 
            IShoppingCartService shoppingCartService, ILocalizationService localizationService)
        {
            _productService = productService;
            _customerService = customerService;
            _shoppingCartService = shoppingCartService;
            _localizationService = localizationService;
        }

        #endregion

        #region Orders

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            OrderModel orderModel = new OrderModel();
            return View(orderModel);
        }

        #endregion

        #region Shopping cart

        [HttpPost]
        public virtual ActionResult CartList(DataSourceRequest dataSourceRequest, AddToCartModel model)
        {
            if (model.CustomerId == 0)
                return Json(new DataSourceResult() { Data = new List<ShoppingCartModel>(), Total = 0 });

            var list = _shoppingCartService.GetAllItems(customerId: model.CustomerId, pageIndex: dataSourceRequest.Page - 1, pageSize: dataSourceRequest.PageSize);
            DataSourceResult dataSourceResult = new DataSourceResult()
            {
                Data = list.Select(cart =>
                {
                    return cart.ToModel();
                }),
                Total = list.TotalRecords
            };
            return Json(dataSourceResult);
        }

        [HttpPost]
        [ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        [FormNameRequired("insertItem")]
        public ActionResult InsertIntoCart(AddToCartModel model)
        {
            if (!_customerService.IsCustomerExists(model.CustomerId))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, _localizationService.GetLocaleStringResource("Admin.Orders.Order.Customer.Id.Invalid",1,defaultValue: "Admin.Orders.Order.Customer.Id.Invalid"));
            if (!_productService.IsProductExists(model.ProductId))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, _localizationService.GetLocaleStringResource("Admin.Orders.Order.Product.Id.Invalid", 1, defaultValue: "Admin.Orders.Order.Product.Id.Invalid"));

            var shoppingCart = _shoppingCartService.IsItemExists(model.CustomerId, model.ProductId);
            if (shoppingCart != null)
            {
                shoppingCart.Quantity += model.Quantity;
                shoppingCart.TotalAmount = shoppingCart.Quantity * shoppingCart.Rate;
                shoppingCart.UpdatedDateTime = DateTime.UtcNow;
                _shoppingCartService.UpdateItem(shoppingCart);
            }
            else
            {
                var product = _productService.GetProductById(model.ProductId);
                var sc = new ShoppingCart()
                {
                    CustomerId = model.CustomerId,
                    ProductId = model.ProductId,
                    CreatedDateTime = DateTime.UtcNow,
                    Quantity = model.Quantity,
                    Rate = product.Price,
                    TotalAmount = model.Quantity * product.Price
                };
                _shoppingCartService.InsertItem(sc);
            }

            var grandTotal = _shoppingCartService.GetCartTotal(model.CustomerId);
            return Json(new { GrandTotal = grandTotal });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EditCartItem(AddToCartModel model)
        {
            var cartItem = _shoppingCartService.GetCartItem(model.Id, model.CustomerId);
            return Json(new { ProductId = cartItem.ProductId, ProductName=cartItem.Product.Name, Measurement=cartItem.Product.Measurement.Name, Quantity=cartItem.Quantity});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteCartItem(AddToCartModel model)
        {
            return Json("data delted");
        }
        
        [HttpPost]
        [ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        [FormNameRequired("updateItem")]
        public ActionResult UpdateCart(AddToCartModel model)
        {
            return Json(new { Name = "dinesh" });
        }

        #endregion
    }
}
