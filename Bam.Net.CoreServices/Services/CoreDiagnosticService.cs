using System;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    [Proxy("diagSvc")]
    [ApiKeyRequired]
    public class CoreDiagnosticService: ProxyableService
    {
        public virtual DiagnosticInfo GetDiagnosticInfo()
        {
            return new DiagnosticInfo();
        }

        [Exclude]
        public override object Clone()
        {
            CoreDiagnosticService clone = new CoreDiagnosticService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
