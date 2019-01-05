/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dst = Bam.Net.Presentation.Dust;
using Bam.Net;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using dotless.Core;
using dotless.Core.configuration;
using dotless.Core.Input;
using IO = System.IO;
using Bam.Net.Drawing;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
    public class DesignController : BaseController
    {
        //
        // GET: /Design/

        public ActionResult Index()
        {
            return View();
        }

        internal static HttpServerUtilityBase StaticServer
        {
            get;
            set;
        }

        internal sealed class LessFileReader : IFileReader
        {

            public byte[] GetBinaryFileContents(string fileName)
            {
                return IO.File.ReadAllBytes(StaticServer.MapPath("~/bam/less/" + fileName));
            }

            public string GetFileContents(string fileName)
            {
                return IO.File.ReadAllText(StaticServer.MapPath("~/bam/less/" + fileName));
            }

            public bool DoesFileExist(string fileName)
            {
                return IO.File.Exists(StaticServer.MapPath("~/bam/less/" + fileName));
            }

			#region IFileReader Members


			public bool UseCacheDependencies
			{
				get { return false; }
			}

			#endregion
		}

        public ActionResult GetColorScheme()
        {
            string jsonFile = Server.MapPath("~/bam/js/json/colorscheme.json");
            return Json(ColorPalette.Load(jsonFile));
        }

        /// <summary>
        /// Saves the specified ColorScheme to ~/bam/json/colorscheme.json and returns 
        /// the updated contents of variables.less
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public ActionResult LessBootstrapVariables(ColorPalette scheme)
        {
            scheme.ToJson().SafeWriteToFile(Server.MapPath("~/bam/js/json/colorscheme.json"), true);
            return PartialView(scheme);
        }

        private void SetServer(HttpServerUtilityBase server)
        {
            if (StaticServer == null)
            {
                StaticServer = server;
            }
        }

        public ActionResult SetColorScheme(string variablesDotLess)
        {
            try
            {
                SetServer(Server);
                variablesDotLess.SafeWriteToFile(Server.MapPath("~/bam/less/variables.less"), true);
                string less = IO.File.ReadAllText(Server.MapPath("~/bam/less/bootstrap.less"));

                DotlessConfiguration config = new DotlessConfiguration();
                config.MinifyOutput = false;
                config.ImportAllFilesAsLess = true;
                config.CacheEnabled = false;
                config.LessSource = typeof(LessFileReader);
                string result = Less.Parse(less, config);
                result.SafeWriteToFile(Server.MapPath("~/content/bootstrap.css"), true);

                return Json(GetSuccessWrapper(true));
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex));
            }
        }

        public ActionResult DeriveColorPalette(string url)
        {
            try
            {
                ColorPalette cp = ColorPalette.DeriveFrom(new Uri(url));
                return Json(GetSuccessWrapper(cp.Colors));
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex));
            }
        }

        public ActionResult Db()
        {
            return View();
        }
    }
}
