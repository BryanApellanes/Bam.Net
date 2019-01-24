using Bam.Net.CoreServices;
using Bam.Net.Services.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bam.Net.Web.Models.Rpc
{
    [Proxy("testRpc")]
    public class EchoRpcService : CoreProxyableService
    {
        public override object Clone()
        {
            EchoRpcService clone = new EchoRpcService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
