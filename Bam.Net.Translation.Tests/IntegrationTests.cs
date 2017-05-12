/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Translation;
using Bam.Net.Translation.Yandex;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using System.IO;
using Bam.Net.Testing.Integration;
using Bam.Net.Translation.DetectLanguage;
using Bam.Net.Translation.MyMemory;

namespace Bam.Net.Translation.Tests
{
    [Serializable]
    public class IntegrationTests : CommandLineTestInterface
    {
        [IntegrationTest]
        [ConsoleAction]
        public void DetectLanguageTest()
        {
            DetectLanguageResponse r = new DetectLanguageResponse
            {
                data = new DetectionData
                {
                    detections = new Detection[]
                    {
                        new Detection{language = "zu", isReliable = true, confidence = 9}
                    }
                }
            };
            OutLine(r.ToJson());
            DetectLanguageLanguageDetector detector = new DetectLanguageLanguageDetector();
            Language lang = detector.DetectLanguage("Ich mag Affen");
            Expect.AreEqual("de", lang.ISO6391);
        }

        [IntegrationTest]
        [ConsoleAction]
        public void FreeTranslatorTest()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            Database txDb = new SQLiteDatabase(".\\", nameof(FreeTranslatorTest));
            txDb.TryEnsureSchema<Translation>();
            MyMemoryTranslationProvider txor = new MyMemoryTranslationProvider(LanguageDatabase.Default, txDb, new DetectLanguageLanguageDetector(), logger);
            string german = txor.Translate("The sky is blue", "German");
            Expect.AreEqual("Der Himmel ist blau.", german);
        }

        [IntegrationTest]
        public void TestTranslation()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);

            string input = "I like monkeys";
            Language english = Language.OneWhere(l => l.EnglishName == "English", languageDb);
            Language french = Language.OneWhere(l => l.EnglishName == "French", languageDb);
            Language german = Language.OneWhere(l => l.EnglishName == "German", languageDb);
            Expect.IsNotNull(english);
            Expect.IsNotNull(french);
            Expect.IsNotNull(german);

            string inFrench = translator.Translate(english, french, input);
            Expect.AreEqual("J'aime les singes", inFrench);
            OutLineFormat("French: {0}", ConsoleColor.Cyan, inFrench);
            string inGerman = translator.Translate(english, german, input);
            Expect.AreEqual("Ich mag Affen", inGerman);
            OutLineFormat("German: {0}", ConsoleColor.DarkCyan, inGerman);

            string inEnglish = translator.Translate(german, english, inGerman);
            Expect.AreEqual(input, inEnglish);
        }

        [IntegrationTest]
        public void ShouldDetectGerman()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);

            Language shouldBeGerman = translator.DetectLanguage("Ich mag Affen");
            Expect.AreEqual("German", shouldBeGerman.EnglishName);
        }

        [IntegrationTest]
        public void ShouldDetectAndTranslate()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);
            string result = translator.Translate("Ich mag Affen", Language.OneWhere(c => c.EnglishName == "English", languageDb));
            Expect.AreEqual("I like monkeys", result);
        }

        [IntegrationTest]
        public void ShouldTranslateUsingIsoCode1()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);
            string result = translator.Translate("Ich mag Affen", "en");
            Expect.AreEqual("I like monkeys", result);
        }
        [IntegrationTest]
        public void ShouldTranslateUsingIsoCode2()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);
            string result = translator.Translate("Ich mag Affen", "eng");
            Expect.AreEqual("I like monkeys", result);
        }

        [IntegrationTest]
        public void ShouldTranslateUsingLanguageName()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);
            string result = translator.Translate("Ich mag Affen", "English");
            Expect.AreEqual("I like monkeys", result);
        }
        //J'aime les singes
        [IntegrationTest]
        public void ShouldTranslateUsingLanguageName2()
        {
            SQLiteDatabase languageDb;
            YandexTranslationProvider translator;
            GetTranslatorAndLanguageDb(out languageDb, out translator);
            string result = translator.Translate("Ich mag Affen", "French");
            Expect.AreEqual("J'aime les Singes", result);
        }
        private static void GetTranslatorAndLanguageDb(out SQLiteDatabase languageDb, out YandexTranslationProvider translator)
        {
            string vaultPath = DefaultConfiguration.GetAppSetting("vaultPath");
            string translationPath = DefaultConfiguration.GetAppSetting("translationDatabasePath");
            string languagePath = DefaultConfiguration.GetAppSetting("languageDatabasePath");
            Args.ThrowIfNullOrEmpty(vaultPath, "vaultPath was not in the config file");
            Args.ThrowIfNullOrEmpty(translationPath, "databasePath was not in the config file");

            VaultInfo vaultInfo = new VaultInfo(new FileInfo(vaultPath));
            FileInfo translationFile = new FileInfo(translationPath);
            FileInfo languageFile = new FileInfo(languagePath);
            SQLiteDatabase translationDb = new SQLiteDatabase(translationFile.Directory.FullName, Path.GetFileNameWithoutExtension(translationFile.Name));
            languageDb = new SQLiteDatabase(languageFile.Directory.FullName, Path.GetFileNameWithoutExtension(languageFile.Name));
            translator = new YandexTranslationProvider(vaultInfo.Load(), languageDb, translationDb);
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.UseColors = true;
            logger.StartLoggingThread();
            translator.Logger = logger;
            translator.EnsureLanguages();
        }
    }
}
