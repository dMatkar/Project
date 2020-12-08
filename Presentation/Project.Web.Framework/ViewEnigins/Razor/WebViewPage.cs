using Project.Core.Caching;
using Project.Core.Domain.Localization;
using Project.Data;
using Project.Service.Localization;
using Project.Web.Framework.Localization;

namespace Project.Web.Framework.ViewEnigins.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private Localizer _localizer;
        private ILocalizationService _localizationService;

        public Localizer T
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                      {
                          string resourcesValue = _localizationService.GetLocaleStringResource(format, 1, true, format);
                          if (args == null && args.Length == 0)
                          {
                              return string.Format(resourcesValue, args);
                          }
                          return resourcesValue;
                      };
                }
                return _localizer;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            _localizationService = new LocalizationService(new EfRepository<LocaleStringResource>(new ProjectDataContext("Project.Data.ProjectDataContext")), new MemoryCacheManager());
            //_localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        }
    }
}
