using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Use this attribute to require that requests are
    /// explicitly allowed for the hostname/domain that the current
    /// request is for (http://[HOSTorDOMAIN].com) as determined by
    /// a HostDomain entry for the current application
    /// </summary>
    public class HostDomainAuthorizedAttribute : RequestFilterAttribute
    {
        public override bool RequestIsAllowed(ExecutionRequest request, out string failureMessage)
        {
            Args.ThrowIfNull(request, "request");
            Args.ThrowIfNull(request.Instance, "request.Instance");
            bool result = false;
            failureMessage = null;
            if (request.Instance is ApplicationProxyableService svc)
            {
                result = svc.HostDomainIsAuthorized();
            }
            else
            {
                request.Logger.Warning($"Invalid Type {request.Instance.GetType().Name}: Attribute {nameof(HostDomainAuthorizedAttribute)} should only be applied to ApplicationProxyableService and its derivatives or methods");
            }
            return result;
        }
    }
}
