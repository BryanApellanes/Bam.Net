/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using System.Net;
using Newtonsoft.Json.Linq;
using Bam.Net;
using System.Security.Principal;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;

namespace Bam.Net.OAuth
{
    public abstract class OAuthController : ServiceProxyController
    {
        public static event EventHandler<JObjectEventArgs> UserSet;
        public static event EventHandler SigningOut;

        const string _defaultRedirectPath = "~/Home/Index";

        public class AccessTokenStore
        {
            public AccessTokenStore() { }
            public string TargetUrl { get; set; }
            public string AccessToken { get; set; }
        }

        static OAuthController()
        {
            
        }

        public OAuthController()
        {
            PostSignOutUrl = _defaultRedirectPath;
        }

        public string PostSignOutUrl
        {
            get;
            set;
        }

        public const string AccessTokenUrl = "https://graph.facebook.com/oauth/access_token?";
        public const string CurrentUserUrl = "https://graph.facebook.com/me?";
        //
        // GET: /Auth/

        public ActionResult Authenticate(string targetController = null, string targetAction = null)
        {
            Providers.SetSessionProviderIfNull<OAuthController.AccessTokenStore>(new OAuthController.AccessTokenStore(), true);

            string controller = targetController ?? "Home";
            string action = targetAction ?? "Index";

            SetTarget(controller, action);
            
            return new FacebookOAuthResult(ControllerContext, FacebookPermissions.Email);
        }

        /// <summary>
        /// Sets the controller and action to direct to after authentication
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        protected virtual void SetTarget(string controller, string action)
        {
            string targetUrl = GetUrl(controller, action);

            SetTargetUrl(targetUrl);
        }

        /// <summary>
        /// Returns the full application url for the specified controller actoin
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected string GetUrl(string controller, string action)
        {
            string appUrl = ControllerContext.RequestContext.HttpContext.ApplicationUrl();
            string targetUrl = string.Format("{0}{1}/{2}",
                appUrl,
                controller,
                action
            );
            return targetUrl;
        }

        protected virtual void SetTargetUrl(string url)
        {
            AccessTokenStore token = Providers.GetSessionProvider<AccessTokenStore>();
            token.TargetUrl = url;
        }

        public ActionResult SignOut()
        {
            Providers.AbandonSession();
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.User = null;

            OnSigningOut();

            return Redirect(PostSignOutUrl);
        }

        public ActionResult SetToken(string code)
        {
            List<QueryStringParameter> qParams = new List<QueryStringParameter>();
            string clientId = ConfigurationManager.AppSettings["FacebookClientId"];
            string clientSecret = ConfigurationManager.AppSettings["FacebookSecret"];

            qParams.Add(new QueryStringParameter("client_id", clientId));
            qParams.Add(new QueryStringParameter("client_secret", clientSecret));
            qParams.Add(new QueryStringParameter("redirect_uri", GetBaseRedirectUri(ControllerContext)));
            qParams.Add(new QueryStringParameter("code", code));

            string url = AccessTokenUrl + qParams.ToArray().ToQueryString();

            using (WebClient client = new WebClient())
            {
                string accessToken = client.DownloadString(url);
                Dictionary<string, string> normal = accessToken.ToDictionary("&", "=");
                Providers.GetSessionProvider<AccessTokenStore>().AccessToken = normal["access_token"];
            }

            SetUser();

            string redirectTo = Providers.GetSessionProvider<AccessTokenStore>().TargetUrl;
            if (string.IsNullOrEmpty(redirectTo))
            {
                redirectTo = _defaultRedirectPath;
            }

            return Redirect(redirectTo);
        }

        private void OnUserSet(JObject user)
        {
            if (UserSet != null)
            {
                UserSet(this, new JObjectEventArgs(user));
            }
        }

        private void OnSigningOut()
        {
            if (SigningOut != null)
            {
                SigningOut(this, EventArgs.Empty);
            }
        }

        private void SetUser()
        {
            using (WebClient client = new WebClient())
            {
                AccessTokenStore store = Providers.GetSessionProvider<AccessTokenStore>();
                QueryStringParameter accessToken = new QueryStringParameter("access_token", store.AccessToken);

                string data = client.DownloadString(string.Format("{0}{1}", CurrentUserUrl, accessToken.ToString()));
                JObject currentUser = JObject.Parse(data);
                currentUser.Add("auth_source", "Facebook");
                currentUser.Add("access_token", store.AccessToken);

                FacebookPrincipal principal = new FacebookPrincipal(currentUser, store.AccessToken);
                System.Web.HttpContext.Current.User = principal;
                Providers.SetSessionProvider<FacebookPrincipal>(principal, true);
                OnUserSet(currentUser);
            }
        }


        internal FacebookPrincipal CurrentUser
        {
            get
            {
                return Providers.GetSessionProvider<FacebookPrincipal>();
            }
        }

        internal static string GetBaseRedirectUri(ControllerContext context)
        {
            string baseUrl = context.HttpContext.ApplicationUrl();

            string redirectPath = "OAuth/SetToken"; // redirect action of Auth controller

            if (!redirectPath.StartsWith("/") && !baseUrl.EndsWith("/"))
            {
                redirectPath = string.Format("/{0}", redirectPath);
            }

            string redirectUri = string.Format("{0}{1}", baseUrl, redirectPath);

            return redirectUri;
        }
    }
}
