/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Web;
using Bam.Net;
using Bam.Net.Encryption;
using Bam.Net.Data;

namespace Bam.Net.Translation.Yandex
{    
    public enum HttpMethod
    {
        GET,
        POST
    }

    /// <summary>
    /// A TranslationProvider that translates using Yandex
    /// </summary>
    public partial class YandexTranslationProvider : TranslationProvider
    {
        private static string ApiDetectEndpointFormat = "https://translate.yandex.net/api/v1.5/tr.json/detect?key={ApiKey}&text={Text}";
        private static string ApiGetEndpointFormat = "https://translate.yandex.net/api/v1.5/tr.json/translate?key={ApiKey}&lang={From}-{To}&text={Text}";
        private static string ApiPostEndpoint = "https://translate.yandex.net/api/v1.5/tr.json/translate";

        public YandexTranslationProvider(Vault yandexApiKeyVault, Database languageDatabase, Database translationDatabase)
        {
            ApiKeyKey = "YandexApiKey";
            ApiKeyVault = yandexApiKeyVault;
            LanguageDatabase = languageDatabase;
            TranslationDatabase = translationDatabase;
            TryEnsureSchemas();
        }

        protected YandexTranslationProvider()
        {
        }

        /// <summary>
        /// The http method GET or POST
        /// </summary>
        public HttpMethod HttpMethod{get;set;}

        public override Language DetectLanguage(string text)
        {
            YandexLanguageDetectResponse ignore;
            return DetectLanguage(text, out ignore);
        }

        private void TryEnsureSchemas()
        {
            TranslationDatabase.TryEnsureSchema(typeof(Translation));
            LanguageDatabase.TryEnsureSchema(typeof(Language));
        }
    }
}
