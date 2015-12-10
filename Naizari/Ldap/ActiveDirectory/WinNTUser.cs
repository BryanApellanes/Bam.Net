/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace Naizari.Ldap.ActiveDirectory
{
    public class WinNTUser
    {
        DirectoryEntry entry;
        string userName;
        public WinNTUser(string fqdn, string userName): this(fqdn, userName, "", "")
        {

        }
        public WinNTUser(string fqdn, string userName, string authenticateAs, string passwordToAuthenticateWith)
        {
            this.userName = userName;
            if (!string.IsNullOrEmpty(authenticateAs) && !string.IsNullOrEmpty(passwordToAuthenticateWith))
            {
                AuthenticateAs = authenticateAs;
                PasswordToAuthenticateWith = passwordToAuthenticateWith;
                entry = new DirectoryEntry(string.Format("WinNT://{0}/{1}", fqdn, userName), authenticateAs, passwordToAuthenticateWith, AuthenticationTypes.Secure);
            }
            else
            {
                entry = new DirectoryEntry(string.Format("WinNT://{0}/{1}", fqdn, userName));
            }

            try
            {
                string name = entry.Name;
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147024843)
                {
                    NotFound = true;
                }
                else
                {
                    throw ex;
                }
            }
        }



        public string AuthenticateAs { get; set; }
        public string PasswordToAuthenticateWith { get; set; }

        public bool NotFound
        {
            get;
            private set;
        }

        public int PasswordAgeInDays
        {
            get
            {
                return int.Parse(EntryUtil.GetProperty(entry, "PasswordAge")) / 86400;
            }
        }
        
        public bool PasswordExpired
        {
            get
            {
                return EntryUtil.GetProperty(entry, "PasswordExpired").Equals("1");
            }
        }

        public string FullName
        {
            get{return EntryUtil.GetProperty(entry, "FullName");}
        }

        public string Description
        {
            get{ return EntryUtil.GetProperty(entry, "Description");}
        }

        public DateTime LastLogin
        {
            get{ return DateTime.Parse(EntryUtil.GetProperty(entry, "LastLogin"));}
        }

        public string HomeDirectory
        {
            get{ return EntryUtil.GetProperty(entry, "HomeDirectory");}
        }

        public string LoginScript
        {
            get{ return EntryUtil.GetProperty(entry, "LoginScript");}
        }

        public int MinPasswordLength
        {
            get{ return int.Parse(EntryUtil.GetProperty(entry, "MinPasswordLength"));}
        }

        public int MaxPasswordAgeInDays
        {
            get{ return int.Parse(EntryUtil.GetProperty(entry, "MaxPasswordAge")) / 86400;}
        }

        public int PasswordExpiresInDays
        {
            get { return MaxPasswordAgeInDays - PasswordAgeInDays; }
        }

        public string UserName
        {
            get { return userName; }
        }
    }
}
