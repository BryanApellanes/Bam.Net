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
        public ExecutionRequestResolver(Incubator serviceContainer)
        {

        }
        public Incubator ServiceContainer { get; set; }

        public virtual ExecutionRequest CreateExecutionRequest(IHttpContext httpContext, string appName)
        {
            throw new NotImplementedException();
        }        
    }
}
