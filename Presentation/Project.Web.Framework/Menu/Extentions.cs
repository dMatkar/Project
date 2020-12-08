using System;
using System.Linq;

namespace Project.Web.Framework.Menu
{
    public static class Extentions
    {
        public static bool ContainsSystemName(this SiteMapNode siteMap, string systemName)
        {
            if (systemName is null)
                return false;

            if (siteMap.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase))
                return true;

            return siteMap.ChildNodes.Value?.Any(x => ContainsSystemName(x, systemName)) ?? default;
        }
    }
}
