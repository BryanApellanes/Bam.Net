/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    [Proxy("oauth")]
    public class OAuthManager: IRequiresHttpContext
    {
        public Session Session
        {
            get
            {
                return Session.Get(HttpContext);
            }
        }
        [Exclude]
        public object Clone()
        {
            OAuthManager clone = new OAuthManager();
            clone.CopyProperties(this);
            return clone;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public PageResult SetToken(string accessToken)
        {
            Session["accessToken"] = accessToken;
            return new PageResult("TokenSet");
        }
    }
}
