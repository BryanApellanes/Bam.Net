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

        public Language DetectLanguage(string text, out YandexLanguageDetectResponse response)
        {
            string json = Http.Get(ApiDetectEndpointFormat.NamedFormat(new { ApiKey = ApiKey, Text = text }));
            response = json.FromJson<YandexLanguageDetectResponse>();
            Task.Run(() => Logger.AddEntry("DetectLanguageResponse: {0}", json));
            return response.GetLanguage(LanguageDatabase);
        }
    }
}
