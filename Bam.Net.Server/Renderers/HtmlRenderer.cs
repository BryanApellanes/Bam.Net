/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Web;
using Bam.Net.Presentation;
using Bam.Net.Presentation.Html;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using System.Reflection;

namespace Bam.Net.Server.Renderers
{
    public class HtmlRenderer: ContentRenderer
    {
        public HtmlRenderer(ExecutionRequest request, ContentResponder contentResponder)
            : base(request, contentResponder, "text/html", ".htm", ".html")
        {
            this.AppName = UriApplicationNameResolver.ResolveApplicationName(request.Request.Url);
            this.ContentResponder = contentResponder;
            this.ExecutionRequest = request;
        }

        public string AppName { get; set; }

        HttpArgs _args;
        protected internal HttpArgs HttpArgs
        {
            get
            {
                if (_args == null)
                {
                    _args = new HttpArgs(ExecutionRequest.Request.Url.Query);
                }
                return _args;
            }
        }

        public string GetTemplateName(object toRender)
        {
            HttpArgs args = HttpArgs;//new HttpArgs(ExecutionRequest.Request.Url.Query);
            string result;
            args.Has("view", out result);
            if (string.IsNullOrEmpty(result))
            {
                string prefix = string.Empty;
                if (toRender != null)
                {
                    Type typeToRender = toRender.GetType();
                    prefix = "{0}_"._Format(typeToRender.Name);
                    ITemplateRenderer dustRenderer = ContentResponder.AppContentResponders[AppName].AppTemplateRenderer;
                    dustRenderer.EnsureDefaultTemplate(typeToRender);
                }
                AppContentResponder appContentResponder = ContentResponder.AppContentResponders[AppName];
                string domAppName = AppConf.DomApplicationIdFromAppName(appContentResponder.AppConf.Name);

                result = "{0}.{1}default"._Format(domAppName, prefix);
            }

            return result;
        }
        /// <summary>
        /// Render the response to the output stream of ExecutionRequest.Response
        /// </summary>
        public void Render()
        {
            Render(ExecutionRequest.Result, ExecutionRequest.Response.OutputStream);
        }

        public override void Render(object toRender, Stream output)
        {
            AppContentResponder appContentResponder = ContentResponder.AppContentResponders[AppName];
            ITemplateRenderer dustRenderer = appContentResponder.AppTemplateRenderer;
            string templateName = GetTemplateName(toRender);
            string templates = dustRenderer.CombinedCompiledTemplates;            
            string renderedContent = DustScript.Render(templates, templateName, toRender);

            byte[] data;
            if (HttpArgs.Has("layout", out string layout))
            {
                string absolutePath = ExecutionRequest.Request.Url.AbsolutePath;
                string extension = Path.GetExtension(absolutePath);
                string path = absolutePath.Truncate(extension.Length);
                LayoutModel layoutModel = appContentResponder.GetLayoutModelForPath(path);
                layoutModel.LayoutName = layout;
                layoutModel.PageContent = renderedContent;
                MemoryStream ms = new MemoryStream();
                appContentResponder.CommonTemplateRenderer.RenderLayout(layoutModel, ms);
                ms.Seek(0, SeekOrigin.Begin);
                data = ms.GetBuffer();
            }
            else
            {
                data = Encoding.UTF8.GetBytes(renderedContent);
            }
            output.Write(data, 0, data.Length);
        }

       
    }
}
