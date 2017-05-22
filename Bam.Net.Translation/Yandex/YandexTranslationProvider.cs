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
    public class YandexTranslationProvider : TranslationProvider
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

        public Language DetectLanguage(string text, out YandexLanguageDetectResponse response)
        {
            string json = Http.Get(ApiDetectEndpointFormat.NamedFormat(new { ApiKey = ApiKey, Text = text }));
            response = json.FromJson<YandexLanguageDetectResponse>();            
            Task.Run(() => Logger.AddEntry("DetectLanguageResponse: {0}", json));
            return response.GetLanguage(LanguageDatabase);
        }

        protected override string GetTranslationFromService(string from, string to, string text)
        {
            YandexTranslationResponse response = new YandexTranslationResponse();
            switch (HttpMethod)
            {
                case HttpMethod.GET:
                    var args = new { ApiKey = ApiKey, From = from, To = to, Text = Uri.EscapeUriString(text).Replace("%20", "+") };
                    response = Http.GetJson<YandexTranslationResponse>(ApiGetEndpointFormat.NamedFormat(args));
                    break;
                case HttpMethod.POST:
                    var postArgs = new { ApiKey = ApiKey, From = from, To = to, Text = text };
                    response = Http.Post<YandexTranslationResponse>(ApiPostEndpoint, postArgs.FormEncode());
                    break;
            }
            return response.text[0];
        }

        private void TryEnsureSchemas()
        {
            TranslationDatabase.TryEnsureSchema(typeof(Translation));
            LanguageDatabase.TryEnsureSchema(typeof(Language));
        }
    }
}
