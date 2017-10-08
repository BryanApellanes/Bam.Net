using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class HostDomainValidator : Validator
    {
        public override bool RequestIsValid(ExecutionRequest request, out string failureMessage)
        {
            bool result = false;
            failureMessage = null;
            if (request.Instance is ApplicationProxyableService svc)
            {
                result = svc.HostDomainIsAuthorized();
            }
            else
            {
                Logger.Warning($"Invalid Type {request.Instance.GetType().Name}: Attribute {nameof(HostDomainValidator)} should only be applied to ApplicationProxyableService and its derivatives or methods");
            }
            return result;
        }
    }
}
