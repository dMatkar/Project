using Project.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Framework.UI
{
    public static class LayoutExtentions
    {
        public static IPageHeadBuilder _pageHeadBuilder;

        #region Properties

        public static IPageHeadBuilder PageHeadBuilder
        {
            get => _pageHeadBuilder ?? (_pageHeadBuilder = new PageHeadBuilder());
        }

        #endregion

        #region Methods
        /// <summary>
        /// Set active menu item.
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static void SetActiveSystemName(this HtmlHelper _, string systemName)
        {
            PageHeadBuilder.ActiveSystemName = systemName;
        }

        /// <summary>
        /// Get active system name.
        /// </summary>
        /// <returns></returns>
        public static string GetActiveSystemName(this HtmlHelper _)
        {
            return PageHeadBuilder.ActiveSystemName;
        }

        /// <summary>
        /// Used to Add Script file
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="resourcesLocation"></param>
        /// <param name="parts"></param>
        /// <param name="excludeFromBundle"></param>
        /// <param name="isAsync"></param>
        public static void AddScriptFile(this HtmlHelper _, ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle = false, bool isAsync = false)
        {
            PageHeadBuilder.AddScriptFile(resourcesLocation, parts, excludeFromBundle, isAsync);
        }

        /// <summary>
        /// Used insert script file at the first position
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="resourcesLocation"></param>
        /// <param name="parts"></param>
        /// <param name="excludeFromBundle"></param>
        /// <param name="isAsync"></param>
        public static void AppendScriptFile(this HtmlHelper _, ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle = false, bool isAsync = false)
        {
            PageHeadBuilder.AppendScriptFile(resourcesLocation, parts, excludeFromBundle, isAsync);
        }

        /// <summary>
        /// Generate scripts.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="urlHelper"></param>
        /// <param name="resourcesLocation"></param>
        /// <returns></returns>
        public static IHtmlString GenerateScriptLinks(this HtmlHelper _, UrlHelper urlHelper, ResourcesLocation resourcesLocation)
        {
            return MvcHtmlString.Create(PageHeadBuilder.GenerateScriptFiles(urlHelper, resourcesLocation));
        }

        /// <summary>
        /// Add css files.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="resourcesLocation"></param>
        /// <param name="parts"></param>
        /// <param name="excludeFromBundle"></param>
        public static void AddCssFile(this HtmlHelper _, ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle = false)
        {
            PageHeadBuilder.AddCssFile(resourcesLocation, parts, excludeFromBundle);
        }

        /// <summary>
        /// Insert css file.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="resourcesLocation"></param>
        /// <param name="parts"></param>
        /// <param name="excludeFromBundle"></param>
        public static void AppendCssFile(this HtmlHelper _, ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle = false)
        {
            PageHeadBuilder.AppendCssFile(resourcesLocation, parts, excludeFromBundle);
        }

        /// <summary>
        /// Generate css files link.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="urlHelper"></param>
        /// <param name="resourcesLocation"></param>
        /// <returns></returns>
        public static IHtmlString GenerateStylesheetLinks(this HtmlHelper _, UrlHelper urlHelper, ResourcesLocation resourcesLocation)
        {
            return MvcHtmlString.Create(PageHeadBuilder.GenerateCssFiles(urlHelper, resourcesLocation));
        }
        #endregion
    }
}
