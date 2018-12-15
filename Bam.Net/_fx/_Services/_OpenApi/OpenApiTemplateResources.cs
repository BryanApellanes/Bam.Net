using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Services.OpenApi
{
    public static class OpenApiTemplateResources
    {
        public static string Path
        {
            get
            {
                return $"{System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().GetFilePath())}.Services.OpenApi.Templates.";
            }
        }
    }
}
