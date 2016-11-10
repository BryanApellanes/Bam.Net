using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    public static class CoreExtensions
    {
        public static SecureClientSessionInfo GetSecureClientSessionInfo<T>(this SecureServiceProxyClient<T> client)
        {
            return new SecureClientSessionInfo
            {
                ClientSessionInfo = client.SessionInfo,
                SessionCookie = client.SessionCookie,
                SessionKey = client.SessionKey,
                SessionIV = client.SessionIV
            };
        }
    }
}
