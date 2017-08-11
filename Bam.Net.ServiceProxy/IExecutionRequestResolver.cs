using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public interface IExecutionRequestResolver
    {
        ExecutionRequest CreateExecutionRequest(IHttpContext httpContext, string appName);
    }
}
