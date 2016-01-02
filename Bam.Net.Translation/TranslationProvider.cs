/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Logging;

namespace Bam.Net.Translation
{
    public abstract class TranslationProvider: Loggable, ITranslationProvider
    {
        public TranslationProvider()
        {
            this.Logger = Log.Default;
            this.DownloadLanguages = true;
        }
        public TranslationProvider(ILogger logger)
        {
            this.Logger = logger;
            this.DownloadLanguages = true;
        }
        public Database LanguageDatabase { get; set; }
        /// <summary>
        /// The Database to store and retrieve translated
        /// text
        /// </summary>
        public Database TranslationDatabase { get; set; }

        public string Translate(long languageIdFrom, long lanugageIdTo, string input)
        {
            return Translate(Language.GetById(languageIdFrom, LanguageDatabase), Language.GetById(lanugageIdTo, LanguageDatabase), input);
        }

        public string Translate(string uuidFrom, string uuidTo, string input)
        {
            return Translate(Language.GetByUuid(uuidFrom, LanguageDatabase), Language.GetByUuid(uuidTo, LanguageDatabase), input);
        }

        public abstract string Translate(Language from, Language to, string input);

        public event EventHandler LanguageSaved;
        public event EventHandler LanguageOtherNameSaved;
        public ILogger Logger { get; set; }
        public bool DownloadLanguages { get; set; }
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
