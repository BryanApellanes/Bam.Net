using System.IO;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Presentation
{
    public class StaticContentPageRenderer : AppPageRenderer
    {
        public StaticContentPageRenderer(AppContentResponder appContentResponder, ITemplateManager commonTemplateManager) : base(appContentResponder, commonTemplateManager)
        {
            FileExtension = ".html";
        }

        public StaticContentPageRenderer(AppContentResponder appContentResponder, ITemplateManager commonTemplateManager, IApplicationTemplateManager applicationTemplateManager) : base(appContentResponder, commonTemplateManager, applicationTemplateManager)
        {
            FileExtension = ".html";
        }
        
        public override byte[] RenderPage(IRequest request, IResponse response)
        {
            string path = request.Url.AbsolutePath;
            RouteInfo routeInfo = GetRouteInfo(request);
            if (routeInfo.IsHomeRequest)
            {
                if (AppRoot.FileExists(DefaultFilePath, out string locatedPath))
                {
                    return AppContentResponder.GetContent(locatedPath, request, response);
                }
            }
            else 
            {
                string absolutePath = AppRoot.GetAbsolutePath(path);
                if (File.Exists(absolutePath))
                {
                    return AppContentResponder.GetContent(absolutePath, request, response);
                }
            }

            return RenderNotFound(request, response);
        }
    }
}