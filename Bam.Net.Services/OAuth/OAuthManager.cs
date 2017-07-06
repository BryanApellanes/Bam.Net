/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.Services.OAuth
{
    [Proxy("oauth")]
    public class OAuthManager: ProxyableService
    {
        public OAuthManager(CoreClient client)
        {
            CoreClient = client;
        }

        public CoreClient CoreClient { get; set; }
        public Session Session
        {
            get
            {
                return Session.Get(HttpContext);
            }
        }
        [Exclude]
        public override object Clone()
        {
            OAuthManager clone = new OAuthManager(CoreClient);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public void SetToken(string accessToken)
        {
            Session["accessToken"] = accessToken;            
        }        
    }
}
