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

namespace Bam.Net.Messaging.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        // See the below for examples of ConsoleActions and UnitTests

        [ConsoleAction]
        public void CreateCredentialVault()
        {
            string fileName = Prompt("Enter the name of the vault file to create");
            FileInfo file = new FileInfo(".\\{0}.vault.sqlite"._Format(fileName));
            Vault vault = Vault.Create(file, "SmtpSettings");
            vault["SmtpHost"] = Prompt("Enter the smtp host name");
            vault["Port"] = Prompt("Enter the port");
            vault["UserName"] = Prompt("Enter the user name");
            vault["Password"] = Prompt("Enter the password");
            vault["From"] = Prompt("Enter the from address");
            vault["DisplayName"] = Prompt("Enter the display name");
            vault["EnableSsl"] = Confirm("Use Ssl?").ToString();

            OutLineFormat("Vault file {0} was created", ConsoleColor.Cyan, file.FullName);
        }

        [ConsoleAction]
        public void ValidateCredentialFile()
        {
            string fileName = Prompt("Enter the name of the file to validate");
            FileInfo file = new FileInfo(".\\{0}.vault.sqlite"._Format(fileName));
            Vault vault = Vault.Load(file, "SmtpSettings");

            string[] messages;
            if (!Notify.ValidateRequiredSettings(vault, out messages))
            {
                messages.Each(msg =>
                {
                    OutLine(msg, ConsoleColor.Magenta);
                });
            }

            if (!Notify.ValidateExtendedSettings(vault, out messages))
            {
                messages.Each(msg =>
                {
                    OutLine(msg, ConsoleColor.Magenta);
                });
            }

            if (messages.Length == 0)
            {
                OutLine("Validation passed");
                vault.Keys.Each(key =>
                {
                    if (!key.ToLowerInvariant().Equals("password"))
                    {
                        OutLineFormat("{0}={1}", ConsoleColor.Yellow, key, vault[key]);
                    }
                });
            }
        }
    }
}