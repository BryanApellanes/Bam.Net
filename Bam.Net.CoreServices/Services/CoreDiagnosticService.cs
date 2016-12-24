using System;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net;

namespace Bam.Net.CoreServices
{
    [Proxy("diagSvc")]
    [ApiKeyRequired]
    [RoleRequired("Diagnoser")]
    public class CoreDiagnosticService: ProxyableService
    {
        protected CoreDiagnosticService() { } // required for client proxy generation via ProxyFactory
        public CoreDiagnosticService(AppConf conf)
        {
            AppConf = conf;
        }
        public virtual DiagnosticInfo GetDiagnosticInfo()
        {
            return new DiagnosticInfo();
        }

        [Exclude]
        public override object Clone()
        {
            CoreDiagnosticService clone = new CoreDiagnosticService(AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
