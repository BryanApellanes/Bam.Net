using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ApplicationServices
{
    [Encrypt]
    [Proxy("log")]
    [ApiKeyRequired]
    public class DiagnosticService
    {
        public DiagnosticInfo GetDiagnosticInfo()
        {
            return new DiagnosticInfo();
        } 
    }
}
