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
    public class ApiKeyResolver: IApiKeyProvider, IApplicationNameProvider
    {
        public const string KeyTokenName = "KeyToken";

        public ApiKeyResolver()
        {
            this.ApiKeyProvider = DefaultConfigurationApiKeyProvider.Instance;
            this.ApplicationNameProvider = DefaultConfigurationApplicationNameProvider.Instance;
        }

        public ApiKeyResolver(IApiKeyProvider apiKeyProvider)
            : this()
        {
            this.ApiKeyProvider = apiKeyProvider;
        }

        public ApiKeyResolver(IApplicationNameProvider nameProvider)
            : this()
        {
            this.ApplicationNameProvider = nameProvider;
        }

        public ApiKeyResolver(IApiKeyProvider apiKeyProvider, IApplicationNameProvider nameProvider)
        {
            this.ApiKeyProvider = apiKeyProvider;
            this.ApplicationNameProvider = nameProvider;
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
            headers[KeyTokenName] = CreateToken(stringToHash);
        }

        public string CreateToken(string stringToHash)
        {
            // token is the hash of key/shared secret plus plainpost
            ApiKeyInfo apiKey = this.GetApiKeyInfo(this);
            return CreateToken(apiKey.ApiKey, stringToHash);
        }

        public static string CreateToken(string apiKey, string stringToHash)
        {
            return "{0}:{1}"._Format(apiKey, stringToHash).Sha1();
        }

        public bool IsValid(ExecutionRequest request)
        {
            Args.ThrowIfNull(request, "request");
            ApiKeyResolver resolver = request.ApiKeyResolver;
            ValidateResolver(resolver);
			
            string className = request.ClassName;
            string methodName = request.MethodName;
            string stringToHash = ApiParameters.GetStringToHash(className, methodName, request.JsonParams);

            string token = request.Context.Request.Headers[KeyTokenName];
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

        private static void ValidateResolver(ApiKeyResolver resolver)
        {
            Args.ThrowIfNull(resolver, "ApiKeyResolver");
            Args.ThrowIfNull(resolver.ApplicationNameProvider, "ApiKeyResolver.ApplicationNameProvider");
            Args.ThrowIfNull(resolver.ApiKeyProvider, "ApiKeyResolver.ApiKeyProvider");
        }
    }
}
