using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.CoreServices
{
    public static class ProxyTemplateResources
    {
        public static string Path
        {
            get
            {
                return $"{System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().GetFilePath())}.CoreServices.Templates.";
            }
        }
    }
}
