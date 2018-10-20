/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using Bam.Net;
using Bam.Net.Server.Renderers;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server.Meta
{
    [Proxy("appMeta")]
    public class AppMetaManager: IRequiresHttpContext
    {
        public const string AppNamePagesPathFormat = "~/apps/{appName}/pages";
        private AppMetaManager()
        {
        }

        public AppMetaManager(BamConf conf)
        {
            this.BamConf = conf;
        }

        [Exclude]
        public object Clone()
        {
            AppMetaManager clone = new AppMetaManager();
            clone.CopyProperties(this);
            return clone;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        ILogger _logger;
        object _loggerLock = new object();
        public ILogger Logger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () =>
                {
                    Log.Restart();
                    return Log.Default;
                });
            }
            set
            {
				lock (_loggerLock)
				{
					if (_logger != null)
					{
						_logger.StopLoggingThread();
					}

					_logger = value;
					_logger.RestartLoggingThread();
				}
            }
        }

        public BamConf BamConf
        {
            get;
            private set;
        }

        public AppMetaResult GetSuccessWrapper(string appName, object toWrap, string methodName = "Unspecified")
        {
            Logger.AddEntry("{0}::Success::{1}", appName, methodName);
            return new AppMetaResult(true, "", toWrap);
        }

        public AppMetaResult GetErrorWrapper(string appName, Exception ex, bool stack = true, string methodName = "Unspecified")
        {
            Logger.AddEntry("{0}::Error::{1}\r\n***{1}\r\n***", ex, appName, methodName, ex.Message);
            string message = GetMessage(ex, stack);
            return new AppMetaResult(false, message, null);
        }

        /// <summary>
        /// Called by client code
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public AppMetaResult GetPages(string appName = "localhost")
        {
            try
            {
                return GetSuccessWrapper(appName, GetPageNamesFromDomAppId(appName), MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                return GetErrorWrapper(appName, ex, true, MethodBase.GetCurrentMethod().Name);
            }
        }
		
        public AppMetaResult GetBook(string appName = "localhost", string pageName = "home")
        {
            try
            {
                return GetSuccessWrapper(appName, GetBookByAppAndPage(appName, pageName), MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                return GetErrorWrapper(appName, ex, true, MethodBase.GetCurrentMethod().Name);
            }
        }

        protected internal string[] GetPageNamesFromDomAppId(string appId)
        {
            string appName = appId;
            if (AppConf.AppNamesByDomAppId.ContainsKey(appId))
            {
                appName = AppConf.AppNamesByDomAppId[appId];
            }

            return GetPageNames(appName);
        }
        
        protected internal WebBook GetBookByAppAndPage(string appName = "localhost", string pageName = "home")
        {
            AppConf appConf = BamConf[appName];
            WebBook result = new WebBook();
            if (appConf != null)
            {
                string booksDir = "books";
                string bookFile = "{0}.json"._Format(pageName);
                Fs appRoot = appConf.AppRoot;
                if (appRoot.FileExists(booksDir, bookFile))
                {
                    result = appRoot.ReadAllText(booksDir, bookFile).FromJson<WebBook>();
                }
            }
            return result;
        }

        protected internal string[] GetPageNames(string appName = "localhost")
        {
            List<string> pageNames = new List<string>();
            DirectoryInfo pagesDir = new DirectoryInfo(BamConf.Fs.GetAbsolutePath(AppNamePagesPathFormat.NamedFormat(new { appName })));

            if (pagesDir.Exists)
            {
                AddPageNames(pagesDir, pageNames, pagesDir);
            }

            return pageNames.ToArray();
        }


        private string GetMessage(Exception ex, bool stack)
        {
            string st = stack ? ex.StackTrace : "";
            return string.Format("{0}:\r\n\r\n{1}", ex.Message, st);
        }

        private void AddPageNames(DirectoryInfo appPagesDir, List<string> pageNames, DirectoryInfo currentPageSubDir)
        {
            FileInfo[] files = currentPageSubDir.GetFiles("*.html");

            string prefix = currentPageSubDir.FullName.TruncateFront(appPagesDir.FullName.Length).Replace("\\", "/");
            if (!string.IsNullOrEmpty(prefix))
            {
                prefix = string.Format("{0}/", prefix.Substring(1, prefix.Length - 1));
            }
            foreach (FileInfo file in files)
            {
                string pageName = string.Format("{0}{1}", prefix, Path.GetFileNameWithoutExtension(file.Name));
                pageNames.Add(pageName);
            }

            Traverse(appPagesDir, pageNames, currentPageSubDir);
        }

        private void Traverse(DirectoryInfo pagesDir, List<string> pageNames, DirectoryInfo currentPageSubDir)
        {
            DirectoryInfo[] childDirs = currentPageSubDir.GetDirectories();
            for (int i = 0; i < childDirs.Length; i++)
            {
                currentPageSubDir = childDirs[i];
                AddPageNames(pagesDir, pageNames, currentPageSubDir);
            }
        }

    }
}
