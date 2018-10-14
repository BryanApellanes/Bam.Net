using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Translation;

namespace Bam.Net.Services
{
    [Proxy("translationSvc")]
    public class TranslationService : ProxyableService, ILanguageDetector, ITranslationProvider, IIsoLanguageTranslationProvider
    {
        protected TranslationService() { }
        public TranslationService(
            IRepository genericRepo, 
            DaoRepository daoRepo, 
            AppConf appConf,
            ILanguageDetector languageDetector,
            ITranslationProvider translationProvider, 
            IIsoLanguageTranslationProvider isoLanguageTranslationProvider) : base(genericRepo, daoRepo, appConf)
        {
            LanguageDetector = languageDetector;
            TranslationProvider = translationProvider;
            IsoLanguageTranslationProvider = isoLanguageTranslationProvider;
        }

        public ILanguageDetector LanguageDetector { get; set; }
        public ITranslationProvider TranslationProvider { get; set; }
        public IIsoLanguageTranslationProvider IsoLanguageTranslationProvider { get; set; }

        public override object Clone()
        {
            TranslationService clone = new TranslationService(Repository, DaoRepository, AppConf, LanguageDetector, TranslationProvider, IsoLanguageTranslationProvider);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public virtual string DetectLanguageName(string text)
        {
            return DetectLanguage(text).EnglishName;
        }

        [Exclude]
        public virtual Language DetectLanguage(string text)
        {
            return LanguageDetector.DetectLanguage(text);
        }

        public virtual string Translate(string input, string languageIdentifier)
        {
            return IsoLanguageTranslationProvider.Translate(input, languageIdentifier);
        }

        public virtual string Translate(Language from, Language to, string input)
        {
            return TranslationProvider.Translate(from, to, input);
        }

        public virtual string Translate(string uuidFrom, string uuidTo, string input)
        {
            return TranslationProvider.Translate(uuidFrom, uuidTo, input);
        }

        public virtual string Translate(long languageIdFrom, long languageIdTo, string input)
        {
            return TranslationProvider.Translate(languageIdFrom, languageIdTo, input);
        }

        public string TranslateLanguages(string input, string inputLanguageIdentifier, string outputLanguageIdentifier)
        {
            return IsoLanguageTranslationProvider.TranslateLanguages(input, inputLanguageIdentifier, outputLanguageIdentifier);
        }
    }
}
