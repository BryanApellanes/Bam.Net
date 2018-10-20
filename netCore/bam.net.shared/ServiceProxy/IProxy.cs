using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.ServiceProxy
{
    public interface IProxy // referenced by generated proxies **** DO NOT DELETE ****
    {
        Type ProxiedType { get; }
        ServiceProxyClient Client { get; }
        IApiKeyResolver ApiKeyResolver { get; set; }
        IApplicationNameProvider ClientApplicationNameProvider { get; set; }
        void SubscribeToClientEvent(string eventName, EventHandler handler);
    }
}
