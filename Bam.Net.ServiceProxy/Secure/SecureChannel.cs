/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Web;
using Bam.Net.Encryption;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// A secure communication channel.  Provides 
    /// application layer encrypted communication
    /// </summary>
    [Proxy("secureChannelServer")]
    public class SecureChannel: IRequiresHttpContext
    {
        static SecureChannel()
        {
            InitializeDatabase();
        }
        [Exclude]
        public object Clone()
        {
            SecureChannel clone = new SecureChannel();
            clone.CopyProperties(this);
            return clone;
        }

        /// <summary>
        /// Ensure that the SecureServiceProxy database is initialized
        /// using the specified logger to output messages
        /// </summary>
        /// <param name="logger"></param>
        protected internal static void InitializeDatabase(ILogger logger = null)
        {
            if (logger == null)
            {
                logger = Log.Default;
            }

            Exception ex = null;
            Config.SchemaInitializer.Initialize(logger, out ex);
            if (ex != null)
            {
                InitializationException = ex;
            }
        }

        ILogger _logger;
        object _loggerSync = new object();
        public ILogger Logger
        {
            get
            {
                return _loggerSync.DoubleCheckLock(ref _logger, () => Log.Default);
            }
            set
            {
                _logger = value;
            }
        }

        public static Exception InitializationException
        {
            get;
            private set;
        }

        static SecureChannelConfig _config;
        static object _configSync = new object();
        public static SecureChannelConfig Config
        {
            get
            {
                return _configSync.DoubleCheckLock(ref _config, () => SecureChannelConfig.Load());
            }
            set
            {
                _config = value;
                _config.Save();
            }
        }

      
        public string TestDecrypt(string cipher, string b64Key, string b64IV)
        {
            string result = "";
            try
            {
                result = Aes.Decrypt(cipher, b64Key, b64IV);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public string TestSessionKey(string cipher)
        {
            string result = "";
            try
            {
                SecureSession session = SecureSession.Get(HttpContext);
                result = session.Decrypt(cipher);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Establish a secure session
        /// </summary>
        /// <returns></returns>
        public SecureChannelMessage<ClientSessionInfo> InitSession(Instant instant)
        {
            SecureSession session = SecureSession.Get(HttpContext, instant);
            ClientSessionInfo result = GetClientSessionInfo(session);

            SetSessionCookie(session);

            return new SecureChannelMessage<ClientSessionInfo>(result);
        }

        private void SetSessionCookie(SecureSession session)
        {
            Cookie sessionCookie = HttpContext.Response.Cookies[ServiceProxySystem.SecureSessionName];
            if (sessionCookie == null)
            {
                HttpContext.Response.Cookies.Add(new Cookie(ServiceProxySystem.SecureSessionName, session.Identifier));
            }
        }

        internal static ClientSessionInfo GetClientSessionInfo(SecureSession session)
        {
            ClientSessionInfo result = new ClientSessionInfo();
            result.SessionId = session.Id.Value;
            result.ClientIdentifier = session.Identifier;
            result.PublicKey = session.PublicKey;
            
            return result;
        }

        public SecureChannelMessage SetSessionKey(SetSessionKeyRequest request)
        {
            SecureChannelMessage result = new SecureChannelMessage(true);
            try
            {
                SecureSession session = SecureSession.Get(HttpContext);
                session.SetSymmetricKey(request);
            }
            catch (Exception ex)
            {
                result = new SecureChannelMessage(ex);
            }

            return result;
        }

        ApiKeyResolver _apiKeyResolver;
        object _apiKeyResolverSync = new object();
        public ApiKeyResolver ApiKeyResolver
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

        public static bool Debug
        {
            get;
            set;
        }

        public SecureChannelMessage<string> Invoke(string className, string methodName, string jsonParams)
        {
            SecureChannelMessage<string> result = new SecureChannelMessage<string>();

            HttpArgs args = new HttpArgs();
            args.ParseJson(jsonParams);
            string parameters = args["jsonParams"];
            SecureExecutionRequest request = new SecureExecutionRequest(HttpContext, className, methodName, parameters);
            request.ApiKeyResolver = ApiKeyResolver;
            request.ServiceProvider = ServiceProvider;
            request.Execute();
            
            ValidationResult validationResult = request.Result as ValidationResult;
            if (Debug && validationResult != null)
            {
                result.Data = "validation failed";
                result.Message = validationResult.Message;
                result.Success = false;
            }
            else
            {
                if (validationResult != null)
                {
                    Logger.AddEntry("Validation failed for SecureChannel.Invoke for {0}.{1}:\r\n *** jsonParams were ***\r\n{2}",
                        LogEventType.Warning,
                        className,
                        methodName,
                        jsonParams);
                }
                
                result.Data = (string)request.Result; //this will throw an exception if validation failed causing 404 not found to be sent back which is what we want for security if debug is off
                result.Success = true;
            }

            return result;
        }

        static Incubator _incubator;
        static object _incubatorSync = new object();
        /// <summary>
        /// The incubator used for SecureChannel requests
        /// </summary>
        public Incubator ServiceProvider
        {
            get
            {
                return _incubatorSync.DoubleCheckLock(ref _incubator, () =>
                {
                    return new Incubator();                    
                });
            }
            set
            {
                _incubator = value;
            }
        }

        public void EndSession(string sessionIdentifier)
        {
            SecureSession session = SecureSession.Get(sessionIdentifier);
            session.Delete();
            Log.AddEntry("EndSession: Session {0} was deleted", sessionIdentifier);
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
