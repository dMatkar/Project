using Project.Core.Domain.Catalog;
using Project.Service.Catalog;
using Project.Service.Helper;
using Project.Service.Localization;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Models.Catalog;
using Project.Web.Framework.kendoui;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructor

        public CategoryController(ICategoryService categoryService, IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService)
        {
            _categoryService = categoryService;
            _dateTimeHelper = dateTimeHelper;
            _localizationService = localizationService;
        }

        #endregion

        #region Categories list

        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            CategoryListModel categoryListModel = new CategoryListModel();
            categoryListModel.AvailableCategories.Add(new SelectListItem() { Text = "Category 1", Value = "1" });
            categoryListModel.AvailableCategories.Add(new SelectListItem() { Text = "Category 2", Value = "2" });
            return View(categoryListModel);
        }

        [HttpPost]
        public virtual ActionResult CategoryList(DataSourceRequest sourceRequest, CategoryListModel categoryListModel)
        {
            var categories = _categoryService.GetCategories(categoryId:categoryListModel.SearchCategoryId,pageIndex: sourceRequest.Page - 1, pageSize: sourceRequest.PageSize);
            DataSourceResult dataSourceResult = new DataSourceResult()
            {
                Data = categories.Select(category =>
                {
                    CategoryModel categoryModel = category.ToModel();
                    categoryModel.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(category.CreatedOnUtc, DateTimeKind.Utc);
                    return categoryModel;
                }),
                Total = categories.TotalRecords
            };
            return Json(dataSourceResult);
        }

        #endregion

        #region Insert/Update/Delete

        [HttpGet]
        public virtual ActionResult Create()
        {
            CategoryModel categoryModel = new CategoryModel();
            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = categoryModel.ToEntity();
                category.CreatedOnUtc = DateTime.UtcNow;
                category.UpdatedOnUtc = DateTime.UtcNow;
                 _categoryService.Insert(category);
                AddSuccessNotification(_localizationService.GetLocaleStringResource(resourcesKey: "Admin.Category.Category.Added", languageId: 1), true);
                return RedirectToAction("List");
            }
            return View(categoryModel);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Category category = _categoryService.GetCategory(Id);
            if (category is null)
                return RedirectToAction("List");
            CategoryModel categoryModel = category.ToModel();
            categoryModel.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(category.CreatedOnUtc,DateTimeKind.Utc);
            return View(categoryModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = _categoryService.GetCategory(categoryModel.Id);
                categoryModel.ToEntity(category);
                category.UpdatedOnUtc = DateTime.UtcNow;
                _categoryService.Update(category);
                AddSuccessNotification(_localizationService.GetLocaleStringResource("Admin.Category.Category.Edited", languageId: 1, defaultValue: "Admin.Category.Category.Edited"), true);
                return RedirectToAction("List");
            }
            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            Category category = _categoryService.GetCategory(Id);
            if (category is null)
                return RedirectToAction("List");

            _categoryService.Delete(category);
            AddSuccessNotification(_localizationService.GetLocaleStringResource("Admin.Category.Category.Deleted", languageId: 1, defaultValue: "Admin.Category.Category.Deleted"), true);
            return RedirectToAction("List");
        }

        #endregion
    }
}
