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
    public partial class UserUtil
    {
        /// <summary>
        /// Returns the value at index 1 after splitting the specified name
        /// at the backslash character (\).  If there are no backslash characters the 
        /// full value is returned.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// Determine if the current windows (owner of the current process) user has admin rights.
        /// </summary>
        /// <returns></returns>
        public static bool CurrentWindowsUserHasAdminRights()
        {
            return WindowsIdentity.GetCurrent().HasAdminRights();
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
