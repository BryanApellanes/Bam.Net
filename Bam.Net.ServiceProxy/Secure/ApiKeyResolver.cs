/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Configuration;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// A class used to provide the functionality
    /// of both an ApiKeyProvider and an ApplicationNameProvider
    /// </summary>
    public class ApiKeyResolver : IApiKeyProvider, IApplicationNameProvider, IApiKeyResolver
    {
        public ApiKeyResolver()
        {
            ApiKeyProvider = DefaultConfigurationApiKeyProvider.Instance;
            ApplicationNameProvider = DefaultConfigurationApplicationNameProvider.Instance;
            HashAlgorithm = HashAlgorithms.SHA1;
        }

        public ApiKeyResolver(IApiKeyProvider apiKeyProvider)
            : this()
        {
            ApiKeyProvider = apiKeyProvider;
        }

        public ApiKeyResolver(IApplicationNameProvider nameProvider)
            : this()
        {
            ApplicationNameProvider = nameProvider;
        }

        public ApiKeyResolver(IApiKeyProvider apiKeyProvider, IApplicationNameProvider nameProvider) : this()
        {
            ApiKeyProvider = apiKeyProvider;
            ApplicationNameProvider = nameProvider;
        }

        public IApiKeyProvider ApiKeyProvider
        {
            get;
            set;
        }

        public IApplicationNameProvider ApplicationNameProvider
        {
            get;
            set;
        }

        public HashAlgorithms HashAlgorithm { get; set; }

        #region IApiKeyProvider Members

        public ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider)
        {
            return ApiKeyProvider.GetApiKeyInfo(nameProvider);
        }

        public string GetApplicationApiKey(string applicationClientId, int index)
        {
            return ApiKeyProvider.GetApplicationApiKey(applicationClientId, index);
        }

        public string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            return ApiKeyProvider.GetApplicationClientId(nameProvider);
        }

        public string GetCurrentApiKey()
        {
            return ApiKeyProvider.GetCurrentApiKey();
        }

        #endregion

        #region IApplicationNameResolver Members

        public string GetApplicationName()
        {
            return ApplicationNameProvider.GetApplicationName();
        }

        #endregion
        
        public void SetToken(HttpWebRequest request, string stringToHash)
        {
            SetToken(request.Headers, stringToHash);
        }
       
        public void SetToken(NameValueCollection headers, string stringToHash)
        {
            headers[ApiParameters.KeyTokenName] = CreateToken(stringToHash);
        }

        public string CreateToken(string stringToHash)
        {
            ApiKeyInfo apiKey = this.GetApiKeyInfo(this);
            return "{0}:{1}"._Format(apiKey.ApiKey, stringToHash).Hash(HashAlgorithm);
        }

        public bool IsValidRequest(ExecutionRequest request)
        {
            Args.ThrowIfNull(request, "request");
			
            string className = request.ClassName;
            string methodName = request.MethodName;
            string stringToHash = ApiParameters.GetStringToHash(className, methodName, request.JsonParams);

            string token = request.Context.Request.Headers[ApiParameters.KeyTokenName];
            bool result = false;
            if (!string.IsNullOrEmpty(token))
            {
                result = IsValidToken(stringToHash, token);
            }

            return result;
        }
        
        public bool IsValidToken(string stringToHash, string token)
        {
            string checkToken = CreateToken(stringToHash);
            return token.Equals(checkToken);
        }
    }
}
