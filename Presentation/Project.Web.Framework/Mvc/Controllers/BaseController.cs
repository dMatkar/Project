using Project.Web.Framework.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Framework.Mvc.Controllers
{
    public class BaseController : Controller
    {
        #region Methods
        protected virtual void AddNotification(Notify notify, string msg, bool isPersistForNextRequest = false)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return;

            if (isPersistForNextRequest)
            {
                if (TempData[string.Format("project.notifications.{0}", notify)] == null)
                    TempData[string.Format("project.notifications.{0}", notify)] = new List<string>();
                ((List<string>)TempData[string.Format("project.notifications.{0}", notify.ToString())]).Add(msg);
                TempData.Keep();
            }
            else
            {
                if (ViewData[string.Format("project.notifications.{0}", notify)] == null)
                    ViewData[string.Format("project.notifications.{0}", notify)] = new List<string>();
                ((List<string>)ViewData[string.Format("project.notifications.{0}", notify.ToString())]).Add(msg);
            }
        }

        protected virtual void AddSuccessNotification(string msg, bool isPersistForNextRequest = false)
        {
            AddNotification(Notify.Success, msg, isPersistForNextRequest);
        }

        protected virtual void AddErrorNotification(string msg, bool isPersistForNextRequest = false)
        {
            AddNotification(Notify.Error, msg, isPersistForNextRequest);
        }

        protected virtual void AddWarningNotification(string msg, bool isPersistForNextRequest = false)
        {
            AddNotification(Notify.Warning, msg, isPersistForNextRequest);
        }

        #endregion
    }
}
