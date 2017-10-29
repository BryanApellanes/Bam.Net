/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bam.Net.Presentation
{
    /// <summary>
    /// Model used to write the default application layout
    /// for an application
    /// </summary>
    public class LayoutModel
    {
        public LayoutModel()
        {
            this.LayoutName = "basic";
            this.StartPage = "home";
            this.PageContent = string.Empty;
            this.StyleSheetLinkTags = string.Empty;
            this.ScriptTags = string.Empty;
        }
        public object Extras { get; set; }
		public string QueryString { get; set; }
        public string StartPage { get; set; }

        public string LayoutName { get; set; }

        public string ScriptTags { get; set; }

        public string StyleSheetLinkTags { get; set; }

        public string ApplicationDisplayName { get; set; }

        public string ApplicationName
        {
            get;
            set;
        }

        public string PageContent { get; set; }

        /// <summary>
        /// Used to sanitize the app name
        /// </summary>
        public string DomApplicationId { get; set; }

        public string Year
        {
            get
            {
                return DateTime.UtcNow.Year.ToString();
            }
        }
    }
}
