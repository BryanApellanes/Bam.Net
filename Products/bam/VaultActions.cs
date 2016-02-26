using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net;

namespace bam
{
    [Serializable]
    public class VaultActions: CommandLineTestInterface
    {
        [ConsoleAction("rv", "Read vault")]
        public void ReadVault()
        {
            string vaultPath = GetVaultPath();
            string vaultName = GetVaultName();
            OutLineFormat("Loading vault {0} from {1}", ConsoleColor.Cyan, vaultName, vaultPath);
            Vault toRead = Vault.Load(vaultPath, vaultName);
            foreach(string key in toRead.Keys)
            {
                OutLineFormat("{0}: {1}", key, toRead[key]);
            }
        }
        
        private string GetVaultPath()
        {
            return Arguments["rv"].Or(Arguments.Contains("vp") ? Arguments["vp"] : Arguments.Contains("vaultpath") ? Arguments["vaultpath"] : Prompt("Please enter the path to the vault to read"));
        }

        private string GetVaultName()
        {
            return Arguments.Contains("vn") ? Arguments["vn"] : Arguments.Contains("vaultname") ? Arguments["vaultname"] : "SmtpSettings";
        }
    }
}
