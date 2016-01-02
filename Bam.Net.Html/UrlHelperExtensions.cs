/*
	Copyright © Bryan Apellanes 2015  
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Routing;
using Bam.Net.Js;
using Bam.Net.Html.Js;
using Bam.Net;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;

namespace Bam.Net.Html
{
    public static class UrlHelperExtensions
    {
        static UrlHelperExtensions()
        {
            RouteTable.Routes.MapRoute(
                "FileExtHtmlScripts",
                "{controller}/Js/Html/{scriptName}",
                new { controller = "ServiceProxy", action = "ServiceProxyJsResource" }
            );
        }

        /// <summary>
        /// Add a reference to the DataSet.js resource script.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        [Obsolete("Add the dataset.js file using a script tag and not this method")]
        public static MvcHtmlString ServiceProxyDataSet(this UrlHelper helper)
        {
            ResourceScripts.LoadScripts(typeof(DataSet));
            return GetScriptTag(helper, typeof(DataSet).Name);
        }

        private static MvcHtmlString GetScriptTag(UrlHelper helper, string scriptName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(helper.Script(string.Format("{0}.js", scriptName), "~/ServiceProxy/Js/Html/").ToString());
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString Image(this UrlHelper helper, string path, object htmlAttributes = null)
        {
            return new TagBuilder("img")
                .Attr("src", helper.Content(path))
                .Attrs(htmlAttributes).ToHtml();
        }
    }
}
