using System;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net;

namespace Bam.Net.CoreServices
{
    [Proxy("diagSvc")]
    [ApiKeyRequired]
    public class DiagnosticService: ApplicationProxyableService 
    {
        protected DiagnosticService() { } // required for client proxy generation via ProxyFactory
        public DiagnosticService(AppConf conf)
        {
            AppConf = conf;
        }
        [RoleRequired("/", "Admin", "Diagnoser")]
        public virtual DiagnosticInfo GetDiagnosticInfo()
        {
            return new DiagnosticInfo();
        }

        [Exclude]
        public override object Clone()
        {
            DiagnosticService clone = new DiagnosticService(AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
