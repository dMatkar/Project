using Project.Core.Caching;
using Project.Core.Domain.Localization;
using Project.Core.Infrastructure;
using Project.Data;
using Project.Service.Localization;
using System.ComponentModel;

namespace Project.Web.Framework
{
    public class ProjectResourcesDisplayNameAttribute : DisplayNameAttribute
    {
        #region Fields

        private string _resourcesKey = string.Empty;

        #endregion

        #region Properties
        public string ResourcesKey
        {
            get => _resourcesKey;
        }

        public override string DisplayName
        {
            get
            {
                //return EngineContext.Current
                //     .Resolve<ILocalizationService>()
                //     .GetLocaleStringResource(ResourcesKey, 2, true, ResourcesKey);
                var src = new LocalizationService(new EfRepository<LocaleStringResource>(new ProjectDataContext("Project.Data.ProjectDataContext")), new MemoryCacheManager());
                return src.GetLocaleStringResource(ResourcesKey,1, true, ResourcesKey);
            }
        }
        #endregion

        #region Constructor

        public ProjectResourcesDisplayNameAttribute(string displayName) : base(displayName)
        {
            _resourcesKey = displayName;
        }

        #endregion
    }
}
