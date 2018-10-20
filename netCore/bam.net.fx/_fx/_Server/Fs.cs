/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bam.Net;
using Bam.Net.Configuration;
using Bam.Net.Presentation.Html;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using System.Web.Mvc;
using System.Web;
using Bam.Net.Caching.File;

namespace Bam.Net.Server
{
    public partial class Fs
    {

        public Fs(HttpServerUtilityBase server, string appName) : this()
        {
            string root = server.MapPath("~/apps/{appName}/".NamedFormat(new { appName = appName }));
            RootDir = new DirectoryInfo(root);
            AppName = appName;
        }

        public Fs(HttpServerUtility server, string appName) : this()
        {
            string root = server.MapPath("~/apps/{appName}/".NamedFormat(new { appName = appName }));
            RootDir = new DirectoryInfo(root);
            AppName = appName;
        }

        public Fs(Controller controller, string appName)
            : this(controller.Server, appName)
        {
        }
    }
}