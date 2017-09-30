using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Web;

namespace Bam.Net.CoreServices
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
        public string GetApplicationName(IHttpContext context)
        {
            return GetApplicationName(context, true);
        }
        public string GetApplicationName(IHttpContext context, bool withHost)
        {
            string host = context?.Request?.Url?.Host;
            string fromHeader = context?.Request?.Headers[Headers.ApplicationName];
            return withHost ? $"{fromHeader.Or($"{Headers.ApplicationName}-Not-Specified")}@{host.Or("localhost")}": fromHeader.Or($"{Headers.ApplicationName}-Not-Specified");
        }

        public string GetApplicationName()
        {
            return GetApplicationName(HttpContext, false);
        }
    }
}
