/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Security.Principal;

namespace Bam.Net
{
    /// <summary>
    /// Utility for getting information about the current user.
    /// </summary>
    public partial class UserUtil // core
    {
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
        /// Get the username of the current user if HttpContext.Current
        /// is not null and contains user information.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentWebUserName()
        {
            return GetCurrentWebUserName(false);
        }

        /// <summary>
        /// Get the username of the current user if HttpContext.Current
        /// is not null and contains user information.
        /// </summary>
        /// <param name="includeDomain"></param>
        /// <returns></returns>
        public static string GetCurrentWebUserName(bool includeDomain)
        {
            return string.Empty;
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
        /// Gets the domain of the current user.  If running in a web app the 
        /// web user's domain is returned otherwise the domain of the current
        /// windows user is returned.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserDomain()
        {
            string userWithDomain = GetCurrentUser(true);
            return GetDomain(userWithDomain);
        }
    }
}
