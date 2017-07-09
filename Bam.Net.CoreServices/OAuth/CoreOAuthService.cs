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

namespace Bam.Net.CoreServices
{
    [Proxy("oauth")]
    public class CoreOAuthService: CoreProxyableService
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

        public void SetToken(string accessToken)
        {
            Session["accessToken"] = accessToken;            
        }        
    }
}
