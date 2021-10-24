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
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Web;
using Bam.Net.Translation;
using Bam.Net.Configuration;

namespace Bam.Net.Translation.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTool
    {
        // See the below for examples of ConsoleActions and UnitTests

        #region ConsoleAction examples
        [ConsoleAction("Fill language table")]
        public void FillLanguageTable()
        {
            Action<dynamic> output = (args) =>
            {
                OutLineFormat(args.Message, args.Color, args.Parameters);
            };

            string languageDatabasePath = DefaultConfiguration.GetAppSetting("languageDatabasePath");

            FileInfo languageFile = new FileInfo(languageDatabasePath);
            DirectoryInfo directory = languageFile.Directory;
            SQLiteDatabase translationDb = new SQLiteDatabase(directory.FullName, Path.GetFileNameWithoutExtension(languageFile.FullName));
            translationDb.TryEnsureSchema(typeof(Language));
            TranslationProvider.EnsureLanguages(translationDb);
        }

        static string apiKeyKey = "YandexApiKey";
        [ConsoleAction("Set Yandex api key in vault file")]
        public void SetYandexApiKey()
        {
            Vault vault = LoadVault();
            string apiKey = Prompt("Please enter the Yandex api key to store");
            vault.Set(apiKeyKey, apiKey);
        }

        [ConsoleAction("Show YandexApiKey")]
        public void ShowYandexApiKey()
        {
            Vault vault = LoadVault();
            Message.PrintLine("{0}", vault.Get(apiKeyKey));
        }

        private static Encryption.Vault LoadVault()
        {
            string vaultPath = DefaultConfiguration.GetAppSetting("vaultPath");
            VaultInfo vaultInfo = new VaultInfo(new FileInfo(vaultPath));
            Vault vault = vaultInfo.Load();
            return vault;
        }

        #endregion
    }
}