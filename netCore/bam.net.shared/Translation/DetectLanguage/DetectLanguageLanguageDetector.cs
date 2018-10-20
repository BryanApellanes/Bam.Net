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
        private static string ApiKeyKey = "DetectLanguageApiKey";
        private static string ApiDetectEndpointFormat = "http://ws.detectlanguage.com/0.2/detect?q={Text}&key={ApiKey}";
        public DetectLanguageLanguageDetector(Vault apiKeyVault = null, ILogger logger = null)
        {
            LanguageDatabase = Bam.Net.Translation.LanguageDatabase.Default;
            Logger = logger ?? Log.Default;
            ApiKeyVault = apiKeyVault ?? Vault.System;
        }

        public Database LanguageDatabase { get; set; }
        public ILogger Logger { get; set; }


        protected Vault ApiKeyVault { get; set; }
        object _apiKeyLock = new object();
        string _apiKey;
        private string ApiKey
        {
            get
            {
                return _apiKeyLock.DoubleCheckLock(ref _apiKey, () => ApiKeyVault.Get(ApiKeyKey));
            }
        }
    }
}
