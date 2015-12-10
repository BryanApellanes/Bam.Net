/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Naizari.Ldap.ActiveDirectory
{
    public class ADUser: IADUser
    {
        private string firstName;
        private string lastName;
        private string userId;
        private bool hasExchangeMailBox;
        //private bool isInValhalla;
        //private bool isInSafeHouse;

        private Dictionary<string, string> groups;
        DirectoryEntry userEntry;

        public ADUser(string userId)
            : this(userId, null)
        { }

        public ADUser(string userId, DirectoryEntry searchRoot)
        {
            this.userId = userId;
            ADUtil ad = new ADUtil();
            this.userEntry = ad.GetUserDirectoryEntry(userId, new string[] { "givenName", "sn", "maxPwdAge", "pwdLastSet" }, searchRoot);
            this.firstName = EntryUtil.GetProperty(userEntry, "givenName");
            this.lastName = EntryUtil.GetProperty(userEntry, "sn");
            this.mailBoxLocation = EntryUtil.GetProperty(userEntry, "HomeMDB");
            this.hasExchangeMailBox = !string.IsNullOrEmpty(this.mailBoxLocation);
            this.emailAddress = EntryUtil.GetProperty(userEntry, "mail");
        }

        public static bool PasswordIsValid(string fqdn, string userName, string password)//, out DirectoryEntry returnEntry)
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + fqdn, userName, password, AuthenticationTypes.Secure);
                object test = entry.NativeObject;
                
                return true;
            }
            catch// (Exception ex)
            {
                return false;
            }            
        }

        /// <summary>
        /// Returns true if the specified user is found in the default Active Directory for the 
        /// current process.
        /// </summary>
        /// <param name="userId">The userId to find.</param>
        /// <returns>True if the user is found false otherwise.</returns>
        public static bool Exists(string userId)
        {
            ADUtil ad = new ADUtil();
            return ad.GetUserDirectoryEntry(userId) != null;
        }

        private void initGroups()
        {
            this.groups = new Dictionary<string, string>();
            foreach (string group in ADGroupUtil.GetUsersGroups(this.userId))
            {
                groups.Add(group.ToLowerInvariant(), userId);
            }
        }

        public string[] Groups
        {
            get
            {
                if (this.groups == null)
                    this.initGroups();
                string[] ret = new string[this.groups.Keys.Count];
                this.groups.Keys.CopyTo(ret, 0);
                return ret;
            }
        }

        private string mailBoxLocation;

        public string MailBoxLocation
        {
            get { return mailBoxLocation; }
        }

        public bool HasExchangeMailBox
        {
            get { return hasExchangeMailBox; }
        }

        string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
        }
        //public static PasswordChangeResult ChangePassword(string userName, string oldPassword, string newPassword)
        //{
        //    return ChangePassword(string.Empty, userName, oldPassword, newPassword);
        //}

        public static PasswordChangeResult ChangePassword(string fqdn, string userName, string oldPassword, string newPassword)
        {
            PasswordChangeResult returnValue = new PasswordChangeResult();

            //if (string.IsNullOrEmpty(fqdn))
            //    throw new ArgumentNullException("fqdn (fully qualified domain name)");

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            if (string.IsNullOrEmpty(oldPassword))
                throw new ArgumentNullException("oldPassword");

            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException("newPassword");

            returnValue.UserName = userName;//domainSlashUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];
            returnValue.Domain = fqdn;//domainSlashUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[0];

            try
            {
                DirectoryEntry searchRoot = new DirectoryEntry("LDAP://" + fqdn, userName, oldPassword, AuthenticationTypes.Secure);
                ADUser u = new ADUser(userName, searchRoot);

                //DirectoryEntry entry = new DirectoryEntry(u.DirectoryEntry.Path, userName, oldPassword, AuthenticationTypes.Secure);
                
                u.DirectoryEntry.Invoke("ChangePassword", oldPassword, newPassword);
                u.DirectoryEntry.CommitChanges();

                returnValue.Status = PasswordChangeResultStatus.Success;
            }
            catch (COMException cex)
            {
                returnValue.ErrorMessage = cex.Message;
                returnValue.StackTrace = cex.StackTrace;
                returnValue.Status = PasswordChangeResultStatus.Error;
            }
            catch (Exception ex)
            {
                returnValue.ErrorMessage= ex.Message;
                returnValue.StackTrace = ex.StackTrace;
                returnValue.Status = PasswordChangeResultStatus.Error;
            }

            return returnValue;
        }

        public string UserId
        {
            get { return userId; }
            //set { userId = value; }
        }

        public string LastName
        {
            get { return lastName; }
            //set { lastName = value; }
        }

        public string FirstName
        {
            get { return firstName; }
           // set { firstName = value; }
        }

        public DirectoryEntry DirectoryEntry
        {
            get { return this.userEntry; }
        }

        public bool IsInGroup(string groupName)
        {
            return this.groups.ContainsKey(groupName.ToLowerInvariant());
        }

        public DateTime PasswordLastSet
        {
            get
            {
                return DateTime.FromFileTime(LongFromLargeInteger(userEntry.Properties["pwdLastSet"].Value));
            }
        }

        public long MaximumPasswordAgeInDays
        {
            get
            {
                return LongFromLargeInteger(userEntry.Properties["maxPwdAge"].Value);
            }
        }

        public static long LongFromLargeInteger(object largeInteger)
        {
            System.Type type = largeInteger.GetType();
            int highPart = (int)type.InvokeMember("HighPart", BindingFlags.GetProperty, null, largeInteger, null);
            int lowPart = (int)type.InvokeMember("LowPart", BindingFlags.GetProperty, null, largeInteger, null);

            return (long)highPart << 32 | (uint)lowPart;
        }
    }
}
