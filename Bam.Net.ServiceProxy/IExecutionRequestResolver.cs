using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public interface IExecutionRequestResolver: IExecutionTargetInfoResolver
    {
        ExecutionRequest ResolveExecutionRequest(IHttpContext httpContext, string appName);
    }
}
