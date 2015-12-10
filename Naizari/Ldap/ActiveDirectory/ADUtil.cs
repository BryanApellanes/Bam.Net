/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

namespace Naizari.Ldap.ActiveDirectory
{
    public class ADUtil
    {
        static string defaultNamingContext;

        public static string GetDefaultNamingContext()
        {
            if (!string.IsNullOrEmpty(defaultNamingContext))
                return defaultNamingContext;
            else
            {
                DirectoryEntry root = new DirectoryEntry("LDAP://rootDSE");
                defaultNamingContext = root.Properties["defaultNamingContext"].Value.ToString();
                return defaultNamingContext;
            }
        }

        /// <summary>
        /// Used to change the default naming context queried by this component.
        /// </summary>
        /// <param name="defaultNamingContext"></param>
        public static void SetDefaultNamingContext(string setContextTo)
        {
            defaultNamingContext = setContextTo;
        }

        public DirectoryEntry GetUserDirectoryEntry(string userName)
        {
            return this.GetUserDirectoryEntry(userName, null);
        }

        public DirectoryEntry GetUserDirectoryEntry(string userName, string[] propertiesToLoad)
        {
            return GetUserDirectoryEntry(userName, propertiesToLoad, null);
        }

        public DirectoryEntry GetUserDirectoryEntry(string userName, string[] propertiesToLoad, DirectoryEntry searchRoot)
        {
            EntryUtil eu = new EntryUtil();
            return eu.GetADDirectoryEntry(propertiesToLoad, EntryUtil.GetADUserFilter(userName), searchRoot);
        }

        public static DirectoryEntry GetOU(string ouPathWithoutDefaultNamingContext)
        {
            return GetOU(ouPathWithoutDefaultNamingContext, GetDefaultNamingContext());
        }

        public static DirectoryEntry GetOU(string ouPathWithoutDefaultNamingContext, string userName, string password)
        {
            return GetOU(ouPathWithoutDefaultNamingContext, GetDefaultNamingContext(), userName, password);
        }

        public static DirectoryEntry GetOU(string ouPathWithoutDefaultNamingContext, string namingContext)
        {
            return GetOU(ouPathWithoutDefaultNamingContext, namingContext, null, null);
        }

        public static DirectoryEntry GetOU(string ouPathWithoutDefaultNamingContext, string namingContext, string userName, string password)
        {
            return new DirectoryEntry("LDAP://" + ouPathWithoutDefaultNamingContext + "," + namingContext, userName, password, AuthenticationTypes.Secure);
        }

        public DirectoryEntry GetComputerDirectoryEntry(string computerName)
        {
            return this.GetComputerDirectoryEntry(computerName, null);
        }

        public DirectoryEntry GetComputerDirectoryEntry(string computerName, string[] propertiesToLoad)
        {
            EntryUtil eu = new EntryUtil();
            return eu.GetADDirectoryEntry(propertiesToLoad, EntryUtil.GetADComputerFilter(computerName));
        }
    }
}
