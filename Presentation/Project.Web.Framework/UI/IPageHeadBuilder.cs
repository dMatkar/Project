using System.Web.Mvc;

namespace Project.Web.Framework.UI
{
    public interface IPageHeadBuilder
    {
        #region Properties
        string ActiveSystemName { get; set; }

        #endregion

        #region Methods

        void AddCssFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle);
        void AppendCssFile(ResourcesLocation resourcesLocation, string parts,bool excludeFromBundle);
        string GenerateCssFiles(UrlHelper urlHelper, ResourcesLocation resourcesLocation);

        void AddScriptFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle, bool isAsync);
        void AppendScriptFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle, bool isAsync);
        string GenerateScriptFiles(UrlHelper urlHelper, ResourcesLocation resourcesLocation);

        #endregion
    }
}
