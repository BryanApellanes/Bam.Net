/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Bam.Net.OAuth
{
    public class FacebookIdentity : IIdentity
    {
        public static implicit operator FacebookIdentity(JObject jsonUser)
        {
            return new FacebookIdentity(jsonUser, jsonUser["access_token"].ToString());
        }

        public FacebookIdentity() { }

        public FacebookIdentity(JObject jsonUser, string accessToken)
        {
            if (jsonUser["name"] != null)
            {
                this.Name = jsonUser["name"].ToString().Replace("\"", "");
            }
            if (jsonUser["id"] != null)
            {
                this.Id = jsonUser["id"].ToString().Replace("\"", "");
            }

            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Maps to the source id (currently the Facebook id property)
        /// </summary>
        public string Id
        {
            get;
            internal set;
        }
        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "Facebook"; }
        }

        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(AccessToken);
            }
        }

        public string AccessToken
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        
        #endregion
    }

    public class FacebookPrincipal: IPrincipal
    {
        IIdentity identity;

        public static implicit operator FacebookPrincipal(JObject userJson)
        {
            return new FacebookPrincipal(userJson, userJson["access_token"].ToString());
        }

        public FacebookPrincipal() { }

        public FacebookPrincipal(JObject userJson, string accessToken)
        {
            this.UserJson = userJson;
            this.identity = new FacebookIdentity(userJson, accessToken);
        }

        public List<string> Roles { get; set; }

        public JObject UserJson { get; set; }

        #region IPrincipal Members

        
        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            return this.Roles.Contains(role);
        }

        #endregion
    }
}
