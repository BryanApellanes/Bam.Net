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
    public class YandexTranslationProvider : LanguageDetectingTranslationProvider
    {
        public static string ApiKeyKey = "YandexApiKey";
        private static string ApiDetectEndpointFormat = "https://translate.yandex.net/api/v1.5/tr.json/detect?key={ApiKey}&text={Text}";
        private static string ApiGetEndpointFormat = "https://translate.yandex.net/api/v1.5/tr.json/translate?key={ApiKey}&lang={From}-{To}&text={Text}";
        private static string ApiPostEndpoint = "https://translate.yandex.net/api/v1.5/tr.json/translate";

        public YandexTranslationProvider(Vault yandexApiKeyVault, Database languageDatabase, Database translationDatabase)
        {
            this.ApiKeyVault = yandexApiKeyVault;
            this.LanguageDatabase = languageDatabase;
            this.TranslationDatabase = translationDatabase;
            this.TryEnsureSchemas();
        }
    
        /// <summary>
        /// The http method GET or POST
        /// </summary>
        public HttpMethod HttpMethod{get;set;}

        /// <summary>
        /// The Vault that holds the Yandex api key
        /// </summary>
        protected Vault ApiKeyVault { get; set; }

        public override Language DetectLanguage(string text)
        {
            LanguageDetectResponse ignore;
            return DetectLanguage(text, out ignore);
        }

        public Language DetectLanguage(string text, out LanguageDetectResponse response)
        {
            response = Http.GetJson<LanguageDetectResponse>(ApiDetectEndpointFormat.NamedFormat(new { ApiKey = ApiKey, Text = text }));
            return response.GetLanguage(LanguageDatabase);
        }

        /// <summary>
        /// Translate the specified input from the specified fromLanguage to the specified
        /// toLanguage 
        /// </summary>
        /// <param name="fromLanguage"></param>
        /// <param name="toLanguage"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string Translate(Language fromLanguage, Language toLanguage, string input)
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
                translation.TranslatorUuid = this.GetType().Name;
                translation.Save(TranslationDatabase);
            }

            return translation.Value;
        }

        object _apiKeyLock = new object();
        string _apiKey;
        private string ApiKey
        {
            get
            {
                return _apiKeyLock.DoubleCheckLock(ref _apiKey, () => ApiKeyVault.Get(ApiKeyKey));
            }
        }

        private string GetTranslationFromService(string from, string to, string text)
        {
            TranslationResponse response = new TranslationResponse();
            switch (HttpMethod)
            {
                case HttpMethod.GET:
                    var args = new { ApiKey = ApiKey, From = from, To = to, Text = Uri.EscapeUriString(text).Replace("%20", "+") };
                    response = Http.GetJson<TranslationResponse>(ApiGetEndpointFormat.NamedFormat(args));
                    break;
                case HttpMethod.POST:
                    var postArgs = new { ApiKey = ApiKey, From = from, To = to, Text = text };
                    response = Http.Post<TranslationResponse>(ApiPostEndpoint, postArgs.FormEncode());
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
