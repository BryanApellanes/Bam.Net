using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ApplicationRegistration
{
    public class HostDomainAuthorizer : Authorizer
    {
        public override bool Authorize(ExecutionRequest request)
        {
            ApplicationProxyableService svc = request.Instance as ApplicationProxyableService;
            return svc.HostDomainIsAuthorized();
        }
    }
}
