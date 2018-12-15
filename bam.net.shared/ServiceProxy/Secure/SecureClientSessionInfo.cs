using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.ServiceProxy.Secure
{
    public class SecureClientSessionInfo
    {
        public ClientSessionInfo ClientSessionInfo { get; set; }
        public Cookie SessionCookie { get; set; }
        public string SessionKey { get; set; }
        public string SessionIV { get; set; }

        public override bool Equals(object obj)
        {
            SecureClientSessionInfo info = obj as SecureClientSessionInfo;
            if(info != null)
            {
                return info.ClientSessionInfo.Equals(ClientSessionInfo) && info.SessionCookie.Equals(SessionCookie) && info.SessionKey.Equals(SessionKey) && info.SessionIV.Equals(SessionIV);
            }
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return $"{ClientSessionInfo.ToString()}::SessionCookie={SessionCookie.ToString()};SessionKey=XXX;SessionIV=XXX";
        }
    }
}
