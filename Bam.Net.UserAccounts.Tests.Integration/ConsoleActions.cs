using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Encryption;
using Bam.Net.Testing;

namespace Bam.Net.UserAccounts.Tests.Integration
{
    public class ConsoleActions: CommandLineTestInterface
    {
        static Vault _vault;

        [ConsoleAction]
        public Vault SetVault()
        {
            SetVaultDatabase();
            VaultCollection vaults = Vault.LoadAll(Vault.DefaultDatabase);
            _vault = SelectFrom(vaults, v => v.Name);
            return _vault;
        }

        [ConsoleAction]
        public void ShowSettings()
        {
            SetVaultDatabase();
            Vault vault = _vault ?? SetVault();
            foreach (string key in _vault.Keys)
            {
                OutLineFormat("{0}: {1}", key, _vault[key]);
            }
        }

        [ConsoleAction]
        public void SetVaultValue()
        {
            SetVaultDatabase();
            Vault vault = _vault ?? SetVault();
            int index = SelectFrom(vault.Keys);
            string keyToSet = vault.Keys[index];
            vault[keyToSet] = Prompt($"Enter the vault to set {keyToSet}");
        }

        private static void SetVaultDatabase()
        {
            DataSettings dataSettings = DataSettings.Default;
            Vault.DefaultDatabase = dataSettings.GetDatabaseFor(typeof(Vault), "System");
            Vault.DefaultDatabase.TryEnsureSchema<Vault>();
        }



        [ConsoleAction]
        public void IntegrationTests()
        {
            RunIntegrationTests();
        }
    }
}
