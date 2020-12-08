using Project.Core.Domain.Catalog;
using Project.Service.Catalog;
using Project.Service.Helper;
using Project.Service.Localization;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Helpers;
using Project.Web.Areas.Admin.Models.Catalog;
using Project.Web.Framework.kendoui;
using Project.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        #region Fields

        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IProductService _productService;
        private readonly ILocalizationService _localizationService;
        private readonly ICategoryService _categoryService;
        private readonly IMeasurementService _measurementService;

        #endregion

        #region Constructor

        public ProductController(IDateTimeHelper dateTimeHelper, IProductService productService,
            ILocalizationService localizationService, ICategoryService categoryService,
            IMeasurementService measurementService)
        {
            _dateTimeHelper = dateTimeHelper;
            _productService = productService;
            _localizationService = localizationService;
            _categoryService = categoryService;
            _measurementService = measurementService;
        }

        #endregion

        #region Utilities

        [NonAction]
        public virtual void PrepapreProductModel(ProductModel productModel)
        {
            productModel = productModel ?? new ProductModel();
            if (productModel != null && !productModel.CategoryIds.Any())
                productModel.CategoryIds = _categoryService.GetProductCategoriesByProductId(productModel.Id, false).Select(c => c.CategoryId).ToList();

            productModel.AvailableCategories = SelectListHelper.GetCategories(_categoryService, productModel.CategoryIds, false);
            productModel.AvailableMeasurements = SelectListHelper.GetMeasurements(_measurementService, productModel.MeasurementId, false);
        }

        [NonAction]
        public virtual void PrepareProductListModel(ProductListModel productListModel)
        {
            productListModel = productListModel ?? new ProductListModel();
            productListModel.AvailableCategories = SelectListHelper.GetCategories(_categoryService, null, false);
        }

        [NonAction]
        public virtual void SaveProductCategoryMapping(ProductModel productModel, Product product)
        {
            var existingProductCategoryMapping = _categoryService.GetProductCategoriesByProductId(product.Id, true);
            if (existingProductCategoryMapping != null && existingProductCategoryMapping.Any())
            {
                foreach (var pc in existingProductCategoryMapping)
                {
                    if (!productModel.CategoryIds.Contains(pc.CategoryId))
                        _categoryService.DeleteProductCategory(pc);
                    if (productModel.CategoryIds.Contains(pc.CategoryId))
                        productModel.CategoryIds.Remove(pc.CategoryId);
                }
            }

            foreach (var newId in productModel.CategoryIds)
            {
                _categoryService.InsertProductCategory(new ProductCategory() { CategoryId = newId, ProductId = product.Id });
            }
        }

        #endregion

        #region Products

        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            ProductListModel productListModel = new ProductListModel();
            PrepareProductListModel(productListModel);
            return View(productListModel);
        }

        [HttpPost]
        public ActionResult ProductsList(DataSourceRequest sourceRequest, ProductListModel productList)
        {
            var products = _productService.GetAllProducts(
                categoryId: productList.SearchCategoryId, productName: productList.SearchProductName,
                pageIndex: sourceRequest.Page - 1, pageSize: sourceRequest.PageSize);

            DataSourceResult dataSourceResult = new DataSourceResult()
            {
                Data = products.Select(product =>
                {
                    var productModel = product.ToModel();
                    productModel.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc);
                    return productModel;
                }),
                Total = products.TotalRecords
            };
            return Json(dataSourceResult);
        }

        #endregion

        #region Create/Edit/Delete

        public virtual ActionResult Create()
        {
            ProductModel productModel = new ProductModel();
            PrepapreProductModel(productModel);
            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = productModel.ToEntity();
                product.CreatedOnUtc = DateTime.UtcNow;
                product.UpdatedOnUtc = DateTime.UtcNow;
                _productService.InsertProduct(product);
                SaveProductCategoryMapping(productModel, product);
                AddSuccessNotification(_localizationService.GetLocaleStringResource("Admin.Products.Products.Added", languageId: 1, defaultValue: "Admin.Products.Products.Added"), true);
                return RedirectToAction("List");
            }
            PrepapreProductModel(productModel);
            return View(productModel);
        }

        public virtual ActionResult Edit(int Id)
        {
            var product = _productService.GetProductById(Id);
            if (product is null)
                return RedirectToAction("List");

            ProductModel productModel = product.ToModel();
            PrepapreProductModel(productModel);
            productModel.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetProductById(productModel.Id);
                productModel.ToEntity(product);
                product.UpdatedOnUtc = DateTime.UtcNow;
                _productService.UpdateProduct(product);
                SaveProductCategoryMapping(productModel, product);
                AddSuccessNotification(_localizationService.GetLocaleStringResource("Admin.Products.Products.Edited", languageId: 1, defaultValue: "Admin.Products.Products.Edited"), true);
                return RedirectToAction("List");
            }
            PrepapreProductModel(productModel);
            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int Id)
        {
            var product = _productService.GetProductById(Id);
            if (product is null)
                return RedirectToAction("List");

            _productService.DeleteProduct(product);
            AddSuccessNotification(_localizationService.GetLocaleStringResource("Admin.Products.Products.Deleted", languageId: 1, defaultValue: "Admin.Products.Products.Deleted"), true);
            return RedirectToAction("List");
        }

        #endregion

        #region Ajax Action Methods

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SearchProduct(string search)
        {
            if (string.IsNullOrEmpty(search))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var data = _productService.SearchProductByName(search).Select(c =>
              {
                  return new Select2Model() { Id = c.Key.ToString(), Text = c.Value };
              });
            return Json(new { items = data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult GetProductInfo(int Id)
        {
            var product = _productService.GetProductById(Id);
            if (product is null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return Json(new { CurrentStock = product.StockQuantity, product.Price, Measurement = product.Measurement?.Name });
        }

        #endregion
    }
}
