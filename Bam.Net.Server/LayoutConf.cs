/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using Bam.Net.Server;
using Newtonsoft.Json;
using Bam.Net.Presentation;

namespace Bam.Net.Server
{
    public class LayoutConf
    {
        /// <summary>
        /// Required for deserialization
        /// </summary>
        public LayoutConf() { }

        public LayoutConf(AppConf conf)
        {
            IncludeCommon = true;
			SetConf(conf);
        }

        internal AppConf AppConf
        {
            get;
            set;
        }
        public string Extras { get; set; }
        public string QueryString { get; set; }
        public string LayoutName { get; set; }

        public bool IncludeCommon { get; set; }

		public bool RenderBody { get; set; }

		public void SetConf(AppConf appConf)
		{
			RenderBody = appConf.RenderLayoutBody;
			LayoutName = appConf.DefaultLayout;
			AppConf = appConf;
		}

        public LayoutModel CreateLayoutModel(string[] htmlPathSegments = null)
        {
            LayoutModel model = new LayoutModel()
            {
                ApplicationName = AppConf.Name,
                QueryString = QueryString,
                Extras = string.IsNullOrEmpty(Extras) ? null : JsonConvert.DeserializeObject(Extras),

                LayoutName = LayoutName,
                ApplicationDisplayName = AppConf.DisplayName
            };
            SetIncludes(AppConf, model);

            if (htmlPathSegments != null && RenderBody) 
            {
                SetBody(model, htmlPathSegments);
            }

            return model;
        }

        protected internal void SetIncludes(AppConf conf, LayoutModel layoutModel)
        {
            Includes includes = AppContentResponder.GetAppIncludes(conf);
            if (IncludeCommon)
            {
                Includes commonIncludes = ContentResponder.GetCommonIncludes(conf.BamConf.ContentRoot);
                includes = commonIncludes.Combine(includes);
            }
            // TODO: add a config flag "Debug" based on ProcessMode.Current
            layoutModel.ScriptTags = includes.GetScriptTags().ToHtmlString();
            layoutModel.StyleSheetLinkTags = includes.GetStyleSheetLinkTags().ToHtmlString();
        }

        protected internal void SetBody(LayoutModel layout, string[] pathSegments) 
        {
            Fs appRoot = AppConf.AppRoot;
            if (appRoot.FileExists(pathSegments)) 
            {
                string html = appRoot.ReadAllText(pathSegments);
                CQ dollarSign = CQ.Create(html);
                string body = dollarSign["body"].Html().Replace("\r", "").Replace("\n", "").Replace("\t", "");
                layout.PageContent = body;

                StringBuilder headLinks = new StringBuilder();
                dollarSign["link", dollarSign["head"]].Each(el => 
                {
                    AddLink(headLinks, el);
                });
                StringBuilder links = new StringBuilder(layout.StyleSheetLinkTags);
                links.Append(headLinks.ToString());              
                layout.StyleSheetLinkTags = links.ToString();

                StringBuilder scriptTags = new StringBuilder();
                dollarSign["script", dollarSign["head"]].Each(el =>
                {
                  AddScript(scriptTags, el);
                });
                StringBuilder scripts = new StringBuilder(layout.ScriptTags);
                scripts.Append(scriptTags.ToString());
                layout.ScriptTags = scripts.ToString();
            }
        }

        private void AddLink(StringBuilder headLinks, IDomObject el) 
        {
            CQ cq = CQ.Create(el);
            string relAttr = cq.Attr("rel");
            string typeAttr = cq.Attr("type");
            string hrefAttr = cq.Attr("href");
            var obj = new
            {
                rel = string.IsNullOrEmpty(relAttr) ? "" : "rel=\"{0}\""._Format(relAttr),
                type = string.IsNullOrEmpty(typeAttr) ? "" : "type=\"{0}\""._Format(typeAttr),
                href = string.IsNullOrEmpty(hrefAttr) ? "" : "href=\"{0}\""._Format(hrefAttr)
            };
            headLinks.Append("<link {rel} {type} {href}>".NamedFormat(obj));
        }

        private void AddScript(StringBuilder scriptTags, IDomObject el)
        {
          CQ cq = CQ.Create(el);

          var obj = new
          {
            src = "src=\"{0}\""._Format(cq.Attr("src"))
          };
          scriptTags.Append("<script {src}></script>".NamedFormat(obj));
        }
    }
}
