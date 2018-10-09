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
        protected override string GetTranslationFromService(string twoLetterIsoLanguageCodeFrom, string twoLetterIsoLanguageCodeTo, string input)
        {
            string json = Http.Get(ApiTranslateEndpointFormat.NamedFormat(new { Text = input, FromIsoLang = twoLetterIsoLanguageCodeFrom, ToIsoLang = twoLetterIsoLanguageCodeTo }));
            Task.Run(() => Logger.AddEntry("DetectLanguageResponse: {0}", json));
            MyMemoryResponse response = json.FromJson<MyMemoryResponse>();
            return response.responseData?.translatedText;
        }
    }
}
