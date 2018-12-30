using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Logging;
using Bam.Net.Web;

namespace Bam.Net.Translation.MyMemory
{
    public partial class MyMemoryTranslationProvider : TranslationProvider
    {
        private static string ApiTranslateEndpointFormat = "http://api.mymemory.translated.net/get?q={Text}&langpair={FromIsoLang}|{ToIsoLang}";

        public MyMemoryTranslationProvider(Database languageDatabase, Database translationDatabase, ILanguageDetector languageDetector, ILogger logger) : base(logger)
        {
            LanguageDatabase = languageDatabase ?? Bam.Net.Translation.LanguageDatabase.Default;
            TranslationDatabase = translationDatabase ?? Bam.Net.Translation.LanguageDatabase.Default;
            LanguageDetector = languageDetector;
            ApiKeyKey = "MyMemoryApiKey"; // not needed; its a free service
        }

        public ILanguageDetector LanguageDetector { get; set; }
        public override Language DetectLanguage(string text)
        {
            return LanguageDetector.DetectLanguage(text);
        }
    }
}
