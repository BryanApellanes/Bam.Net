/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net;
using Bam.Net.Javascript;
using Bam.Net.Html;
using Bam.Net.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yahoo.Yui.Compressor;

namespace Bam.Net.Server
{
    public static class Bam
    {
        static Bam()
        {

        }

        public static MvcHtmlString Icon(Icons icon)
        {
            return new Tag("i", new { Class = BootStrapIcons.All[(int)icon] });
        }

        public const string AppsRoot = "~/bam/apps/";
        public const string AppRoot = "~/bam/apps/{appName}/";
        public const string AppPages = "~/bam/apps/{appName}/pages/";
        public const string AppJs = "~/bam/apps/{appName}/js/";
        public const string AppViewModels = "~/bam/apps/{appName}/viewModels/";

        /// <summary>
        /// Gets script tags for all scripts pertaining to the specified appName.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="appName"></param>
        /// <param name="reloadConfigScripts"></param>
        /// <returns></returns>
        public static MvcHtmlString Scripts(WebViewPage page, string appName)
        {
            object appNameObj = new { appName = appName };
            StringBuilder commonConfigScripts = CreateCommonConfigScriptTags(page, appNameObj);
            StringBuilder appConfigScripts = CreateAppConfigScriptTags(page, appNameObj);
            StringBuilder jsScripts = CreateJsScriptsTags(page, appNameObj);
            StringBuilder pagesScripts = CreatePagesScriptsTags(page, appNameObj);
            StringBuilder viewModelScripts = CreateViewModelsScriptTags(page, appNameObj); 
            StringBuilder appInitScript = CreateAppInitScriptTag(page, appNameObj);
            StringBuilder dustBamTemplates = CreateDustBamTemplatesScriptTag(page, appNameObj);

            StringBuilder all = new StringBuilder()
                .A(commonConfigScripts.ToString())
                .A(appConfigScripts.ToString())
                .A(jsScripts.ToString())
                .A(pagesScripts.ToString())
                .A(viewModelScripts.ToString())
                .A(appInitScript.ToString())
                .A(dustBamTemplates.ToString());

            return MvcHtmlString.Create(all.ToString());
        }

        static Dictionary<string, string> _combinedScripts = new Dictionary<string, string>();
        static Dictionary<string, string> _combinedMinScripts = new Dictionary<string, string>();

        public static string CombinedAppScript(HttpServerUtilityBase server, string appName, bool refresh = false, bool min = true)
        {
            if (_combinedScripts.ContainsKey(appName) && 
                _combinedMinScripts.ContainsKey(appName) &&
                !refresh)
            {
                return min ? _combinedMinScripts[appName] : _combinedScripts[appName];
            }

            string[] scriptPaths = GetAppScriptPaths(server, appName);

            StringBuilder combined = new StringBuilder();
            StringBuilder combinedAndCompressed = new StringBuilder();
            JavaScriptCompressor jsc = new JavaScriptCompressor();
            foreach (string scriptPath in scriptPaths)
            {
                if (!File.Exists(scriptPath))
                {
                    Log.AddEntry("BAM::Script not found: {0}", LogEventType.Warning, scriptPath);
                }
                else
                {
                    string code = File.ReadAllText(scriptPath);
                    combined.Append(code);
                    combined.Append(";");

                    string compressed = jsc.Compress(code);
                    combinedAndCompressed.Append(compressed);
                    combinedAndCompressed.Append(";\r\n");
                }
            }

            string src = combined.ToString();
            _combinedScripts[appName] = src;

            string compressedSrc = combinedAndCompressed.ToString();
            _combinedMinScripts[appName] = compressedSrc;

            return min ? compressedSrc: src;
        }

        public static string[] GetAppScriptPaths(HttpServerUtilityBase server, string appName)
        {
            object appNameObj = new { appName = appName };

            string[] commonConfigScriptPaths = GetCommonConfigScriptPaths(server);
            string[] appConfigScriptPaths = GetAppConfigScriptPaths(appNameObj, server);
            string[] viewModelScripts = new string[] { };
            string[] pagesScripts = new string[] { };
            string[] jsScripts = new string[] { };
            string initPath = new StringBuilder()
                                    .A(AppRoot)
                                    .A("init.js")
                                    .ToString()
                                    .NamedFormat(appNameObj);

            string viewModelsDirPath = server.MapPath(AppViewModels.NamedFormat(appNameObj));
            DirectoryInfo viewModelsDir = new DirectoryInfo(viewModelsDirPath);
            if (viewModelsDir.Exists)
            {
                viewModelScripts = GetScriptPathsFromDirectory(appNameObj, AppViewModels, viewModelsDir);
            }

            string pagesDirPath = server.MapPath(AppPages.NamedFormat(appNameObj));
            DirectoryInfo pagesScriptsDir = new DirectoryInfo(pagesDirPath);
            if (pagesScriptsDir.Exists)
            {
                pagesScripts = GetScriptPathsFromDirectory(appNameObj, AppPages, pagesScriptsDir);
            }

            string jsDirPath = server.MapPath(AppJs.NamedFormat(appNameObj));
            DirectoryInfo jsDir = new DirectoryInfo(jsDirPath);
            if (jsDir.Exists)
            {
                jsScripts = GetScriptPathsFromDirectory(appNameObj, AppJs, jsDir);
            }

            List<string> scriptPaths = new List<string>();
            scriptPaths.AddRange(commonConfigScriptPaths);
            scriptPaths.AddRange(appConfigScriptPaths);
            scriptPaths.AddRange(viewModelScripts);
            scriptPaths.AddRange(pagesScripts);
            scriptPaths.AddRange(jsScripts);
            scriptPaths.Add(initPath);
            return scriptPaths.ToArray();
        }


        /// <summary>
        /// Get script tags that reference the scripts listed in the /bam/apps/config.js file
        /// </summary>
        /// <param name="page"></param>
        /// <param name="namedFormatValues"></param>
        /// <returns></returns>
        public static MvcHtmlString CommonConfigScripts(WebViewPage page, object namedFormatValues = null)
        {
            StringBuilder scriptTags = CreateCommonConfigScriptTags(page, namedFormatValues);

            return MvcHtmlString.Create(scriptTags.ToString());
        }

        /// <summary>
        /// Get script tags that reference the scripts listed in 
        /// the /bam/apps/{appName}/config.js file.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="namedFormatValues"></param>
        /// <returns></returns>
        public static MvcHtmlString AppConfigScripts(WebViewPage page, object namedFormatValues)
        {
            StringBuilder scriptTags = CreateAppConfigScriptTags(page, namedFormatValues);

            return MvcHtmlString.Create(scriptTags.ToString());
        }

        /// <summary>
        /// Get script tags that reference scripts in the pages folder
        /// </summary>
        /// <param name="page"></param>
        /// <param name="namedFormatValues"></param>
        /// <returns></returns>
        public static MvcHtmlString AppScripts(WebViewPage page, object namedFormatValues)
        {
            StringBuilder scriptTags = CreatePagesScriptsTags(page, namedFormatValues);

            return MvcHtmlString.Create(scriptTags.ToString());
        }

        /// <summary>
        /// Get a script tag that references the /bam/apps/{appName}/init.js script
        /// </summary>
        /// <param name="page"></param>
        /// <param name="namedFormatValues"></param>
        /// <returns></returns>
        public static MvcHtmlString AppInitScript(WebViewPage page, object namedFormatValues)
        {
            StringBuilder scriptTag = CreateAppInitScriptTag(page, namedFormatValues);

            return MvcHtmlString.Create(scriptTag.ToString());
        }

        public static MvcHtmlString DustBamTemplates(WebViewPage page, object namedFormatValues)
        {
            StringBuilder dustBamTemplates = CreateDustBamTemplatesScriptTag(page, namedFormatValues);

            return MvcHtmlString.Create(dustBamTemplates.ToString());
        }

        private static StringBuilder CreateDustBamTemplatesScriptTag(WebViewPage page, object appNameObj)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            StringBuilder templatePath = new StringBuilder();
            Tag script = new Tag("script", new { src = "/dust/bamtemplates?appName={appName}".NamedFormat(appNameObj) });
            templatePath.A(script.ToHtmlString());
            return templatePath;
        }

        private static StringBuilder CreateAppInitScriptTag(WebViewPage page, object appNameObj)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            StringBuilder initPath = new StringBuilder();
            initPath.A(AppRoot).A("init.js").ToString().NamedFormat(appNameObj);

            return CreateScriptTags(url, appNameObj, initPath.ToString());
        }

        private static StringBuilder CreateViewModelsScriptTags(WebViewPage page, object appNameObj)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            string viewModelsDirPath = server.MapPath(AppViewModels.NamedFormat(appNameObj));

            StringBuilder viewModelScripts = new StringBuilder();
            DirectoryInfo viewModelsDir = new DirectoryInfo(viewModelsDirPath);
            if (viewModelsDir.Exists)
            {
                string[] scriptPaths = GetScriptPathsFromDirectory(appNameObj, AppViewModels, viewModelsDir);

                viewModelScripts = CreateScriptTags(url, appNameObj, scriptPaths);
            }
            return viewModelScripts;
        }

        private static StringBuilder CreatePagesScriptsTags(WebViewPage page, object appNameObject)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            string pagesDirPath = server.MapPath(AppPages.NamedFormat(appNameObject));

            StringBuilder pagesScripts = new StringBuilder();
            DirectoryInfo pagesScriptsDir = new DirectoryInfo(pagesDirPath);
            if (pagesScriptsDir.Exists)
            {
                string[] scriptPaths = GetScriptPathsFromDirectory(appNameObject, AppPages, pagesScriptsDir);
                pagesScripts = CreateScriptTags(url, appNameObject, scriptPaths);
            }

            return pagesScripts;
        }

        private static StringBuilder CreateJsScriptsTags(WebViewPage page, object appNameObject)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            string jsDirPath = server.MapPath(AppJs.NamedFormat(appNameObject));

            StringBuilder jsScripts = new StringBuilder();
            DirectoryInfo jsDir = new DirectoryInfo(jsDirPath);
            if (jsDir.Exists)
            {
                string[] scriptPaths = GetScriptPathsFromDirectory(appNameObject, AppJs, jsDir);
                jsScripts = CreateScriptTags(url, appNameObject, scriptPaths);
            }

            return jsScripts;
        }

        private static StringBuilder CreateAppConfigScriptTags(WebViewPage page, object namedFormatValues = null)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            string[] scriptPaths = GetAppConfigScriptPaths(namedFormatValues, server);
            StringBuilder scriptTags = CreateScriptTags(url, namedFormatValues, scriptPaths);

            return scriptTags;
        }

        internal static string[] GetAppConfigScriptPaths(object namedFormatValues, HttpServerUtilityBase server)
        {
            string appRoot = AppRoot.NamedFormat(namedFormatValues);
            string appConfigPath = server.MapPath(string.Format("{0}/config.js", appRoot));
            string[] scriptPaths = GetScriptPathsFromConfigJs(appConfigPath);
            return scriptPaths;
        }

        private static StringBuilder CreateCommonConfigScriptTags(WebViewPage page, object namedFormatValues)
        {
            HttpServerUtilityBase server = page.Context.Server;
            UrlHelper url = page.Url;

            string[] scripts = GetCommonConfigScriptPaths(server);
            StringBuilder scriptTags = CreateScriptTags(url, namedFormatValues, scripts);
            return scriptTags;
        }

        internal static string[] GetCommonConfigScriptPaths(HttpServerUtilityBase server)
        {
            string appsConfigPath = server.MapPath(string.Format("{0}/config.js", AppsRoot));
            string[] scripts = GetScriptPathsFromConfigJs(appsConfigPath);
            return scripts;
        }

        private static StringBuilder CreateScriptTags(UrlHelper url, object namedFormatValues, params string[] scriptPaths)
        {
            StringBuilder scriptTags = new StringBuilder();
            foreach (string script in scriptPaths)
            {
                string scriptPath = script;
                if (scriptPath.StartsWith("~"))
                {
                    scriptPath = url.Content(scriptPath);
                }

                if (namedFormatValues != null)
                {
                    scriptPath = scriptPath.NamedFormat(namedFormatValues);
                }
                Tag scr = new Tag("script", new { src = scriptPath });
                scriptTags.Append(scr.ToHtmlString());
            }
            return scriptTags;
        }

        private static string[] GetScriptPathsFromConfigJs(string configJsPath)
        {
            string[] result = new string[] { };
            if (File.Exists(configJsPath))
            {
                dynamic config = configJsPath.JsonFromJsLiteralFile("config").JsonToDynamic();

                result = ((JArray)config["scripts"]).Select(v => (string)v).ToArray();
            }
            return result;
        }

        internal static string[] GetScriptPathsFromDirectory(object appNameObj, string pathFormat, DirectoryInfo containingDirectory)
        {
            FileInfo[] jsFiles = containingDirectory.GetFiles("*.js");
            string[] scriptPaths = new string[jsFiles.Length];
            int iterator = 0;
            jsFiles.Each((jsFile) =>
            {
                scriptPaths[iterator] = string.Format("{0}/{1}", pathFormat.NamedFormat(appNameObj), jsFile.Name);
                iterator++;
            });
            return scriptPaths;
        }
    }
}
