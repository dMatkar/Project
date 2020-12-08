﻿using System.Collections.Generic;
using System.Web.Optimization;

namespace Project.Web.Framework.UI
{
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
