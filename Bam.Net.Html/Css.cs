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

namespace Bam.Net.Html
{
    public class Css
    {
        /// <summary>
        /// Get a link tag (stylesheet reference) with the specified href
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        public Tag Inc(string href)
        {
            return Link(href);
        }

        /// <summary>
        /// Get a link tag (stylesheet reference) with the specified href
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        public Tag Link(string href)
        {
            return new Tag("link", new { rel = "stylesheet", href = href }).Type("text/css");
        }         
    }
}
