/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Bam.Net.Caching.File;
using Bam.Net.Logging;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;
using Yahoo.Yui.Compressor;
using Bam.Net.Presentation;
using System.Reflection;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using System.Linq;
using Bam.Net.Configuration;

namespace Bam.Net.Server
{
    public partial class AppContentResponder : ContentResponder
    {
        public Includes GetAppIncludes()
        {
            return GetAppIncludes(AppConf);
        }
    }
}
