/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bam.Net;

namespace Bam.Net.OAuth
{
    public abstract class OAuthResult: ActionResult
    {
        public string AuthorizationUrl { get; set; }

        /// <summary>
        /// The pemissions to request.
        /// </summary>
        public string[] Scope { get; set; }

        /// <summary>
        /// The ClientId for the application assigned by the OAuth 
        /// provider.
        /// </summary>
        public QueryStringParameter ClientId { get; set; }
        /// <summary>
        /// The uri that the OAuth provider will redirect to in the current
        /// app after the user logs in/authorizes the app.
        /// </summary>
        public QueryStringParameter RedirectUri { get; set; }

        internal string RequestAuthorizationUrl
        {
            get
            {
                List<QueryStringParameter> qParams = new List<QueryStringParameter>();
                qParams.Add(ClientId);
                qParams.Add(RedirectUri);
                qParams.Add(new QueryStringParameter("scope", Scope.ToDelimited(s => s, ",")));
                return string.Format("{0}{1}", AuthorizationUrl, qParams.ToArray().ToQueryString());
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Redirect(RequestAuthorizationUrl);
        }
    }
}
