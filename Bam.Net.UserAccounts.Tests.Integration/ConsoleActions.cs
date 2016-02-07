using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Encryption;
using Bam.Net.Testing;

namespace Bam.Net.UserAccounts.Tests.Integration
{
    public class ConsoleActions: CommandLineTestInterface
    {
        [ConsoleAction]
        public void ShowSettings()
        {
            Vault vault = Vault.Load("C:\\testData\\StickerizeSmtpSettings.vault.sqlite", "SmtpSettings");
            foreach(string key in vault.Keys)
            {
                OutLineFormat("{0}: {1}", key, vault[key]);
            }
        }
    }
}
