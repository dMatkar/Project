using System;
using System.Collections.Generic;

namespace Project.Web.Framework.Menu
{
    public class SiteMapNode
    {
        public SiteMapNode()
        {
            ChildNodes = new Lazy<IList<SiteMapNode>>(() => new List<SiteMapNode>());
        }
        public string Controller { get; set; }
        public string SystemName  { get; set; }
        public string Action { get; set; }
        public string IconClass { get; set; }
        public Lazy<IList<SiteMapNode>> ChildNodes { get; set; }
    }
}
