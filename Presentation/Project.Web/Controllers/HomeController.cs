using Project.Core.Domain.Customers;
using Project.Core.Infrastructure;
using Project.Service.Configuration;
using Project.Service.Customers;
using Project.Web.Framework.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            AddSuccessNotification("File saved successfully");  
            return View();
        }
    }
}