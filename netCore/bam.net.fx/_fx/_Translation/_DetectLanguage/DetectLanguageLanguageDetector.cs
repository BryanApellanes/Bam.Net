using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.Web;

namespace Bam.Net.Translation.DetectLanguage
{
    /// <summary>
    /// A language detector that uses 
    /// detectlanguage.com
    /// </summary>
    public partial class DetectLanguageLanguageDetector : ILanguageDetector
    {
        public Language DetectLanguage(string text)
        {
            var args = new { ApiKey = ApiKey.Or("demo"), Text = text };
            string json = Http.Get(ApiDetectEndpointFormat.NamedFormat(args));
            Task.Run(() => Logger.AddEntry("DetectLanguageResponse: {0}", json));
            DetectLanguageResponse response = json.FromJson<DetectLanguageResponse>();
            return response.GetLanguage(LanguageDatabase);
        }
    }
}
