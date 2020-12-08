using Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Project.Web.Framework.UI
{
    public class PageHeadBuilder : IPageHeadBuilder
    {
        #region Fields 
        private readonly object obj = new object();

        private string  _activeSystemName;
        private readonly IDictionary<ResourcesLocation, IList<ScriptsReferenceMeta>> _scriptFiles = null;
        private readonly IDictionary<ResourcesLocation, IList<CssReferenceMeta>> _cssFiles = null;

        #endregion

        #region Properties

        string IPageHeadBuilder.ActiveSystemName
        {
            get => _activeSystemName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _activeSystemName = string.Empty;
                else
                    _activeSystemName = value;
            }
        }

        #endregion

        #region Constructor

        public PageHeadBuilder()
        {
            _scriptFiles = new Dictionary<ResourcesLocation, IList<ScriptsReferenceMeta>>();
            _cssFiles = new Dictionary<ResourcesLocation, IList<CssReferenceMeta>>();
        }

        #endregion

        #region Methods
        public void AddScriptFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle, bool isAsync)
        {
            if (!_scriptFiles.ContainsKey(resourcesLocation))
                _scriptFiles.Add(resourcesLocation, new List<ScriptsReferenceMeta>());

            if (string.IsNullOrWhiteSpace(parts))
                return;

            _scriptFiles[resourcesLocation].Add(new ScriptsReferenceMeta()
            {
                Parts = parts,
                ExcludeFromBundle = excludeFromBundle,
                IsAsync = isAsync
            });
        }

        public void AppendScriptFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle, bool isAsync)
        {
            if (!_scriptFiles.ContainsKey(resourcesLocation))
                _scriptFiles.Add(resourcesLocation, new List<ScriptsReferenceMeta>());

            if (string.IsNullOrEmpty(parts))
                return;

            _scriptFiles[resourcesLocation].Insert(0, new ScriptsReferenceMeta()
            {
                Parts = parts,
                ExcludeFromBundle = excludeFromBundle,
                IsAsync = isAsync
            });
        }

        public string GenerateScriptFiles(UrlHelper urlHelper, ResourcesLocation resourcesLocation)
        {
            if (!_scriptFiles.ContainsKey(resourcesLocation))
                return string.Empty;

            if (!_scriptFiles[resourcesLocation].Any())
                return string.Empty;

            var partsIncludeInBundle = _scriptFiles[resourcesLocation]
                        .Where(x => !x.ExcludeFromBundle)
                        .Select(x => x.Parts)
                        .Distinct()
                        .ToArray();
            var partsExcludeFromBundle = _scriptFiles[resourcesLocation]
                        .Where(x => x.ExcludeFromBundle)
                        .Select(x => new { x.Parts, x.IsAsync })
                        .Distinct()
                        .ToList();

            var result = new StringBuilder();
            if (partsIncludeInBundle.Any())
            {
                lock (obj)
                {
                    var bundleCollection = BundleTable.Bundles;
                    string virtualPath = "~/bundles/scripts";
                    Bundle scriptsBundle = bundleCollection.GetBundleFor(virtualPath);
                    if (scriptsBundle == null)
                    {
                        Bundle bundle = new ScriptBundle(virtualPath)
                        {
                            EnableFileExtensionReplacements = false,
                            Orderer = new AsIsBundleOrderer()
                        };
                        bundle.Include(partsIncludeInBundle);
                        bundleCollection.Add(bundle);
                    }
                    result.AppendLine(Scripts.Render(virtualPath).ToString());
                }

                foreach (var file in partsExcludeFromBundle)
                {
                    result.AppendFormat("<script {2} src=\"{0}\" type=\"{1}\"></script>", urlHelper.Content(file.Parts), MimeTypes.TextJavascript, file.IsAsync ? "async" : "");
                    result.AppendLine(Environment.NewLine);
                }
            }
            else
            {
                foreach (var file in partsExcludeFromBundle)
                {
                    result.AppendFormat("<script {2} src={0} type={1}></script>", urlHelper.Content(file.Parts), MimeTypes.TextJavascript, file.IsAsync ? "async" : "");
                    result.AppendLine(Environment.NewLine);
                }
            }
            return result.ToString();
        }

        public void AddCssFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle)
        {
            if (!_cssFiles.ContainsKey(resourcesLocation))
                _cssFiles.Add(resourcesLocation, new List<CssReferenceMeta>());

            _cssFiles[resourcesLocation].Add(new CssReferenceMeta()
            {
                Parts = parts,
                ExcludeFromBundle = excludeFromBundle
            });
        }

        public void AppendCssFile(ResourcesLocation resourcesLocation, string parts, bool excludeFromBundle)
        {
            if (!_cssFiles.ContainsKey(resourcesLocation))
                _cssFiles.Add(resourcesLocation, new List<CssReferenceMeta>());

            _cssFiles[resourcesLocation].Insert(0, new CssReferenceMeta()
            {
                Parts = parts,
                ExcludeFromBundle = excludeFromBundle
            });
        }

        public string GenerateCssFiles(UrlHelper urlHelper, ResourcesLocation resourcesLocation)
        {
            if (!_cssFiles.ContainsKey(resourcesLocation))
                return string.Empty;

            if (!_cssFiles.Any())
                return string.Empty;

            var partsIncludeInBundle = _cssFiles[resourcesLocation].Where(x => !x.ExcludeFromBundle).Select(x => x.Parts).ToArray();
            var partsExcludeFromBundle = _cssFiles[resourcesLocation].Where(x => x.ExcludeFromBundle).Select(x => x.Parts).ToArray();

            var result = new StringBuilder();
            if (partsIncludeInBundle.Any())
            {
                lock (obj)
                {
                    var bundleCollection = BundleTable.Bundles;
                    string virtualPath = "~/bundles/styles";
                    Bundle bundle = bundleCollection.GetBundleFor(virtualPath);
                    if (bundle is null)
                    {
                        Bundle styleBundle = new StyleBundle(virtualPath)
                        {
                            Orderer = new AsIsBundleOrderer(),
                            EnableFileExtensionReplacements = false
                        };
                        styleBundle.Include(partsIncludeInBundle);
                        bundleCollection.Add(styleBundle);
                    }
                    result.AppendLine(Styles.Render(virtualPath).ToString());
                }

                foreach (var file in partsExcludeFromBundle)
                {
                    result.AppendFormat("<link rel=\"stylesheet\" href=\"{0}\" type=\"{1}\">", urlHelper.Content(file), MimeTypes.TextCss).Append(Environment.NewLine);
                }
            }
            else
            {
                foreach (var file in partsExcludeFromBundle)
                {
                    result.AppendFormat("<link rel=\"stylesheet\" href=\"{0}\" type=\"{1}\">", urlHelper.Content(file), MimeTypes.TextCss).Append(Environment.NewLine);
                }
            }
            return result.ToString();
        }

        #endregion
    }
    public class CssReferenceMeta
    {
        public string Parts { get; set; }
        public bool ExcludeFromBundle { get; set; }
    }

    public class ScriptsReferenceMeta
    {
        public string Parts { get; set; }
        public bool ExcludeFromBundle { get; set; }
        public bool IsAsync { get; set; }
    }

}


