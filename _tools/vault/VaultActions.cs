using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net;
using System.IO;

namespace vault
{
    [Serializable]
    public class VaultActions: CommandLineTestInterface
    {
        [ConsoleAction("readVaults", "Read vault")]
        public void ReadVault()
        {
            string vaultPath = GetVaultPath();
            OutLineFormat("Loading vaults from {0}", ConsoleColor.Cyan, vaultPath);
            VaultDatabase vaultDatabase = VaultDatabase.FromFile(vaultPath);
            foreach(Vault toRead in Vault.LoadAll(vaultDatabase))
            {
                OutLineFormat("Vault {0}", ConsoleColor.DarkBlue, toRead.Name);
                foreach (string key in toRead.Keys)
                {
                    OutLineFormat("{0}: {1}", key, toRead[key]);
                }
                OutLine();
            }
        }

        [ConsoleAction("import", "Import key value pairs")]
        public void Import()
        {
            string filePath = Path.Combine(Paths.Conf, GetArgument("import", $"Enter the name of the json file in {Paths.Conf} to import"));
            if (!File.Exists(filePath))
            {
                OutLineFormat("File not found: {0}", ConsoleColor.Magenta, filePath);
                return;
            }
            Dictionary<string, string> values = filePath.FromJsonFile<Dictionary<string, string>>();
            Paths.AppData = Paths.Conf; // redirect AppData so Vault.Load directs to conf
            Vault vault = Vault.Load(Path.GetFileNameWithoutExtension(filePath));
            vault.ImportValues(values);
            foreach (string key in vault.Keys)
            {
                OutLineFormat("{0}: {1}", key, vault[key]);
            }
        }
        
        [ConsoleAction("importCredentials", "Import credentials from a json file")]
        public void ImportCredentials()
        {
            CredentialManager mgr = CredentialManager.Local;
            string jsonFile = GetArgument("importCredentials", "Please enter the path to the credential json file to import");
            CredentialInfo[] infos = jsonFile.FromJsonFile<CredentialInfo[]>();
            foreach(CredentialInfo info in infos)
            {
                mgr.SetCredentials(info);
            }
        }

        // TODO: write value, set password, export password
        private string GetVaultPath()
        {
            return Arguments["rv"].Or(Arguments.Contains("vp") ? Arguments["vp"] : Arguments.Contains("vaultpath") ? Arguments["vaultpath"] : Prompt("Please enter the path to the vault to read"));
        }

        private string GetVaultName()
        {
            return Arguments.Contains("vn") ? Arguments["vn"] : Arguments.Contains("vaultname") ? Arguments["vaultname"] : GetArgument("vaultname");
        }
    }
}
