/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.Web;
using CsQuery;
using NCuid;

namespace Bam.Net.Translation
{
    /// <summary>
    /// A component that provides language translation
    /// </summary>
    public abstract class TranslationProvider: Loggable, ITranslationProvider, ILanguageDetector, IIsoLanguageTranslationProvider
    {
        public TranslationProvider()
        {
            Logger = Log.Default;
            DownloadLanguages = DefaultConfiguration.GetAppSetting("DownloadLanguages", "No").IsAffirmative();
        }
        public TranslationProvider(ILogger logger)
        {
            Logger = logger;
            DownloadLanguages = DefaultConfiguration.GetAppSetting("DownloadLanguages", "No").IsAffirmative();
        }

        static TranslationProvider()
        {
            void setCuid(Dao dao) => dao.Property("Cuid", Cuid.Generate());
            Dao.PostConstructActions.AddMissing(typeof(Language), setCuid);
            Dao.PostConstructActions.AddMissing(typeof(LanguageDetection), setCuid);
            Dao.PostConstructActions.AddMissing(typeof(Text), setCuid);
            Dao.PostConstructActions.AddMissing(typeof(OtherName), setCuid);
            Dao.PostConstructActions.AddMissing(typeof(Translation), setCuid);
        }

        public Database LanguageDatabase { get; set; }
        /// <summary>
        /// The Database to store and retrieve translated
        /// text
        /// </summary>
        public Database TranslationDatabase { get; set; }
        protected string ApiKeyKey { get; set; }
        /// <summary>
        /// The Vault that holds the Yandex api key
        /// </summary>
        protected Vault ApiKeyVault { get; set; }

        object _apiKeyLock = new object();
        string _apiKey;
        protected string ApiKey
        {
            get
            {
                return _apiKeyLock.DoubleCheckLock(ref _apiKey, () => ApiKeyVault.Get(ApiKeyKey));
            }
        }

        public string Translate(long languageIdFrom, long lanugageIdTo, string input)
        {
            return Translate(Language.GetById(languageIdFrom, LanguageDatabase), Language.GetById(lanugageIdTo, LanguageDatabase), input);
        }

        public string Translate(string uuidFrom, string uuidTo, string input)
        {
            return Translate(Language.GetByUuid(uuidFrom, LanguageDatabase), Language.GetByUuid(uuidTo, LanguageDatabase), input);
        }
        
        /// <summary>
        /// Translate the specified input from the specified fromLanguage to the specified
        /// toLanguage 
        /// </summary>
        /// <param name="fromLanguage"></param>
        /// <param name="toLanguage"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string Translate(Language fromLanguage, Language toLanguage, string input)
        {
            Text text = Text.OneWhere(c => c.Value == input, TranslationDatabase);
            if (text == null)
            {
                text = new Text();
                text.Value = input;
                text.LanguageId = fromLanguage.Id;
                text.Save(TranslationDatabase);
            }
            Translation translation = text.TranslationsByTextId.FirstOrDefault(t => t.LanguageId == toLanguage.Id);
            if (translation == null)
            {
                string from = fromLanguage.ISO6391.Or(fromLanguage.ISO6392.First(2));
                string to = toLanguage.ISO6391.Or(toLanguage.ISO6392.First(2));
                translation = new Translation();
                translation.Value = GetTranslationFromService(from, to, input);
                translation.LanguageId = toLanguage.Id;
                translation.TextId = text.Id;
                translation.Translator = this.GetType().Name;
                translation.Save(TranslationDatabase);
            }

            return translation.Value;
        }

        protected abstract string GetTranslationFromService(string twoLetterIsoLanguageCodeFrom, string twoLetterIsoLanguageCodeTo, string input);

        public abstract Language DetectLanguage(string text);

        public virtual string Translate(string input, string languageIdentifier)
        {
            Language toLanguage = FindLanguage(languageIdentifier);

            Args.ThrowIf<ArgumentException>(toLanguage == null, "Unable to identify specified language {0}, supported values are:\r\n{1}", languageIdentifier, string.Join("\r\n", Language.LoadAll(LanguageDatabase).Select(lang => lang.ISO6391).ToArray()));

            return Translate(input, toLanguage);
        }

        public string Translate(string input, Language toLanguage)
        {
            Text text = Text.OneWhere(t => t.Value == input, TranslationDatabase);
            Language fromLanguage = null;            
            if (text != null && text.LanguageOfLanguageId != null)
            {
                fromLanguage = text.LanguageOfLanguageId;
            }
            else
            {
                fromLanguage = DetectLanguage(input);
                LanguageDetection detection = new LanguageDetection();
                detection.LanguageId = fromLanguage.Id;

                text = new Text();
                text.Value = input;
                text.LanguageId = fromLanguage.Id;
                text.Save(TranslationDatabase);
                detection.TextId = text.Id;
                detection.Detector = this.GetType().FullName;
                detection.Save(TranslationDatabase);
            }

            return Translate(fromLanguage, toLanguage, input);
        }

        public virtual string TranslateLanguages(string input, string inputLanguageIdentifier, string outputLanguageIdentifier)
        {
            Language fromLanguage = FindLanguage(inputLanguageIdentifier);
            Language toLanguage = FindLanguage(outputLanguageIdentifier);
            return Translate(fromLanguage, toLanguage, input);
        }
        public virtual Language FindLanguage(string languageIdentifier)
        {
            return FindLanguage(languageIdentifier, LanguageDatabase);
        }

        public static Language FindLanguage(string languageIdentifier, Database db)
        {
            Language toLanguage;
            if (languageIdentifier.Length == 2)
            {
                toLanguage = Language.OneWhere(c => c.ISO6391 == languageIdentifier, db);
            }
            else if (languageIdentifier.Length == 3)
            {
                toLanguage = Language.OneWhere(c => c.ISO6392 == languageIdentifier, db);
            }
            else
            {
                toLanguage = Language.OneWhere(c => c.EnglishName == languageIdentifier, db);
                if (toLanguage == null)
                {
                    OtherName otherName = OtherName.FirstOneWhere(c => c.Value == languageIdentifier, db);
                    if (otherName != null)
                    {
                        toLanguage = otherName.LanguageOfLanguageId;
                    }
                }
            }

            return toLanguage ?? Language.Default;
        }
        public event EventHandler LanguageSaved;
        public event EventHandler LanguageOtherNameSaved;
        public ILogger Logger { get; set; }
        public bool DownloadLanguages { get; set; }

        /// <summary>
        /// Retrieves language information from https://www.loc.gov/standards/iso639-2/php/code_list.php
        /// and stores it in the LanguageDatabase
        /// </summary>
        public void EnsureLanguages()
        {
            if (DownloadLanguages)
            {
                EnsureLanguages(LanguageDatabase, (lang) =>
                {
                    FireEvent(LanguageSaved, new LanguageEventArgs { Language = lang, Message = "Language saved" });
                    Logger.AddEntry("Language saved: \r\n{0}", lang.ToJsonSafe().PropertiesToString());
                }, (lang) =>
                {
                    Logger.AddEntry("Language already exists: \r\n{0}", LogEventType.Warning, lang.ToJsonSafe().PropertiesToString());
                }, OnOtherNameSaved, OnOtherNameExists);
            }
        }

        static bool? _languagesRetrieved;
        static object _retrieveLock = new object();
        /// <summary>
        /// Retrieves language information from https://www.loc.gov/standards/iso639-2/php/code_list.php
        /// and stores it in the specified languageDb
        /// </summary>
        /// <param name="languageDb"></param>
        /// <param name="onSaved"></param>
        /// <param name="onExists"></param>
        /// <param name="onOtherNameSaved"></param>
        /// <param name="onOtherNameExists"></param>
        public static bool? EnsureLanguages(Database languageDb, Action<Language> onSaved = null, Action<Language> onExists = null, Action<OtherName> onOtherNameSaved = null, Action<OtherName> onOtherNameExists = null)
        {
            return _retrieveLock.DoubleCheckLock(ref _languagesRetrieved, () =>
            {
                CQ cq = CQ.Create(Http.Get("https://www.loc.gov/standards/iso639-2/php/code_list.php"));
                cq = cq.Remove("script");

                bool first = true;
                cq["tr", cq["table[width='100%'][cellspacing='0'][cellpadding='4'][border='1']"]].Each((Action<IDomObject>)(row =>
                {
                    if (!first)
                    {
                        string isoCode = cq["td", row][0].InnerText.DelimitSplit(" ")[0];
                        Language language = Language.OneWhere(c => c.ISO6392 == isoCode, languageDb);
                        if (language == null)
                        {
                            language = new Language();
                            language.Uuid = Guid.NewGuid().ToString();
                            language.ISO6392 = isoCode;
                            language.ISO6391 = cq["td", row][1].InnerText;
                            string[] englishNames = cq["td", row][2].InnerText.DelimitSplit(";");
                            string[] frenchNames = cq["td", row][3].InnerText.DelimitSplit(";");
                            string[] germanNames = cq["td", row][4].InnerText.DelimitSplit(";");
                            language.EnglishName = englishNames.Length > 0 ? englishNames[0] : "";
                            language.FrenchName = frenchNames.Length > 0 ? frenchNames[0].Or("&nbsp;") : "";
                            language.GermanName = germanNames.Length > 0 ? germanNames[0].Or("&nbsp;") : "";
                            language.Save(languageDb);

                            if (onSaved != null)
                            {
                                onSaved(language);
                            }

                            if (englishNames.Length > 1)
                            {
                                SetOtherNames(languageDb, language, englishNames, "English");
                            }
                            if (frenchNames.Length > 1)
                            {
                                SetOtherNames(languageDb, language, frenchNames, "French");
                            }
                            if (germanNames.Length > 1)
                            {
                                SetOtherNames(languageDb, language, germanNames, "German");
                            }
                        }
                        else
                        {
                            if (onExists != null)
                            {
                                onExists(language);
                            }
                        }
                    }
                    first = false;
                }));
                return true;
            });
        }

        private void OnOtherNameSaved(OtherName name)
        {
            FireEvent(LanguageOtherNameSaved, new LanguageEventArgs { OtherName = name });
            Logger.AddEntry("Language OtherName saved: \r\n{0}", name.ToJsonSafe().PropertiesToString());
        }

        private void OnOtherNameExists(OtherName name)
        {
            Logger.AddEntry("Language OtherName exists: \r\n{0}", name.ToJsonSafe().PropertiesToString());
        }

        private static void SetOtherNames(Database db, Language language, string[] names, string languageIn, Action<OtherName> onSaved = null, Action<OtherName> onExists = null)
        {
            names.Rest(1, (langName) =>
            {
                OtherName name = OtherName.OneWhere(o => o.LanguageId == language.Id && o.LanguageName == languageIn && o.Value == langName, db);
                if (name == null)
                {
                    name = new OtherName();
                    name.Uuid = Guid.NewGuid().ToString();
                    name.LanguageId = language.Id;
                    name.LanguageName = languageIn;
                    name.Value = langName;
                    name.Save(db);
                    if (onSaved != null)
                    {
                        onSaved(name);
                    }
                }
                else
                {
                    if (onExists != null)
                    {
                        onExists(name);
                    }
                }
            });
        }

    }
}
