using Bam.Net.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation.Yandex
{
    public partial class YandexTranslationProvider
    {
        private static string ApiDetectEndpointNumberedFormat = "https://translate.yandex.net/api/v1.5/tr.json/detect?key={0}&text={1}";
        private static string ApiGetEndpointNumberedFormat = "https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&lang={1}-{2}&text={3}";

        protected override string GetTranslationFromService(string from, string to, string text)
        {
            YandexTranslationResponse response = new YandexTranslationResponse();
            switch (HttpMethod)
            {
                case HttpMethod.GET:
                    response = JsonHttp.Get<YandexTranslationResponse>(string.Format(ApiGetEndpointNumberedFormat, ApiKey, from, to, Uri.EscapeUriString(text).Replace("%20", "+")));
                    break;
                case HttpMethod.POST:
                    var postArgs = new { ApiKey = ApiKey, From = from, To = to, Text = text };
                    response = Http.Post<YandexTranslationResponse>(ApiPostEndpoint, postArgs.FormEncode());
                    break;
            }
            return response.text[0];
        }

        public Language DetectLanguage(string text, out YandexLanguageDetectResponse response)
        {
            string json = Http.Get(string.Format(ApiDetectEndpointNumberedFormat, ApiKey, text));
            response = json.FromJson<YandexLanguageDetectResponse>();
            Task.Run(() => Logger.AddEntry("DetectLanguageResponse: {0}", json));
            return response.GetLanguage(LanguageDatabase);
        }
    }
}
