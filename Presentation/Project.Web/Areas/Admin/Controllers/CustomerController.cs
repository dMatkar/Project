using Project.Core.Domain.Customers;
using Project.Service.Customers;
using Project.Service.Directory;
using Project.Service.Helper;
using Project.Service.Localization;
using Project.Service.Orders;
using Project.Web.Areas.Admin.Extentions;
using Project.Web.Areas.Admin.Helpers;
using Project.Web.Areas.Admin.Models.Customers;
using Project.Web.Framework.kendoui;
using Project.Web.Framework.Models;
using Project.Web.Framework.Mvc.Attributes;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseAdminController
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IStateService _stateService;
        private readonly IShoppingCartService _shoppingCartService;

        #endregion

        #region Constructor

        public CustomerController(ICustomerService customerService, CustomerSettings customerSettings,
            ILocalizationService localizationService, IDateTimeHelper dateTimeHelper, IStateService stateService, IShoppingCartService shoppingCartService)
        {
            _customerService = customerService;
            _customerSettings = customerSettings;
            _localizationService = localizationService;
            _dateTimeHelper = dateTimeHelper;
            _stateService = stateService;
            _shoppingCartService = shoppingCartService;
        }

        #endregion

        #region Utilities

        public void PrepareCustomerModel(CustomerModel customerModel)
        {
            if (customerModel is null)
                throw new ArgumentException("customerModel");

            customerModel.Address.AvailableStates = SelectListHelper.GetAvailableStates(_stateService, customerModel.Address.StateProvinceId);
        }

        #endregion

        #region Customers

        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            CustomerList customerList = new CustomerList();
            return View(customerList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CustomersList(DataSourceRequest dataSourceRequest, CustomerList customerSearchModel)
        {
            var customers = _customerService.GetAllCustomers(
                  firstName: customerSearchModel.SearchFirstName,
                  lastName: customerSearchModel.SearchLastName,
                  email: customerSearchModel.SearchEmail,
                  mobileNumber: customerSearchModel.SearchMobileNumber,
                  pageIndex: dataSourceRequest.Page - 1,
                  pageSize: dataSourceRequest.PageSize
                  );

            DataSourceResult dataSourceResult = new DataSourceResult()
            {
                Data = customers.Select(customer =>
                {
                    CustomerModel customerModel = customer.ToModel();
                    customerModel.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
                    return customerModel;
                }),
                Total = customers.TotalRecords
            };
            return Json(dataSourceResult);
        }

        #endregion

        #region Create/Update/Delete

        public virtual ActionResult Create()
        {
            CustomerModel customerModel = new CustomerModel();
            PrepareCustomerModel(customerModel);
            return View(customerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                Customer customer = customerModel.ToEntity();
                customer.CreatedOnUtc = DateTime.UtcNow;
                customer.Address.CreatedOnUtc = DateTime.UtcNow;
                _customerService.InsertCustomer(customer);
                AddSuccessNotification(_localizationService.GetLocaleStringResource(resourcesKey: "Admin.Customers.Customers.Added", languageId: 1, defaultValue: "Admin.Customers.Customers.Added"), true);
                return RedirectToAction("List");
            }
            return View(customerModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int Id)
        {
            Customer customer = _customerService.GetCustomerById(customerId: Id);
            if (customer is null)
                return RedirectToAction("List");

            CustomerModel customerModel = customer.ToModel();
            customerModel.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
            return View(customerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _customerService.GetCustomerById(customerId: customerModel.Id);
                customerModel.ToEntity(customer: customer);
                customer.UpdatedOnUtc = DateTime.UtcNow;
                customer.UpdatedOnUtc = DateTime.UtcNow;
                _customerService.UpdateCustomer(customer: customer);
                AddSuccessNotification(_localizationService.GetLocaleStringResource("Admin.Customers.Customers.Edited", languageId: 1, defaultValue: "Admin.Customers.Customers.Edited"), true);
                return RedirectToAction("List");
            }
            return View(customerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int Id)
        {
            Customer customer = _customerService.GetCustomerById(Id);
            if (customer != null)
            {
                _customerService.DeleteCustomer(customer);
                AddSuccessNotification(_localizationService.GetLocaleStringResource(resourcesKey: "Admin.Customers.Customers.Deleted", languageId: 1, defaultValue: "Admin.Customers.Customers.Deleted"), true);
            }
            return RedirectToAction("List");
        }

        #endregion

        #region Export / Import

        [HttpPost, ActionName("List")]
        [FormNameRequired("exportexcel-All")]
        public virtual ActionResult ExportExcelAll(CustomerList customerSearchModel)
        {
            return View(new CustomerList());
        }

        [HttpPost]
        public virtual ActionResult ExportExcelSelected([ModelBinder(typeof(CommaSeparatedModelBinder))]int[] selectedIds)
        {
            return View(selectedIds);
        }

        #endregion

        #region Ajax Action Methods

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchCustomer(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var data = _customerService.SearchCustomerByName(search).Select(c =>
             {
                 return new Select2Model() { Id = c.Key.ToString(), Text = c.Value };
             });
            return Json(new { items = data});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCustomerAddress(int Id)
        {
            var customer = _customerService.GetCustomerById(Id);
            if (customer is null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var cartTotal = _shoppingCartService.GetCartTotal(customer.Id);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{customer.Address.Address1},\n{customer.Address.Address2},{customer.Address.Address3},");
            stringBuilder.AppendLine($"\n{customer.Address.City},{customer.Address.StateProvinceId},");
            stringBuilder.AppendLine($"\n{customer.Address.ZipPostalCode}");
            return Json(new { Address = stringBuilder.ToString() ,GrandTotal=cartTotal});
        }

        #endregion
    }
}
