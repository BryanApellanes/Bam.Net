using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Messaging;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class WebActions : CommandLineTestInterface
    {
        [ConsoleAction("addPage", "Add a page to the current BamFramework project")]
        public void AddPage()
        {
            string pageName = Arguments["addPage"] ?? Arguments["ap"];
            if (string.IsNullOrEmpty(pageName))
            {
                OutLine("Page name not specified", ConsoleColor.Magenta);
                Exit(1);
            }

            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root then add the files
            // - Pages/[pagePath].cshtml
            // - Pages/[pagePath].cshtml.cs
            // - wwwroot/bam.js/pages/[pagePath].js
            // - wwwroot/bam.js/configs/[pagePath]/webpack.config.js
        }

        [ConsoleAction("webpack", "WebPack each bam.js page found in wwwroot/bam.js/pages using corresponding configs found in wwwroot/bam.js/configs")]
        public void WebPack()
        {
            // change directories into wwwroot/bam.js
            // for every webpack.config.js file in ./configs/ call
            // npx  webpack --config [configPath]
        }
    }
}
