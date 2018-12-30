/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.UserAccounts;

namespace Bam.Net.Encryption.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static ConsoleActions()
        {
            IsolateMethodCalls = false;
        }

        #region ConsoleAction examples
        //[ConsoleAction("Create Vault")]
        //public void CreateVault()
        //{
        //    OutLine("Enter the name of the vault file, it will be created if necessary.");
        //    OutLine("The file name will be in the format ([name].vault.sqlite)");
        //    string name = Prompt("Enter the vault name");
        //    string password = Prompt("Enter the password to use");
        //    FileInfo file = new FileInfo("{0}.vault.sqlite"._Format(name));

        //    Vault vault = Vault.Get(file, name, password);

        //    Current = vault;
        //    OutLineFormat("Vault file {0} created", file.FullName);
        //}

        public static Vault Current
        {
            get;
            set;
        }

        [ConsoleAction("Set vault value")]
        public void SetVaultValue()
        {
            string key = Prompt("Enter the key");
            string value = Prompt("Enter the value");

            if (Current == null)
            {
                SetCurrentVault();
            }

            Current[key] = value;
        }

        [ConsoleAction("Show vault values")]
        public void ShowVaultValues()
        {
            if (Current == null)
            {
                SetCurrentVault();
            }
            OutLine(Current.Name, ConsoleColor.Cyan);
            Current.Keys.Each(key =>
            {
                OutLineFormat("{0}={1}"._Format(key, Current[key]));
            });
        }
  
        [ConsoleAction("Set current vault")]
        public void SetCurrentVault()
        {
            string fileName = Prompt("Enter the name of the vault file");
            FileInfo vaultFile = new FileInfo(".\\{0}.vault.sqlite"._Format(fileName));
            Current = Vault.Load(vaultFile, fileName);
        }

        [ConsoleAction("Copy vault")]
        public void CopyVault()
        {            
            string copyTo = Prompt("Enter the name of the new vault");

            if (Current == null)
            {
                SetCurrentVault();
            }
            FileInfo toCopyFile = new FileInfo(".\\{0}.vault.sqlite"._Format(copyTo));
            Current = Current.Copy(toCopyFile, copyTo);
        }

        [ConsoleAction("Create smtp settings file")]
        public void CreateSmtpSettingsFile()
        {
            string fileName = Prompt("Enter the name of the vault settings file to create");
            FileInfo credsFile = new FileInfo(".\\{0}.vault.sqlite"._Format(fileName));
            Vault credsVault = Vault.Load(credsFile, fileName);
            string smtpHost = Prompt("Enter the smtp host name");
            string userName = Prompt("Enter the user name for the host");
            string password = Prompt("Enter the password for the user");
            string from = Prompt("Enter the from address to use");
            string displayName = Prompt("Enter the display name to use");
            string port = Prompt("Enter the port number to use");
            bool enableSsl = Confirm("Use ssl?");

            credsVault["SmtpHost"] = smtpHost;
            credsVault["UserName"] = userName;
            credsVault["Password"] = password;
            credsVault["From"] = from;
            credsVault["DisplayName"] = displayName;
            credsVault["Port"] = port;
            credsVault["EnableSsl"] = enableSsl.ToString();

            Current = credsVault;
        }
        #endregion
    }
}