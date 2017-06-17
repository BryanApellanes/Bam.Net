/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Bam.Net.Configuration;
using Bam.Net.Encryption;
using Bam.Net.CommandLine;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Javascript;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using System.Collections.Specialized;

namespace Bam.Net.ServiceProxy.Tests
{
    /// <summary>
    /// A helper class to manage server lifecycles in tests
    /// </summary>
    public class ServiceProxyTestHelpers
    {
        public class TestRequest : IRequest
        {
            public TestRequest()
            {
                this.Headers = new NameValueCollection();
                this.QueryString = new NameValueCollection();
                this.Cookies = new CookieCollection();
                Cookie sessionCookie = new Cookie(SecureSession.CookieName, "0368c7fde0a40272d42e14e224d37761dbccef665116ccb063ae31aaa7708d72");
                this.Cookies.Add(sessionCookie);
            }

            #region IRequest
            public string[] AcceptTypes
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public Encoding ContentEncoding
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public long ContentLength64
            {
                get { throw new NotImplementedException(); }
            }

            public int ContentLength
            {
                get { throw new NotImplementedException(); }
            }

            public System.Collections.Specialized.NameValueCollection QueryString
            {
                get;
                set;
            }

            public string ContentType
            {
                get { return "application/x-www-form-urlencoded; charset=utf-8"; }
            }

            public CookieCollection Cookies
            {
                get;
                set;
            }

            NameValueCollection _headers;
            public NameValueCollection Headers
            {
                get { return _headers; }
                set
                {
                    _headers = value;
                }
            }

            public string HttpMethod
            {
                get { throw new NotImplementedException(); }
            }

            public Stream InputStream
            {
                get { return null; }
            }

            public Uri Url
            {
                get { throw new NotImplementedException(); }
            }

            public Uri UrlReferrer
            {
                get { throw new NotImplementedException(); }
            }

            public string UserAgent
            {
                get { throw new NotImplementedException(); }
            }

            public string UserHostAddress
            {
                get { throw new NotImplementedException(); }
            }

            public string UserHostName
            {
                get { throw new NotImplementedException(); }
            }

            public string[] UserLanguages
            {
                get { throw new NotImplementedException(); }
            }

            public string RawUrl
            {
                get { throw new NotImplementedException(); }
            }

            #endregion
        }

        public static void StopServers()
        {
            Servers.Each(server =>
            {
                server.Stop();
            });
        }

        #region helpers
        static List<BamServer> _servers = new List<BamServer>();
        public static List<BamServer> Servers
        {
            get
            {
                return _servers;
            }
        }

        // TODO: this crap needs to be refactored, oh my
        public static void StartTestServerGetEchoClient(out BamServer server, out SecureServiceProxyClient<Echo> sspc)
        {
            string baseAddress;

            StartTestServer(out baseAddress, out server);
            sspc = new SecureServiceProxyClient<Echo>(baseAddress);
        }

        public static void StartSecureChannelTestServerGetApiKeyRequiredEchoClient(out BamServer server, out SecureServiceProxyClient<ApiKeyRequiredEcho> sspc)
        {
            string baseAddress;
            StartTestServer<SecureChannel, ApiKeyRequiredEcho>(out baseAddress, out server);
            sspc = new SecureServiceProxyClient<ApiKeyRequiredEcho>(baseAddress);
        }

        public static void StartSecureChannelTestServerGetEchoClient(out BamServer server, out SecureServiceProxyClient<Echo> sspc)
        {
            string baseAddress;
            StartTestServer<SecureChannel, Echo>(out baseAddress, out server);
            sspc = new SecureServiceProxyClient<Echo>(baseAddress);
        }

        public static void StartSecureChannelTestServerGetEncryptedEchoClient(out BamServer server, out SecureServiceProxyClient<EncryptedEcho> sspc)
        {
            string baseAddress;
            StartTestServer<SecureChannel, EncryptedEcho>(out baseAddress, out server);
            sspc = new SecureServiceProxyClient<EncryptedEcho>(baseAddress);
        }

        public static void StartSecureChannelTestServerGetClient<T>(out BamServer server, out SecureServiceProxyClient<T> sspc)
        {
            string baseAddress;
            StartTestServer<SecureChannel, T>(out baseAddress, out server);
            sspc = new SecureServiceProxyClient<T>(baseAddress);
        }

        public static void StartTestServer(out string baseAddress, out BamServer server)
        {
            StartTestServer<Echo>(out baseAddress, out server);
        }
        public static void StartTestServer<T>(out string baseAddress, out BamServer server)
        {
            InjectTestConfiguration();
            // Test server to catch calls
            CreateServer(out baseAddress, out server);
            server.AddCommonService<T>();
            Servers.Add(server);
            server.Start();
            // /end- Test server to catch calls
        }
        public static void StartTestServer<T1, T2>(out string baseAddress, out BamServer server)
        {
            InjectTestConfiguration();
            // Test server to catch calls
            CreateServer(out baseAddress, out server);
            server.AddCommonService<T1>();
            server.AddCommonService<T2>();
            //SecureChannel.ServiceProvider = server.CommonServiceProvider.Clone();
            Servers.Add(server);
            server.Start();
            // /end- Test server to catch calls
        }
        public static void CreateServer(out string baseAddress, out BamServer server)
        {
            BamConf conf = new BamConf();
            baseAddress = "http://localhost:8080"; // this works because of the contents of C:\BamContent\apps\localhost
            server = new BamServer(conf);
        }

        public static void AddService<T>(BamServer server)
        {
            server.AddCommonService<T>();
        }

        public static void InjectTestConfiguration()
        {
            string ignore, ignore2;
            InjectTestConfiguration(out ignore, out ignore2);
        }
        public static void InjectTestConfiguration(out string injectedClientId, out string injectedApiKey)
        {
            // This is injecting test settings into the DefaultConfiguration
            Dictionary<string, string> testAppSettings = new Dictionary<string, string>();
            injectedClientId = "TestClientIdValue_".RandomLetters(16);
            injectedApiKey = "TestApiKeyValue_".RandomLetters(16);
            testAppSettings.Add("ClientId", injectedClientId);
            testAppSettings.Add("ApiKey", injectedApiKey);
            DefaultConfiguration.SetAppSettings(testAppSettings);
            // /end This is injecting test settings into the DefaultConfiguration
        }
        #endregion

    }
}
