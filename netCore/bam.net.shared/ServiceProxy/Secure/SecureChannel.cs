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
using Bam.Net.Configuration;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// A secure communication channel.  Provides 
    /// application layer encrypted communication
    /// </summary>
    [Proxy("secureChannelServer")]
    public partial class SecureChannel: IRequiresHttpContext
    {
        static SecureChannel()
        {
            InitializeDatabase();
            Debug = DefaultConfiguration.GetAppSetting($"{nameof(SecureChannel)}.Debug", "false").IsAffirmative();
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

            Config.SchemaInitializer.Initialize(logger, out Exception ex);
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
            Cookie sessionCookie = HttpContext.Response.Cookies[SecureSession.CookieName];
            if (sessionCookie == null)
            {
                HttpContext.Response.Cookies.Add(new Cookie(SecureSession.CookieName, session.Identifier));
            }
        }

        internal static ClientSessionInfo GetClientSessionInfo(SecureSession session)
        {
            ClientSessionInfo result = new ClientSessionInfo()
            {
                SessionId = session.Id.Value,
                ClientIdentifier = session.Identifier,
                PublicKey = session.PublicKey
            };
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

        public static bool Debug
        {
            get;
            set;
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
