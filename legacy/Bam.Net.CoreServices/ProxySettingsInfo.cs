using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ProxySettingsInfo
    {
        public Protocols Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ServiceType { get; set; }
        public ProxySettings ToProxySettings()
        {
            ProxySettings result = new ProxySettings();
            result.CopyProperties(this);
            result.ServiceType = Type.GetType(ServiceType);
            Args.ThrowIf<InvalidOperationException>(result.ServiceType == null, "ServiceType: ({0}) was not found", ServiceType);
            return result;
        }
    }
}
