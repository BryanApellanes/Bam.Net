/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Javascript;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
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
using FakeItEasy;
using FakeItEasy.Creation;
using Bam.Net.Web;
using Bam.Net.Testing.Unit;

namespace Bam.Net.ServiceProxy.Tests
{
    public partial class ServiceProxyTestContainer
    {
        [UnitTest]
        public void Securesession_ShouldBeAbleToEncryptAndDecryptWithSecureSession()
        {
            SecureChannelConfig config = new SecureChannelConfig();
            Exception ex;
            config.SchemaInitializer.Initialize(new ConsoleLogger(), out ex);
            Expect.IsNull(ex);

            SecureSession testObject = SecureSession.Get(new ServiceProxyTestHelpers.FormUrlEncodedTestRequest(), A.Fake<IResponse>());
            string data = "Monkey";
            string cipher = testObject.EncryptWithPublicKey(data);
            string decrypted = testObject.DecryptWithPrivateKey(cipher);
            Expect.AreEqual(data, decrypted);
        }
        
        [UnitTest]
        public void Securesession_ShouldBeAbleToGetSecureSession()
        {
            ConsoleLogger logger = new ConsoleLogger();
            SecureChannel.InitializeDatabase(logger);
            Cookie cookie = new Cookie(SecureSession.CookieName, "TestSecureSessionId");
            SecureSession session = SecureSession.Get(cookie);
            Expect.IsNotNull(session);
        }

        [UnitTest]
        public void Securesession_StartSessionShouldCreateSecureSessionEntry()
        {
            SecureChannel server = new SecureChannel();
            server.HttpContext = A.Fake<IHttpContext>();
            server.HttpContext.Request = new ServiceProxyTestHelpers.FormUrlEncodedTestRequest();

            SecureChannelMessage<ClientSessionInfo> message = server.InitSession(new Instant());
            ClientSessionInfo sessionInfo = message.Data;

            SecureSession created = SecureSession.OneWhere(c => c.Id == sessionInfo.SessionId, new SecureSessionDatabase());
            Expect.IsNotNull(created);
            Expect.IsNotNullOrEmpty(created.Identifier, "Identifier was null or empty");
            Expect.AreEqual(created.Identifier, sessionInfo.ClientIdentifier, "ClientIdentifiers didn't match");
            Expect.AreEqual(sessionInfo.PublicKey, created.PublicKey);
        }

        [UnitTest]
        public void Securesession_ShouldGetTheSameSession()
        {
            SecureChannel server = new SecureChannel();
            server.HttpContext = A.Fake<IHttpContext>();
            server.HttpContext.Request = new ServiceProxyTestHelpers.FormUrlEncodedTestRequest();

            SecureChannelMessage<ClientSessionInfo> one = server.InitSession(new Instant());
            SecureChannelMessage<ClientSessionInfo> two = server.InitSession(new Instant());

            Expect.AreEqual(one.Data.SessionId, two.Data.SessionId, "Session Ids didn't match");
        }

        [UnitTest]
        public void Securesession_ShouldBeAbleToSetValidationToken()
        {
            ConsoleLogger logger = new ConsoleLogger();
            SecureChannel.InitializeDatabase(logger);

            IRequest request = CreateFakeRequest();
            SecureSession session = SecureSession.Get(request);
            ApiEncryptionValidation.SetEncryptedValidationToken(request.Headers, "Some random data", session.PublicKey);

            Expect.IsNotNull(request.Headers[CustomHeaders.ValidationToken]);

            OutLine(request.Headers[CustomHeaders.ValidationToken]);
        }

        private static IRequest CreateFakeRequest()
        {
            IRequest request = A.Fake<IRequest>();
            A.CallTo(() => request.Headers).Returns(new NameValueCollection());
            A.CallTo(() => request.Cookies).Returns(new CookieCollection());
            Cookie sessionCookie = new Cookie(SecureSession.CookieName, SecureSession.GenerateId());
            request.Cookies.Add(sessionCookie);
            return request;
        }

        [UnitTest]
        public void Securesession_ShouldBeAbleToSetSessionKey()
        {
            InitializeSecureChannelSchema();

            SecureChannel server = new SecureChannel();
            server.HttpContext = A.Fake<IHttpContext>();
            server.HttpContext.Request = new ServiceProxyTestHelpers.FormUrlEncodedTestRequest();
            SecureChannelMessage<ClientSessionInfo> msg = server.InitSession(new Instant());

            AesKeyVectorPair kvp;
            SetSessionKeyRequest request;
            SecureServiceProxyClient<Echo> client = new SecureServiceProxyClient<Echo>("http://localhost:8080");
            client.SessionInfo = msg.Data;
            client.CreateSetSessionKeyRequest(out kvp, out request);

            server.SetSessionKey(request);
        }

        [UnitTest]
        public void ShouldBeAbleToEncryptAndDecryptWithSecureSession()
        {
            InitializeSecureChannelSchema();

            SecureSession testObject = SecureSession.Get(new ServiceProxyTestHelpers.FormUrlEncodedTestRequest(), A.Fake<IResponse>());
            string data = "Monkey";
            string cipher = testObject.EncryptWithPublicKey(data);
            string decrypted = testObject.DecryptWithPrivateKey(cipher);
            Expect.AreEqual(data, decrypted);
        }

        private static void InitializeSecureChannelSchema()
        {
            SecureChannelConfig config = new SecureChannelConfig();
            Exception ex;
            config.SchemaInitializer.Initialize(new ConsoleLogger(), out ex);
            Expect.IsNull(ex);
        }
    }
}
