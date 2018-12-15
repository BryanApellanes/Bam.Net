/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.DirectoryServices;
using System.Collections;
using System.Text;
using System.Collections.Generic;


namespace Naizari.Ldap.ActiveDirectory
{
    public class ADGroupUtil
    {
        //DirectoryEntry searchRoot;
        //DirectoryEntry user;
        //DirectoryEntry[] groups;

        static Dictionary<string, Dictionary<DateTime, string[]>> usersToGroup;

        //public GroupUtil(DirectoryEntry user, DirectoryEntry searchRoot)
        //{
        //    if (searchRoot == null)
        //        throw new ArgumentNullException("searchRoot");

        //    searchRoot = searchRoot;
        //    user=user;
            
        //}

        public static bool IsInGroup(string groupName, string objectName)
        {
            bool ret = false;
            foreach (string member in GetGroupMembers(groupName))
            {
                if (member.ToLowerInvariant().Trim().Equals(objectName.Trim().ToLowerInvariant()))
                {
                    ret = true;
                }
            }
            return ret;
        }

        public static bool GroupExists(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                return false;

            if (groupName.Contains("\\"))
                groupName = groupName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];

            DirectorySearcher gSearch = GetGroupSearcher(groupName);
            return gSearch.FindOne() != null;
        }

                /// <summary>
        /// Create an Active Directory security group.
        /// </summary>
        /// <param name="groupName">The name of the group to be created</param>
        /// <param name="groupDescription">The description of the group to be created</param>
        /// <param name="containerOU">A DirectoryEntry object representing the OU the new group will be created in</param>
        public static DirectoryEntry CreateGroup(string groupName, string groupDescription, DirectoryEntry containerOU)
        {
            return CreateGroup(groupName, groupDescription, containerOU, new Dictionary<int,string>());
        }
        /// <summary>
        /// Create an Active Directory security group.
        /// </summary>
        /// <param name="groupName">The name of the group to be created</param>
        /// <param name="groupDescription">The description of the group to be created</param>
        /// <param name="containerOU">A DirectoryEntry object representing the OU the new group will be created in</param>
        /// <param name="extensionAttributes">A dictionary of extensionAttributes to set on the new group keyed by attribute number.</param>
        public static DirectoryEntry CreateGroup(string groupName, string groupDescription, DirectoryEntry containerOU, Dictionary<int, string> extensionAttributes)
        {
            if (groupName.Contains("\\"))
                groupName = groupName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];

            if (!IsOU(containerOU))
                throw new InvalidOperationException("containerOU must be an Organizational Unit");

            if (extensionAttributes.Keys.Count > 15)
                throw new InvalidOperationException("Extension attributes numbers must be between 1 and 15");

            DirectoryEntry newGroup = containerOU.Children.Add("CN=" + groupName, "group");
            if (!string.IsNullOrEmpty(groupDescription))
                newGroup.Properties["description"].Value = groupDescription;
            newGroup.Properties["sAMAccountName"].Value = groupName;

            foreach (int key in extensionAttributes.Keys)
            {
                if (key < 1 || key > 15)
                    throw new InvalidOperationException("Extension attributes numbers must be between 1 and 15");

                newGroup.Properties["extensionAttribute" + key.ToString()].Value = extensionAttributes[key];
            }


            newGroup.CommitChanges();
            return newGroup;
            /*
             * THIS WORKS
            DirectoryEntry direntChild = (DirectoryEntry)groupOu.Invoke("Create", "Group", "CN=" + groupName);
            // Include the Description
            direntChild.Invoke("Put", "Description", groupDescription);
            // Include the Group Type
            direntChild.Invoke("Put", "groupType", -2147483646);
            // Include the Account Name
            direntChild.Invoke("Put", "sAMAccountName", groupName);
            // set all the information 
            direntChild.Invoke("SetInfo");*/
        }

        public static void DeleteGroup(string groupName, string ouPathWithoutDefaultNamingContext)
        {
            DirectoryEntry OU = ADUtil.GetOU(ouPathWithoutDefaultNamingContext);
            DeleteGroup(groupName, OU);
        }

        public static void DeleteGroup(string groupName, DirectoryEntry containerOU)
        {
            if (groupName.Contains("\\"))
                groupName = groupName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];

            if (!IsOU(containerOU))
                throw new InvalidOperationException("containerOU must be an Organizational Unit");

            DirectoryEntry group = GetGroup(groupName);
            if (group != null)
                containerOU.Children.Remove(group);
        }

        /// <summary>
        /// Determine if the specified DirectoryEntry represents an OrganizationalUnit
        /// </summary>
        /// <param name="ouToTest">The object to test</param>
        /// <returns>True if the object represents an OU, false otherwise</returns>
        public static bool IsOU(DirectoryEntry ouToTest)
        {
            bool isOu = false;
            foreach (object prop in ouToTest.Properties["objectClass"])
            {
                if (prop.ToString().Equals("organizationalUnit"))
                    isOu = true;
            }

            return isOu;
        }

        public static void AddToGroup(string userName, string groupName)
        {
            DirectoryEntry group = GetGroup(groupName);
            AddToGroup(userName, group);
        }

        public static void AddToGroup(string userName, DirectoryEntry groupEntry)
        {
            ADUtil adu = new ADUtil();
            DirectoryEntry user = adu.GetUserDirectoryEntry(userName);
            AddToGroup(user, groupEntry);
        }

        public static void AddToGroup(DirectoryEntry objectToAdd, DirectoryEntry groupToAddTo)
        {
            groupToAddTo.Properties["member"].Add(objectToAdd.Properties["distinguishedName"].Value.ToString());
            groupToAddTo.CommitChanges();
        }

        public static void RemoveFromGroup(string userName, string groupName)
        {
            DirectoryEntry groupToRemoveFrom = GetGroup(groupName);
            RemoveFromGroup(userName, groupToRemoveFrom);
        }

        public static void RemoveFromGroup(string userName, DirectoryEntry groupToRemoveFrom)
        {
            ADUtil adu = new ADUtil();
            DirectoryEntry user = adu.GetUserDirectoryEntry(userName);
            RemoveFromGroup(user, groupToRemoveFrom);
        }

        public static void RemoveFromGroup(DirectoryEntry objectToRemove, DirectoryEntry groupToRemoveFrom)
        {
            groupToRemoveFrom.Properties["member"].Remove(objectToRemove.Properties["distinguishedName"].Value.ToString());
            groupToRemoveFrom.CommitChanges();
        }
        /// <summary>
        /// Get a DirectoryEntry object representing the specified OU
        /// </summary>
        /// <param name="ouPathWithoutDefaultNamingContext">The OU path without the default naming context appended. Example:  "ou=Software, ou=Groups"</param>
        /// <returns>DirectoryEntry of type</returns>
        public static DirectoryEntry GetOU(string ouPathWithoutDefaultNamingContext)
        {
            return ADUtil.GetOU(ouPathWithoutDefaultNamingContext);
        }

        public static DirectoryEntry GetOU(string ouPathWithoutDefaultNamingContext, string defaultNamingContext)
        {
            return ADUtil.GetOU(ouPathWithoutDefaultNamingContext, defaultNamingContext);
        }

        public static DirectoryEntry GetGroup(string groupName)
        {
            return GetGroup(groupName, null);
        }

        public static DirectoryEntry GetGroup(string groupName, DirectoryEntry rootOU)
        {
            groupName = groupName.Replace("*", "");
            DirectorySearcher adSearcher = GetGroupSearcher(groupName, rootOU);
            SearchResult result = adSearcher.FindOne();
            if (result != null)
                return result.GetDirectoryEntry();
            else
                return null;
        }

        public static SearchResultCollection FindGroups(string groupSearchPrefix)
        {
            return FindGroupsByPrefix(groupSearchPrefix, null);
        }

        public static SearchResultCollection FindGroupsByWildCard(string groupSearchWildCard, DirectoryEntry searchRootOU)
        {
            DirectorySearcher adSearcher = GetGroupSearcher("*" + groupSearchWildCard + "*", searchRootOU);
            SearchResultCollection searchResults = adSearcher.FindAll();
            return searchResults;
        }

        /// <summary>
        /// Finds groups whose name (sAMAccountName) starts with the specified text.
        /// </summary>
        /// <param name="groupSearchPrefix">The text to search for</param>
        /// <param name="searchRootOU">A DirectoryEntry representing the OU to start searching in</param>
        /// <returns>SearchResultCollection</returns>
        public static SearchResultCollection FindGroupsByPrefix(string groupSearchPrefix, DirectoryEntry searchRootOU)
        {
            DirectorySearcher adSearcher = GetGroupSearcher(groupSearchPrefix + "*", searchRootOU);
            SearchResultCollection searchResults = adSearcher.FindAll();
            return searchResults;
        }

        public static string[] GetGroupMembers(string groupName)
        {
            //ADUtil ad = new ADUtil();
            DirectorySearcher adSearcher = GetGroupSearcher(groupName);
            SearchResult result = adSearcher.FindOne();
            if (result == null)
                return new string[] { };

            GroupExpander g = new GroupExpander(result.GetDirectoryEntry());
            adSearcher.Dispose();
            return g.Members;
        }

        public static string[] GetGroupMembers(DirectoryEntry groupEntry)
        {
            GroupExpander g = new GroupExpander(groupEntry);
            return g.Members;
        }

        private static DirectorySearcher GetGroupSearcher(string groupName)
        {
            return GetGroupSearcher(groupName, null);
        }

        private static DirectorySearcher GetGroupSearcher(string groupName, DirectoryEntry searchRootOU)
        {
            DirectorySearcher adSearcher = null;
            if (searchRootOU != null && IsOU(searchRootOU))
                adSearcher = new DirectorySearcher(searchRootOU);
            else
                adSearcher = new DirectorySearcher();//ADUtil.GetDefaultNamingContext());
            adSearcher.Filter = "(&(objectCategory=group)(sAMAccountName=" + groupName + "))";
            return adSearcher;
        }

        public static string[] GetUsersGroups(string userName)
        {
            return GetUsersGroups(userName, false);
        }

        public static string[] GetUsersGroups(string userName, bool forceRecache)
        {
            ADUtil ad = new ADUtil();
            return GetUsersGroups(ad.GetUserDirectoryEntry(userName, null), forceRecache);
        }

        public static string[] GetUsersGroups(DirectoryEntry user, bool forceRecache)
        {
            if (user == null)
                return new string[] { };

            string userName = user.Properties["cn"].Value.ToString();
            if (usersToGroup == null)
            {
                usersToGroup = new Dictionary<string, Dictionary<DateTime, string[]>>();
            }

            if (usersToGroup.ContainsKey(userName) && !forceRecache)
            {
                foreach (DateTime key in usersToGroup[userName].Keys)
                {
                    if(DateTime.Now.Subtract(new TimeSpan(1,0,0,0)) < key)
                    {
                        return usersToGroup[userName][key];
                    }
                }
            }

            usersToGroup[userName] = new Dictionary<DateTime,string[]>();

            StringBuilder sb = new StringBuilder();

            //we are building an '|' clause
            sb.Append("(|");

            //ADUtil ad = new ADUtil();
            //DirectoryEntry user = ad.GetUserDirectoryEntry(userName, new string[] { "name" });
            //we must ask for this one first
            user.RefreshCache(new string[] { "tokenGroups" });

            foreach (byte[] sid in user.Properties["tokenGroups"])
            {
                //append each member into the filter
                sb.AppendFormat("(objectSid={0})", BuildOctetString(sid));
            }


            //end our initial filter
            sb.Append(")");

            //now create and pull in one search
            string[] groups = null;
            DirectorySearcher ds = new DirectorySearcher();
            ds.Filter = sb.ToString();
            ds.PropertiesToLoad.Add("distinguishedName");
            ds.PropertiesToLoad.Add("objectSid");
            //using (DirectorySearcher ds = new DirectorySearcher(new DirectoryEntry(ADUtil.GetDefaultNamingContext()), sb.ToString(), new string[] { "distinguishedName", "objectSid" }))
            //{
                using (SearchResultCollection src = ds.FindAll())
                {
                    groups = new string[src.Count];

                    for (int i = 0; i < src.Count; i++)
                    {
                        //DirectoryEntryUtility  entryUtility =new DirectoryEntryUtility(src[i].GetDirectoryEntry());
                        groups[i] = src[i].GetDirectoryEntry().Name.Replace("CN=", "");//new DirectoryGroup(
                        //              entryUtility.Name,
                        //           entryUtility.GetOctetStringValue("objectSid"));

                    }

                    usersToGroup[userName][DateTime.Now] = groups;
                }
            //}

            return groups;
        }

        private static string BuildOctetString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.AppendFormat("\\{0}", bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}