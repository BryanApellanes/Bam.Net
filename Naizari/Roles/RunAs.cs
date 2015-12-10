/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Permissions;
using System.ComponentModel;

namespace Naizari.Roles
{
    public static class RunAs
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private unsafe static extern int FormatMessage(int dwFlags, ref IntPtr lpSource,
            int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, IntPtr* Arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        const int LOGON32_PROVIDER_DEFAULT = 0;
        //This parameter causes LogonUser to create a primary token.
        const int LOGON32_LOGON_INTERACTIVE = 2;

        public static RunAsContext Impersonate(IRunAsCredentials credentials)
        {
            return Impersonate(credentials.DomainAndUserName, credentials.Password);
        }

        public static RunAsContext Impersonate(string userNameAndDomain, string password)
        {
            string[] split = userNameAndDomain.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
                throw new ArgumentException(string.Format("Parameter not in the correct format <domain>\\<username>, value provided was {0}", userNameAndDomain), userNameAndDomain);

            return Impersonate(split[0], split[1], password);
        }

        public static RunAsContext Impersonate(string domain, string userName, string password)
        { 
            IntPtr tokenHandle = IntPtr.Zero;
            bool result = LogonUser(userName, domain, password, 
                LOGON32_LOGON_INTERACTIVE, 
                LOGON32_PROVIDER_DEFAULT, 
                ref tokenHandle);

            if (!result)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            WindowsIdentity newId = new WindowsIdentity(tokenHandle);
            return new RunAsContext(newId.Impersonate(), tokenHandle);
        }

        public static void UndoImpersonate(RunAsContext contextWrapper)
        {
            if (contextWrapper != null &&
                contextWrapper.Context != null)
            {
                contextWrapper.Context.Undo();
            }
            if (contextWrapper.TokenHandle != IntPtr.Zero)
                CloseHandle(contextWrapper.TokenHandle);
        }
    }
}
