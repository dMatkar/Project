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
    public class MeasurementController : BaseAdminController
    {
        #region Fields

        private readonly IMeasurementService _measurementService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructor

        public MeasurementController(IMeasurementService measurementService, IDateTimeHelper dateTimeHelper, ILocalizationService localizationService)
        {
            _measurementService = measurementService;
            _dateTimeHelper = dateTimeHelper;
            _localizationService = localizationService;
        }

        #endregion

        #region Measurements

        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            MeasurementListModel measurementListModel = new MeasurementListModel();
            return View(measurementListModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult MeasurementsList(DataSourceRequest sourceRequest, MeasurementListModel searchModel)
        {
            var measurements = _measurementService.GetAllMeasurements(name: searchModel.SearchName, shortName: searchModel.SearchName,
                 pageIndex: sourceRequest.Page - 1, pageSize: sourceRequest.PageSize);

            DataSourceResult dataSourceResult = new DataSourceResult()
            {
                Data = measurements.Select(m =>
                {
                    var model = m.ToModel();
                    model.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(m.CreatedOnUtc, DateTimeKind.Utc);
                    return model;
                }),
                Total = measurements.TotalPages
            };
            return Json(dataSourceResult);
        }

        #endregion

        #region Create/Update/Delete

        public virtual ActionResult Create()
        {
            MeasurementModel measurementModel = new MeasurementModel();
            return View(measurementModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(MeasurementModel measurementModel)
        {
            if (ModelState.IsValid)
            {
                var entity = measurementModel.ToEntity();
                entity.CreatedOnUtc = DateTime.UtcNow;
                entity.UpdatedOnUtc = DateTime.UtcNow;
                _measurementService.InsertMeasurement(entity);
                AddSuccessNotification(_localizationService.GetLocaleStringResource(resourcesKey: "Admin.Measurements.Measurements.Added", languageId: 1, defaultValue: "Admin.Measurements.Measurements.Added"), true);
                return RedirectToAction("List");
            }
            return View(measurementModel);
        }

        public virtual ActionResult Edit(int Id)
        {
            Measurement measurement = _measurementService.GetMeasurement(Id);
            if (measurement is null)
                return RedirectToAction("List");

            MeasurementModel model = measurement.ToModel();
            model.CreatedOnUtc = _dateTimeHelper.ConvertToUserTime(measurement.CreatedOnUtc, DateTimeKind.Utc);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(MeasurementModel measurementModel)
        {
            if (ModelState.IsValid)
            {
                Measurement measurement = _measurementService.GetMeasurement(measurementModel.Id);
                Measurement entity = measurementModel.ToEntity(measurement);
                entity.UpdatedOnUtc = DateTime.UtcNow;
                _measurementService.UpdateMeasurement(measurement);
                AddSuccessNotification(_localizationService.GetLocaleStringResource(resourcesKey: "Admin.Measurements.Measurements.Edited", languageId: 1, defaultValue: "Admin.Measurements.Measurements.Edited"), true);
                return RedirectToAction("List");
            }
            return View(measurementModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int Id)
        {
            Measurement measurement = _measurementService.GetMeasurement(Id);
            if (measurement is null)
                return RedirectToAction("List");

            _measurementService.DeleteMeasurement(measurement);
            AddSuccessNotification(_localizationService.GetLocaleStringResource(resourcesKey: "Admin.Measurements.Measurements.Deleted", languageId: 1, defaultValue: "Admin.Measurements.Measurements.Deleted"), true);
            return RedirectToAction("List");
        }

        #endregion
    }
}
