/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using System.IO;

namespace Bam.Net.Messaging
{
    public class Notify
    {
        static Vault _credentialVault;
        static object _credentialVaultSync = new object();
        public const string CredentialVaultName = "SysCreds";

        public static Vault CredentialVault
        {
            get
            {
                return _credentialVaultSync.DoubleCheckLock(ref _credentialVault, () =>
                {
                    return Vault.Load(new FileInfo(".\\SysCreds.vault.sqlite"), CredentialVaultName, "".RandomLetters(16), Log.Default);
                });
            }
            set
            {
                _credentialVault = value;
            }
        }
        public static bool ValidateRequiredSettings(Vault credentialContainer)
        {
            string[] ignore;
            return ValidateRequiredSettings(credentialContainer, out ignore);
        }

        public static bool ValidateRequiredSettings(Vault credentialContainer, out string[] messages)
        {
            List<string> msgTmp = new List<string>();
            if (string.IsNullOrEmpty(credentialContainer["SmtpHost"]))
            {
                msgTmp.Add("SmtpHost was blank");
            }

            if (string.IsNullOrEmpty(credentialContainer["UserName"]))
            {
                msgTmp.Add("UserName was blank");
            }

            if (string.IsNullOrEmpty(credentialContainer["Password"]))
            {
                msgTmp.Add("Password was blank");
            }

            messages = msgTmp.ToArray();
            return messages.Length == 0;
        }
        public static bool ValidateExtendedSettings(Vault credentialContainer)
        {
            string[] ignore;
            return ValidateExtendedSettings(credentialContainer, out ignore);
        }

        public static bool ValidateExtendedSettings(Vault credentialContainer, out string[] messages)
        {
            List<string> msgTmp = new List<string>();
            if (string.IsNullOrEmpty(credentialContainer["From"]))
            {
                msgTmp.Add("From was blank");
            }

            if (string.IsNullOrEmpty(credentialContainer["DisplayName"]))
            {
                msgTmp.Add("DisplayName was blank");
            }

            if (string.IsNullOrEmpty(credentialContainer["Port"]))
            {
                msgTmp.Add("Port was blank");
            }

            if (string.IsNullOrEmpty(credentialContainer["EnableSsl"]))
            {
                msgTmp.Add("EnableSsl was blank");
            }

            messages = msgTmp.ToArray();
            return messages.Length == 0;
        }

        private static void EnsureCredentials()
        {
            EnsureCredentials(CredentialVault);
        }

        private static void EnsureCredentials(Vault credentialContainer)
        {
            Args.ThrowIfNullOrEmpty(credentialContainer["SmtpHost"]);
            Args.ThrowIfNullOrEmpty(credentialContainer["UserName"]);
            Args.ThrowIfNullOrEmpty(credentialContainer["Password"]);
        }

        public static Email ByEmail(string to)
        {
            EnsureCredentials();

            return ByEmail(CredentialVault, to);
        }

        public static Email ByEmail(Vault smtpSettings, string to)
        {
            EnsureCredentials(smtpSettings);

            Email email = new Email()
                            .To(to)
                            .SetCredentials(smtpSettings);

            string from;
            if(smtpSettings.HasKey("From", out from))
            {
                string displayName;
                if(smtpSettings.HasKey("DisplayName", out displayName))
                {
                    email.From(from, displayName);
                }
                else
                {
                    email.From(from);
                }
            }

            string port;
            if(smtpSettings.HasKey("Port", out port))
            {
                email.Port(int.Parse(port));
            }

            string enableSsl;
            if (smtpSettings.HasKey("EnableSsl", out enableSsl))
            {
                email.EnableSsl(bool.Parse(enableSsl));
            }

            return email;
        }
    }
}
