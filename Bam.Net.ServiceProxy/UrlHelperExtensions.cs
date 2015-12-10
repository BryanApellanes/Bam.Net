/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net.ServiceProxy.Js;

namespace Bam.Net.ServiceProxy
{
    public static class UrlHelperExtensions
    {
        static UrlHelperExtensions()
        {
            ResourceScripts.LoadScripts(typeof(Js.PlaceHolder));
            ServiceProxySystem.Initialize();
        }

        /// <summary>
        /// Create a script tag
        /// </summary>
        /// <param name="helper">The helper being extended</param>
        /// <param name="scriptName">The value to be used by the src attribute of the script tag</param>
        /// <param name="addMinIfDebugDisabled">If true .min will be inserted before the .js extension only
        /// if debugging is disabled</param>
        /// <returns></returns>
        public static MvcHtmlString Script(this UrlHelper helper, string scriptName, bool addMinIfDebugDisabled)
        {
            return helper.Script(scriptName, "~/Scripts/", addMinIfDebugDisabled);
        }

        public static MvcHtmlString Script(this UrlHelper helper, string scriptName, string folder = "~/Scripts/", bool addMinIfDebugDisabled = true)
        {
            if (addMinIfDebugDisabled)
            {
                scriptName = AddMinIfDebuggingDisabled(scriptName);
            }
            string path = helper.Content(Path.Combine(folder, scriptName));
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("src", path);
            builder.MergeAttribute("type", "text/javascript");
            return MvcHtmlString.Create(builder.ToString());
        }
        
        public static MvcHtmlString Css(this UrlHelper helper, string styleSheet, string folder = "~/Content/")
        {
            styleSheet = AddMinIfDebuggingDisabled(styleSheet);
            string path = helper.Content(Path.Combine(folder, styleSheet));
            TagBuilder builder = new TagBuilder("link");
            builder.MergeAttribute("href", path);
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("type", "text/css");
            return MvcHtmlString.Create(builder.ToString());
        }

        private static string AddMinIfDebuggingDisabled(string scriptName)
        {
            if (System.Web.HttpContext.Current.IsDebuggingEnabled)
            {
                return scriptName;
            }

            if (!scriptName.Trim().ToLowerInvariant().EndsWith("min.js"))
            {
                int pos = scriptName.LastIndexOf(".");
                if (pos != -1)
                {
                    scriptName = scriptName.Insert(pos, ".min");
                }
            }

            return scriptName;
        }

        /// <summary>
        /// Register client side FileExt proxies for the specified className
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="className"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static MvcHtmlString Proxy(this UrlHelper helper, string className, bool min = false)
        {
            string minQuery = min ? "?min=true" : "";
            return helper.Script(string.Format("Proxy/{0}{1}", className, minQuery), "~/FileExt/");
        }

        /// <summary>
        /// Register all FileExt proxies
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static MvcHtmlString Proxies(this UrlHelper helper, bool min = false)
        {
            string minQuery = min ? "?min=true" : "";
            return helper.Script(string.Format("Proxies{0}", minQuery), "~/FileExt/");
        }

        /// <summary>
        /// Adds a reference to the FileExt.js script.  And registers all
        /// proxies on the page.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString FileExt(this UrlHelper helper)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(helper.Script("FileExt.js", "~/FileExt/Js/").ToString());
            builder.AppendLine(helper.Proxies().ToString());
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
