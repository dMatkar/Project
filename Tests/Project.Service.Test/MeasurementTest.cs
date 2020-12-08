using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Catalog;
using Project.Core.Infrastructure;
using Project.Data;
using Project.Service.Catalog;
using Project.Service.Helper;
using Project.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Test
{
    [TestClass]
    public class MeasurementTest
    {
        public static IMeasurementService GetInstance()
        {
            IDbContext dbContext = new ProjectDataContext("Project.Data.ProjectDataContext");
            IRepository<Measurement> measurementService = new EfRepository<Measurement>(dbContext);
            IMeasurementService measurementService1 = new MeasurementService(measurementService, new MemoryCacheManager());
            return measurementService1;
        }

        public static IDateTimeHelper GetDateTimeHelper()
        {
            return new DateTimeHelper();
        }

        [TestMethod]
        public void ShouldInsertData()
        {
            var service = GetInstance();
            Measurement measurement = new Measurement()
            {
                CreatedOnUtc = DateTime.UtcNow,
                ShortName = "ltr",
                Name = "Liter",
                UpdatedOnUtc = DateTime.UtcNow,
                AdminComment = "none"
            };
            service.InsertMeasurement(measurement);
            Assert.IsTrue(measurement.Id > 0);
        }

        [TestMethod]
        public void ShouldDisplayPagedList()
        {
            var service = GetInstance();
            var data = service.GetAllMeasurements(shortName:"ltr",name:"Liter");
            Assert.IsTrue(data.Count == 1);
        }

        [TestMethod]
        public void ShouldUpdateData()
        {
            var service = GetInstance();
            var data = service.GetMeasurement(3);
            data.Name = "liter";
            service.UpdateMeasurement(data);
            Assert.IsTrue(data.Name == "liter");
        }

        [TestMethod]
        public void SholdDeleteData()
        {
            var service = GetInstance();
            var data = service.GetMeasurement(3);
            service.DeleteMeasurement(data);
            Assert.IsTrue(service.GetAllMeasurements().Count == 2);
        }

        [TestMethod]
        public void ShouldDeleteData()
        {
            //var service = GetInstance();
            //MeasurementController measurementController = new MeasurementController(service);
            //measurementController.Delete(1);
        }
    }
}
