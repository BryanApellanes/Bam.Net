using System;
using Bam.Net.Analytics;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsAppPageRenderer : AppPageRenderer
    {
        public HandlebarsAppPageRenderer(AppContentResponder appContentResponder, ITemplateManager commonTemplateManager) : base(appContentResponder, commonTemplateManager)
        {
            FileExtension = ".hbs";
        }

        public HandlebarsAppPageRenderer(AppContentResponder appContentResponder, ITemplateManager commonTemplateManager, IApplicationTemplateManager applicationTemplateManager) : base(appContentResponder, commonTemplateManager, applicationTemplateManager)
        {
            FileExtension = ".hbs";
            TemplateRenderer = new HandlebarsTemplateRenderer(AppConf.HtmlDir);
        }

        public HandlebarsTemplateRenderer TemplateRenderer { get; set; }
        
        public override byte[] RenderPage(IRequest request, IResponse response)
        {
            string templateName = GetTemplateName(request);

            if (!string.IsNullOrEmpty(templateName))
            {
                return TemplateRenderer.Render(templateName, CreatePageModel(request)).ToBytes();
            }

            return new byte[] { };
        }

        protected virtual string GetTemplateName(IRequest request)
        {
            RequestInfo requestInfo = GetRequestInfo(request);
            string templateName = string.Empty;
            if (requestInfo.RouteInfo.IsHomeRequest)
            {
                templateName = System.IO.Path.GetFileNameWithoutExtension(DefaultFilePath);
            }
            else if (requestInfo.FileExists(AppConf, out string absolutePath))
            {
                templateName = System.IO.Path.GetFileNameWithoutExtension(absolutePath);
            }

            return templateName;
        }
    }
}