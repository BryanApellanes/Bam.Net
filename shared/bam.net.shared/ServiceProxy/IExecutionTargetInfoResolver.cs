using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// When implemented, resolves the class name, method name and extension
    /// the specified context is targeting for execution.  Not to be confused 
    /// with the associated Type and MethodInfo, this method merely identifies
    /// them by name (string) so they can be resolved later by a different
    /// mechanism.
    /// </summary>
    public interface IExecutionTargetInfoResolver
    { 
        ExecutionTargetInfo ResolveExecutionTarget(IHttpContext context);
    }
}
