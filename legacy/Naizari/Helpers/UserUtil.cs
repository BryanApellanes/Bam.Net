/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Security.Principal;

namespace Naizari.Helpers
{
    public class UserUtil
    {
        public static string GetCurrentWebUserName()
        {
            return GetCurrentWebUserName(false);
        }

        public static string GetCurrentWebUserName(bool includeDomain)
        {
            string ret = "";
            if (HttpContext.Current != null &&
                HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity != null &&
                !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                ret = HttpContext.Current.User.Identity.Name;
            }

            if (!includeDomain && !string.IsNullOrEmpty(ret))
                ret = StripDomain(ret);

            return ret;
        }

        public static string StripDomain(string name)
        {
            string[] split = name.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (split.Length > 1)
                    return split[1];
                else
                    return split[0];
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Returns the current user of the application.  If this 
        /// method is called from a web application the current web
        /// user is returned, otherwise the name of the owner of the
        /// current process is returned.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUser()
        {
            return GetCurrentUser(false);
        }

        /// <summary>
        /// Returns the current user of the application.  If this 
        /// method is called from a web application the current web
        /// user is returned, otherwise the name of the owner of the
        /// current process is returned.
        /// </summary>
        /// <param name="includeDomain"></param>
        /// <returns></returns>
        public static string GetCurrentUser(bool includeDomain)
        {
            string user = GetCurrentWebUserName(includeDomain);
            if (string.IsNullOrEmpty(user))
            {
                user = GetCurrentWindowsUser(includeDomain);
            }

            return user;
        }

        /// <summary>
        /// Gets the current Windows user.  This is typically the owner of the currently
        /// running process.
        /// </summary>
        /// <param name="includeDomain">True to return &lt;domain&gt;\&lt;userName&gt;, if false &lt;userName&gt;</param>
        /// <returns>User as string</returns>
        public static string GetCurrentWindowsUser(bool includeDomain)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (includeDomain)
            {
                return identity.Name;
            }
            else
            {
                return StripDomain(identity.Name);
            }
        }

        public static ICredentials GetCurrentCredentials()
        {
            return CredentialCache.DefaultCredentials;
        }

        public static ICredentials GetCurrentNetworkCredentials()
        {
            return CredentialCache.DefaultNetworkCredentials;
        }

        /// <summary>
        /// Gets the domain of the current Windows user.  The current Windows user is 
        /// typically the owner of the currently running process.
        /// </summary>
        /// <returns>The domain of the current Windows user.</returns>
        public static string GetCurrentWindowsUserDomain()
        {
            string userWithDomain = GetCurrentWindowsUser(true);
            return GetDomain(userWithDomain);
        }

        /// <summary>
        /// Gets the domain of the current user.  If running in a web app the 
        /// web user's domain is returned otherwise the domain of the current
        /// windows user is returned.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserDomain()
        {
            //string retVal = "";
            string userWithDomain = GetCurrentUser(true);
            return GetDomain(userWithDomain);
        }

        private static string GetDomain(string userWithDomain)
        {
            string retVal = string.Empty;
            string[] split = userWithDomain.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 0)
                retVal = split[0];

            return retVal;
        }
    }
}
