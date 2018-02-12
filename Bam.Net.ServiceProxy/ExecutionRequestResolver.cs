using Bam.Net.Incubation;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public class ExecutionRequestResolver: IExecutionRequestResolver
    {
        public ExecutionRequestResolver(IHttpContext context, Incubator serviceProvider, params ProxyAlias[] aliases)
        {
            ServiceProvider = serviceProvider;
            HttpContext = context;
            ProxyAliases = aliases;
        }

        public Incubator ServiceProvider { get; set; }
        public IHttpContext HttpContext { get; set; }
        public ProxyAlias[] ProxyAliases { get; set; }
        public IApplicationNameResolver ApplicationNameResolver { get; set; }
        public virtual ExecutionRequest ResolveExecutionRequest()
        {
            return ResolveExecutionRequest(HttpContext, ApplicationNameResolver.ResolveApplicationName(HttpContext));
        }

        public virtual ExecutionRequest ResolveExecutionRequest(IHttpContext httpContext, string appName)
        {
            HttpArgs args = new HttpArgs(httpContext.Request.InputStream.ReadToEnd(), httpContext.Request.ContentType);
            throw new NotImplementedException();
        }   
        
        public virtual ExecutionTargetInfo ResolveExecutionTarget(IHttpContext context)
        {
            return ExecutionTargetInfo.ResolveExecutionTarget(context.Request.Url.AbsolutePath, ServiceProvider, ProxyAliases);
        }

        
    }
}
