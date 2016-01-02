/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

namespace Naizari.Ldap
{
    public class EntryUtil
    {
        public static string GetProperty(DirectoryEntry entry, string propertyName)
        {
            return GetPropertyString(entry, propertyName);
        }

        public static string GetPropertyString(DirectoryEntry entry, string propertyName)
        {
            object prop = GetPropertyObject(entry, propertyName);
            return prop == null ? string.Empty: prop.ToString();
        }

        public static object GetPropertyObject(DirectoryEntry entry, string propertyName)
        {
            if (entry != null && !string.IsNullOrEmpty(propertyName) &&
                entry.Properties != null &&
                entry.Properties[propertyName] != null &&
                entry.Properties[propertyName].Value != null)
            {
                return entry.Properties[propertyName].Value;
            }
            return null;
        }

        public static Type GetPropertyType(DirectoryEntry entry, string propertyName)
        {
            if( entry != null && !string.IsNullOrEmpty(propertyName) &&
                entry.Properties != null)
                return entry.Properties[propertyName] == null ? typeof(object) : entry.Properties[propertyName].Value.GetType();
            return null;
        }

        public static bool SetProperty(DirectoryEntry entry, string propertyName, object value)
        {
            Exception ex;
            return SetProperty(entry, propertyName, value, true, out ex);
        }

        public static bool SetProperty(DirectoryEntry entry, string propertyName, object value, bool commit, out Exception ex)
        {
            try
            {
                entry.Properties[propertyName].Value = value;
                if (commit)
                {
                    entry.CommitChanges();
                }
            }
            catch (Exception thrown)
            {
                ex = thrown;
                return false;
            }
            ex = null;
            return true;
        }

        public static object GetObjectProperty(DirectoryEntry entry, string propertyName)
        {
            if (entry != null && !string.IsNullOrEmpty(propertyName))
                return entry.Properties[propertyName] == null ? null : entry.Properties[propertyName].Value;
            return null;
        }

        public static string GetLdapUserFilter(string userName)
        {
            return "(&(objectclass=person)(uid=" + userName + "))";
        }

        public static string GetADUserFilter(string userName)
        {
            return "(&(objectCategory=user)(sAMAccountName=" + userName + "))";
        }

        public static string GetADComputerFilter(string computerName)
        {
            return "(&(objectCategory=computer)(cn=" + computerName + "))";
        }

        public DirectoryEntry GetADDirectoryEntry(string[] propertiesToLoad, string filter)
        {
            return GetADDirectoryEntry(propertiesToLoad, filter, null);
        }

        public DirectoryEntry GetADDirectoryEntry(string[] propertiesToLoad, string filter, DirectoryEntry searchRoot)
        {
            DirectorySearcher dirSearcher;
            if (searchRoot == null)
                dirSearcher = new DirectorySearcher();
            else
                dirSearcher = new DirectorySearcher(searchRoot);
            
            AddPropertiesToLoad(propertiesToLoad, dirSearcher);

            dirSearcher.Filter = filter;//EntryUtil.GetLdapUserFilter(username);
            return GetDirectoryEntry(dirSearcher);
        }

        private static DirectoryEntry GetDirectoryEntry(DirectorySearcher dirSearcher)
        {
            SearchResultCollection results = dirSearcher.FindAll();
            if (results.Count == 1)
            {
                foreach (SearchResult result in results)
                {
                    return result.GetDirectoryEntry();
                }
            }

            return null;
        }

        private static void AddPropertiesToLoad(string[] propertiesToLoad, DirectorySearcher dirSearcher)
        {
            if (propertiesToLoad != null)
            {
                foreach (string prop in propertiesToLoad)
                {
                    dirSearcher.PropertiesToLoad.Add(prop);
                }
            }
        }

        public DirectoryEntry GetLdapDirectoryEntry(string[] propertiesToLoad, string searchRoot, string filter)
        {
            DirectoryEntry searchRootEntry = new DirectoryEntry(searchRoot, string.Empty, string.Empty, AuthenticationTypes.None);
            DirectorySearcher searcher = new DirectorySearcher(searchRootEntry);
            searcher.Filter = filter;
            AddPropertiesToLoad(propertiesToLoad, searcher);

            return GetDirectoryEntry(searcher);
        }
    }
}
