/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using Bam.Net.ServiceProxy;
using Bam.Net;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.Configuration;
using Bam.Net.Incubation;
using Bam.Net.Presentation.Html;

namespace Bam.Net.Server
{
    public class BamController: BaseController
    {
        public BamController()
        {
            
        }

        private DirectoryInfo GetPagesDir(string bamAppName)
        {
            return new DirectoryInfo(Server.MapPath("~/bam/apps/{0}/pages"._Format(bamAppName)));
        }

        private string[] GetPageNames(string bamAppName)
        {
            List<string> pageNames = new List<string>();
            DirectoryInfo pagesDir = GetPagesDir(bamAppName);

            if (pagesDir.Exists)
            {
                _dirToSearch = pagesDir;
                AddPageNames(pagesDir, pageNames);
            }

            return pageNames.ToArray();
        }

        DirectoryInfo _dirToSearch;
        private void AddPageNames(DirectoryInfo pagesDir, List<string> pageNames)
        {
            FileInfo[] files = _dirToSearch.GetFiles("*.html");

            string prefix = _dirToSearch.FullName.Replace(pagesDir.FullName, "");
            if (!string.IsNullOrEmpty(prefix))
            {
                prefix = string.Format("{0}\\", prefix.Substring(1, prefix.Length - 1));
            }
            foreach (FileInfo file in files)
            {
                string pageName = string.Format("{0}{1}", prefix, Path.GetFileNameWithoutExtension(file.Name));
                pageNames.Add(pageName);
            }

            Traverse(pagesDir, pageNames);
        }

        private void Traverse(DirectoryInfo pagesDir, List<string> pageNames)
        {
            DirectoryInfo[] childDirs = _dirToSearch.GetDirectories();
            for (int i = 0; i < childDirs.Length; i++)
            {
                _dirToSearch = childDirs[i];
                AddPageNames(pagesDir, pageNames);
            }
        }

        public ActionResult GetPages(string bamAppName = "main")
        {
            string methodName = "{0}.{1}"._Format(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            try
            {
                return Json(GetSuccessWrapper(GetPageNames(bamAppName), methodName));
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex, true, methodName));
            }
        }       

        public ActionResult AppScripts(string callback, string bamAppName = "main", bool refresh = false, bool min = false)
        {
            string methodName = "{0}.{1}"._Format(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            try
            {
                StringBuilder script = new StringBuilder();
                script.Append(Bam.CombinedAppScript(this.Server, bamAppName, refresh, min));
                script.Append("\r\n\r\n");
                script.Append(callback);
                return this.JavaScript(script.ToString());
            }
            catch (Exception ex)
            {
                return this.JavaScript("alert('an error occurred loading app scripts for {0}: {1}');"._Format(bamAppName, ex.Message.Replace("\r","").Replace("\n", "")));
            }
        }

        public ActionResult MethodForm(string className, string methodName, ParameterLayouts layout = ParameterLayouts.Default)
        {
            return new MethodFormResult(className, methodName, null, layout);            
        }

        public ActionResult LoadPage(string pageName, string bamAppName = "main")
        {
            DirectoryInfo pagesDir = GetPagesDir(bamAppName);
            string methodName = "{0}.{1}"._Format(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            string pagePath = Path.Combine(pagesDir.FullName, pageName);
            try
            {                
                if (pagesDir.Exists && System.IO.File.Exists(pagePath))
                {
                    string content = System.IO.File.ReadAllText(pagePath);
                    return GetSuccessWrapper(content);
                }
                else
                {
                    return Json(GetSuccessWrapper("{0} page not found"._Format(pageName)));
                }
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex, true, methodName));
            }
        }

        private string GetAppNameFromRequest()
        {
            string hostName = Request.Url.Host;
            string result = hostName;
            string[] splitHost = hostName.DelimitSplit(".");

            if (splitHost[0].ToLowerInvariant().Equals("www"))
            {
                result = splitHost[1];
            }
            else if (splitHost.Length == 2)
            {
                result = splitHost[0];
            }

            if (!AppPathExists(hostName))
            {
                hostName = DefaultConfiguration.GetAppSetting("ApplicationName", "main");
                if (!hostName.Equals("main") && !AppPathExists(hostName))
                {
                    Log.AddEntry("ApplicationName {0} was specified in the config but no bam app was found by that name", LogEventType.Warning, hostName);
                    hostName = "main";
                }
                result = hostName;
                Log.AddEntry("Unable to determine appName from hostName {0}, directing to 'main' instead.", LogEventType.Warning, hostName);
            }

            return result;
        }

        private bool AppPathExists(string hostName)
        {
            string appPath = Server.MapPath(Bam.AppRoot.NamedFormat(new { appName = hostName }));
            bool exists = Directory.Exists(appPath);
            
            return exists;
        }

        public ActionResult Start(string appName = "")
        {
            if (string.IsNullOrEmpty(appName))
            {
                appName = GetAppNameFromRequest();
            }

            Fs.RegisterProxy(appName);
            return View("Start", (object)appName);
        }

        public ActionResult DisableFsAccess()
        {
            try
            {
                Fs.UnregisterProxy();
                return Json(GetSuccessWrapper(true, MethodBase.GetCurrentMethod().Name));
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex, true, MethodBase.GetCurrentMethod().Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
