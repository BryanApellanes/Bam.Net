using System;
using System.Collections.Generic;
using System.IO;
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
        public void SetVault()
        {
            SetVaultDatabase();
            VaultCollection vaults = Vault.LoadAll(Vault.SystemVaultDatabase);
            _vault = SelectFrom(vaults, v => v.Name);
        }

        [ConsoleAction]
        public void DeleteVault()
        {
            SetVault();
            ShowSettings();
            if(Confirm("Are you sure you want to delete this vault?"))
            {
                _vault.Delete();
            }
        }
        [ConsoleAction]
        public void ShowSettings()
        {
            SetVaultDatabase();
            if(_vault == null)
            {
                SetVault();
            }
            foreach (string key in _vault.Keys)
            {
                OutLineFormat("{0}: {1}", key, _vault[key]);
            }
        }

        [ConsoleAction]
        public void SetVaultValue()
        {
            SetVaultDatabase();
            if (_vault == null)
            {
                SetVault();
            }
            int index = SelectFrom(_vault.Keys);
            string keyToSet = _vault.Keys[index];
            _vault[keyToSet] = Prompt($"Enter the vault to set {keyToSet}");
        }

        [ConsoleAction]
        public void CreateVault()
        {
            SetVaultDatabase();
            string vaultName = Prompt("Enter the name of the vault to create");
            Vault.Retrieve(vaultName);
        }

        private static void SetVaultDatabase()
        {
            DefaultDataDirectoryProvider dataSettings = DefaultDataDirectoryProvider.Instance;
            Vault.SystemVaultDatabase = dataSettings.GetSysDatabaseFor(typeof(Vault), "System");
            Vault.SystemVaultDatabase.TryEnsureSchema<Vault>();
        }

        [ConsoleAction]
        public void IntegrationTests()
        {
            RunIntegrationTests();
        }
    }
}
