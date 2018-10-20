using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Yaml;
using System.IO;
using System.Reflection;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Web;
using Bam.Net.Server.Renderers;
using Bam.Net.Presentation.Html;
using Bam.Net.Configuration;
using System.Web.Mvc;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Server.Meta;

namespace Bam.Net.Server
{
    public partial class ServiceProxyResponder
    {

        private bool SendMethodForm(IHttpContext context)
        {
            bool result = false;
            IRequest request = context.Request;
            string appName = ApplicationNameResolver.ResolveApplicationName(context);
            string path = request.Url.AbsolutePath;
            string prefix = MethodFormPrefixFormat._Format(ResponderSignificantName.ToLowerInvariant());
            string partsICareAbout = path.TruncateFront(prefix.Length);
            string[] segments = partsICareAbout.DelimitSplit("/", "\\");

            if (segments.Length == 2)
            {
                GetServiceProxies(appName, out Incubator providers, out List<ProxyAlias> aliases);
                string className = segments[0];
                string methodName = segments[1];
                Type type = providers[className];
                if (type == null)
                {
                    ProxyAlias alias = aliases.FirstOrDefault(a => a.Alias.Equals(className));
                    if (alias != null)
                    {
                        type = providers[alias.ClassName];
                    }
                }

                if (type != null)
                {
                    InputFormBuilder builder = new InputFormBuilder(type);
                    QueryStringParameter[] parameters = request.Url.Query.DelimitSplit("?", "&").ToQueryStringParameters();
                    Dictionary<string, object> defaults = new Dictionary<string, object>();
                    foreach (QueryStringParameter param in parameters)
                    {
                        defaults.Add(param.Name, param.Value);
                    }
                    TagBuilder form = builder.MethodForm(methodName, defaults);
                    LayoutModel layoutModel = GetLayoutModel(appName);
                    layoutModel.PageContent = form.ToMvcHtml().ToString();
                    ContentResponder.CommonTemplateManager.RenderLayout(layoutModel, context.Response.OutputStream);
                    result = true;
                }
            }

            return result;
        }

    }
}
