/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Configuration;
using Microsoft.Web.WebPages.OAuth;
using BAMvvm.Models;

namespace BAMvvm
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: DefaultConfiguration.GetAppSetting("FacebookClientId"),
            //    appSecret: DefaultConfiguration.GetAppSetting("FacebookSecret"));

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
