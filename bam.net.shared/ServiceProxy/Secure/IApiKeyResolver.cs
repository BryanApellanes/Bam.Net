using System.Collections.Specialized;
using System.Net;

namespace Bam.Net.ServiceProxy.Secure
{
    public interface IApiKeyResolver
    {
        HashAlgorithms HashAlgorithm { get; set; }
        /// <summary>
        /// When implemented by a derived class hashes the specified
        /// stringToHash using the key/shared secret
        /// </summary>
        /// <param name="stringToHash"></param>
        /// <returns></returns>
        string CreateKeyToken(string stringToHash);
        ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider);
        string GetApplicationApiKey(string applicationClientId, int index);
        string GetApplicationClientId(IApplicationNameProvider nameProvider);
        string GetApplicationName();
        string GetCurrentApiKey();
        bool IsValidRequest(ExecutionRequest request);
        bool IsValidKeyToken(string stringToHash, string token);
        void SetKeyToken(NameValueCollection headers, string stringToHash);
        void SetKeyToken(HttpWebRequest request, string stringToHash);
    }
}