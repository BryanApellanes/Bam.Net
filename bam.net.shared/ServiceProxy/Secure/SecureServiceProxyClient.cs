/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net;
using Bam.Net.Encryption;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Bam.Net.Web;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System.IO;
using System.Reflection;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// A secure service proxy client that uses application level encryption
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SecureServiceProxyClient<T>: ServiceProxyClient<T>
    {
        public SecureServiceProxyClient(string baseAddress)
            : base(baseAddress)
        {
            this.Initialize();
        }
        
        public SecureServiceProxyClient(string baseAddress, string implementingClassName)
            : base(baseAddress, implementingClassName)
        {
            this.Initialize();
        }

        IApiKeyResolver _apiKeyResolver;
        object _apiKeyResolverSync = new object();
        public IApiKeyResolver ApiKeyResolver
        {
            get
            {
                return _apiKeyResolverSync.DoubleCheckLock(ref _apiKeyResolver, () => new ApiKeyResolver());
            }
            set
            {
                _apiKeyResolver = value;
            }
        }

        public Exception SessionStartException
        {
            get;
            private set;
        }

        public bool SessionEstablished
        {
            get
            {
                return SessionInfo != null && SessionInfo.SessionId > 0 && !string.IsNullOrEmpty(SessionInfo.PublicKey);
            }            
        }

        public Cookie SessionCookie
        {
            get;
            protected internal set;
        }

        ClientSessionInfo _sessionInfo;
        public ClientSessionInfo SessionInfo
        {
            get
            {
                return _sessionInfo;
            }
            set
            {
                _sessionInfo = value;
            }
        }
        Type _type;
        /// <summary>
        /// The proxied type
        /// </summary>
        protected Type Type
        {
            get
            {
                if (_type == null)
                {
                    _type = GetType();
                    if (IsSecureServiceProxyClient)
                    {
                        _type = _type.GetGenericArguments().Single();
                    }
                }
                return _type;
            }
        }

        /// <summary>
        /// Return true if the current instance is
        /// a SecureServiceProxyClient and not an
        /// inheriting class instance
        /// </summary>
        protected bool IsSecureServiceProxyClient
        {
            get
            {
                if (Type.Name.Equals("SecureServiceProxyClient`1")) // Can this hackishness be avoided?
                {
                    return true;
                }
                return false;
            }
        }

        protected internal bool TypeRequiresApiKey
        {
            get
            {                
                return Type.HasCustomAttributeOfType<ApiKeyRequiredAttribute>();
            }
        }

        protected internal bool MethodRequiresApiKey(string methodName)
        {
            MethodInfo method = Type.GetMethod(methodName);
            if(method == null)
            {
                return false;
            }
            return method.HasCustomAttributeOfType<ApiKeyRequiredAttribute>();
        }

        /// <summary>
        /// The key for the current session.
        /// </summary>
        protected internal string SessionKey
        {
            get;
            set;
        }

        /// <summary>
        /// The initialization vector for the current session
        /// </summary>
        protected internal string SessionIV
        {
            get;
            set;
        }

        public event Action<SecureServiceProxyClient<T>> SessionStarting;
        protected void OnSessionStarting()
        {
            SessionStarting?.Invoke(this);
        }

        public event Action<SecureServiceProxyClient<T>> SessionStarted;
        protected void OnSessionStarted()
        {
            SessionStarted?.Invoke(this);
        }

        /// <summary>
        /// The event that is raised if an exception occurs starting the 
        /// secure session.
        /// </summary>
        public event Action<SecureServiceProxyClient<T>, Exception> StartSessionException;
        protected void OnStartSessionException(Exception ex)
        {
            StartSessionException?.Invoke(this, ex);
        }

        object _sessionInfoLock = new object();
        public void StartSession()
        {
            if (SessionInfo == null)
            {
                lock (_sessionInfoLock)
                {
                    if (SessionInfo == null)
                    {
                        OnSessionStarting();

                        try
                        {
                            HttpWebRequest request = GetServiceProxyRequest<SecureChannel>(ServiceProxyVerbs.GET, "InitSession", new Instant());

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                SessionCookie = response.Cookies[SecureSession.CookieName];
                                Cookies.Add(SessionCookie);

                                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                                {
                                    SecureChannelMessage<ClientSessionInfo> message = sr.ReadToEnd().FromJson<SecureChannelMessage<ClientSessionInfo>>();
                                    if (!message.Success)
                                    {
                                        throw new Exception(message.Message);
                                    }
                                    else
                                    {
                                        SessionInfo = message.Data;
                                    }
                                }

                                SetSessionKeyAndIv();
                            }
                        }
                        catch (Exception ex)
                        {
                            SessionStartException = ex;
                            OnStartSessionException(ex);
                            return;
                        }

                        OnSessionStarted();
                    }
                }

            }
        }

        protected internal override string DoInvoke(ServiceProxyInvokeEventArgs args)
        {
            string baseAddress = args.BaseAddress;
            string className = args.ClassName;
            string methodName = args.MethodName;
            object[] parameters = args.PostParameters;
            ServiceProxyInvokeEventArgs secureChannelArgs = new ServiceProxyInvokeEventArgs { Cuid = args.Cuid, BaseAddress = baseAddress, ClassName = typeof(SecureChannel).Name, MethodName = "Invoke", PostParameters = new object[] { className, methodName, ApiParameters.ParametersToJsonParamsObjectString(parameters) } };
            try
            {                   
                SecureChannelMessage<string> result = Post(secureChannelArgs).FromJson<SecureChannelMessage<string>>();
                if (result.Success)
                {
                    Decrypted decrypted = new Decrypted(result.Data, SessionKey, SessionIV);
                    return decrypted.Value;
                }
                else
                {
                    string properties = result.PropertiesToString();                    
                    throw new ServiceProxyInvocationFailedException("{0}"._Format(result.Message, properties));
                }
            }
            catch (Exception ex)
            {
                args.Exception = ex;
                args.Message = ex.Message;
                OnInvocationException(args);
            }

            return string.Empty;
        }

        protected override string Post(ServiceProxyInvokeEventArgs invokeArgs, HttpWebRequest request)
        {
            string baseAddress = invokeArgs.BaseAddress;
            string className = invokeArgs.ClassName;
            string methodName = invokeArgs.MethodName;
            object[] parameters = invokeArgs.PostParameters;
            if (className.Equals("securechannel", StringComparison.InvariantCultureIgnoreCase) && methodName.Equals("invoke", StringComparison.InvariantCultureIgnoreCase))
            {
                // the target is the SecureChannel.Invoke method but we
                // need the actual className and method that is in the parameters 
                // object
                string actualClassName = (string)parameters[0];
                string actualMethodName = (string)parameters[1];                
                string jsonParams = (string)parameters[2];
                HttpArgs args = new HttpArgs();
                args.ParseJson(jsonParams);

                if (TypeRequiresApiKey || MethodRequiresApiKey(actualMethodName))
                {
                    ApiKeyResolver.SetKeyToken(request, ApiParameters.GetStringToHash(actualClassName, actualMethodName, args["jsonParams"]));
                }
            }
            return base.Post(invokeArgs, request);
        }

        protected internal override void WriteJsonParams(string jsonParamsString, HttpWebRequest request)
        {
            if (string.IsNullOrEmpty(SessionKey))
            {
                base.WriteJsonParams(jsonParamsString, request);
            }
            else
            {
                Encrypted cipher = new Encrypted(jsonParamsString, SessionKey, SessionIV);
                string postData = cipher.Base64Cipher;
                using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
                {
                    sw.Write(postData);
                }

                ApiEncryptionValidation.SetEncryptedValidationToken(request, jsonParamsString, SessionInfo.PublicKey);

                request.ContentType = "text/plain; charset=utf-8";
            }
        }

        protected internal EncryptedValidationToken CreateValidationToken(string jsonParamsString)
        {
            string publicKeyPem = SessionInfo.PublicKey;

            return CreateEncryptedValidationToken(jsonParamsString, publicKeyPem);
        }

        protected internal override HttpWebRequest GetServiceProxyRequest(ServiceProxyVerbs verb, string className, string methodName, string queryStringParameters = "")
        {
            HttpWebRequest request = base.GetServiceProxyRequest(verb, className, methodName, queryStringParameters);           

            if (SessionCookie == null)
            {
                Logger.AddEntry("Session Cookie ({0}) was missing, call StartSession() first", LogEventType.Warning, SecureSession.CookieName);
            }
            else
            {
                request.Headers.Add(Web.CustomHeaders.SecureSession, SessionCookie.Value);
            }
            if (ClientApplicationNameProvider != null)
            {
                request.Headers[Web.CustomHeaders.ApplicationName] = ClientApplicationNameProvider.GetApplicationName();
            }
            return request;
        }

        protected internal void SetSessionKeyAndIv()
        {
            CreateSetSessionKeyRequest(out AesKeyVectorPair kvp, out SetSessionKeyRequest request);

            SecureChannelMessage response = this.Post<SecureChannelMessage>(typeof(SecureChannel).Name, "SetSessionKey", new object[] { request });
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }

            SessionKey = kvp.Key;
            SessionIV = kvp.IV;
        }

        protected internal void CreateSetSessionKeyRequest(out AesKeyVectorPair kvp, out SetSessionKeyRequest request)
        {
            request = SecureSession.CreateSetSessionKeyInfo(SessionInfo.PublicKey, out kvp);
        }

        private static EncryptedValidationToken CreateEncryptedValidationToken(string jsonParamsString, string publicKeyPem)
        {
            return ApiEncryptionValidation.CreateEncryptedValidationToken(jsonParamsString, publicKeyPem);
        }

        private void Initialize()
        {
            this.InvokingMethod += (s, a) => TryStartSession();
        }

        private void TryStartSession()
        {
            try
            {
                StartSession();
            }
            catch (Exception ex)
            {
                SessionStartException = ex;
            }
        }
    }
}
