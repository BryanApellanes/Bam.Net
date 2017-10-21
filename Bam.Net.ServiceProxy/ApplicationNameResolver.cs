using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Web;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.ServiceProxy
{
    public class ApplicationNameResolver : IApplicationNameResolver
    {
        public ApplicationNameResolver()
        {

        }

        public ApplicationNameResolver(IHttpContext context)
        {
            HttpContext = context;
        }

        public IHttpContext HttpContext { get; set; }

        public string ResolveApplicationName(IHttpContext context)
        {
            return ResolveApplicationName(context, true);
        }

        public string ResolveApplicationName(IHttpContext context, bool withHost)
        {
            string host = context?.Request?.Url?.Host;
            string userHostAddress = context?.Request?.UserHostAddress;
            string fromHeader = context?.Request?.Headers[Headers.ApplicationName];
            string unkown = Application.Unknown.Name;            
            return withHost ? $"{fromHeader.Or(unkown)}@{host.Or("localhost")}({userHostAddress})": fromHeader.Or(unkown);
        }

        public string GetApplicationName()
        {
            return ResolveApplicationName(HttpContext, false);
        }

        public static string Resolve(IHttpContext context)
        {
            return new ApplicationNameResolver(context).GetApplicationName();
        }
    }
}
