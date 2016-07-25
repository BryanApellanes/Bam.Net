using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices.Proxyables
{
    [Encrypt]
    [Proxy("diag")]
    [ApiKeyRequired]
    public class DiagnosticService
    {
        public DiagnosticInfo GetDiagnosticInfo()
        {
            return new DiagnosticInfo();
        } 
    }
}