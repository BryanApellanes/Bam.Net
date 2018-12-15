/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Mvc;

namespace Bam.Net.OAuth
{
    public class FacebookOAuthResult: OAuthResult
    {
        public FacebookOAuthResult(ControllerContext context, params string[] permissions)
        {
            this.AuthorizationUrl = "https://graph.facebook.com/oauth/authorize?";

            string redirectUri = OAuthController.GetBaseRedirectUri(context);
            string clientId = ConfigurationManager.AppSettings["FacebookClientId"];

            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentNullException("FacebookClientId was not found in the config file");
            }

            ClientId = new QueryStringParameter("client_id", clientId);
            RedirectUri = new QueryStringParameter("redirect_uri", redirectUri);
            Scope = permissions;            
        }


    }
}
