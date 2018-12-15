using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices
{
    public static class ProxyTemplateResources
    {
        public static string Path
        {
            get
            {
                return $"{typeof(ProxyTemplateResources).Namespace}.Templates.";
            }
        }
    }
}
