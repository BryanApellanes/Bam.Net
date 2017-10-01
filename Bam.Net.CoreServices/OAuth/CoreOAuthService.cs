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
using Bam.Net.Server;

namespace Bam.Net.CoreServices
{
    [Proxy("oauthSvc")]
    [ServiceSubdomain("oauth")]
    public class CoreOAuthService: ApplicationProxyableService // This is not fully implemented
    {
        [Exclude]
        public override object Clone()
        {
            CoreOAuthService clone = new CoreOAuthService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public virtual void SetToken(string accessToken)
        {
            Session["accessToken"] = accessToken;            
        }        
    }
}
