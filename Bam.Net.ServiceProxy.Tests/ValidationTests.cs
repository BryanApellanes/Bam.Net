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

namespace Bam.Net.ServiceProxy.Tests
{
    public partial class ServiceProxyTestContainer
    {
        [UnitTest]
        public void Validation_ShouldBeAbleToCreateToken()
        {
            Prepare();
            
            IHttpContext context = CreateFakeContext(MethodBase.GetCurrentMethod().Name);
            SecureSession session = SecureSession.Get(context);
            string postString = ApiParameters.ParametersToJsonParamsObject("random information");

            EncryptedValidationToken token = ApiEncryptionValidation.CreateEncryptedValidationToken(postString, session);
        }

        public static void Prepare()
        {
            ConsoleLogger logger = new ConsoleLogger();
            SecureChannel.InitializeDatabase(logger);
            RegisterDb();
            ClearApps();
        }

        [UnitTest]
        public void Validation_ShouldBeAbleToValidateToken()
        {
            Prepare();

            IHttpContext context = CreateFakeContext(MethodBase.GetCurrentMethod().Name);
            SecureSession session = SecureSession.Get(context);
            string postString = ApiParameters.ParametersToJsonParamsObject("random information");

            EncryptedValidationToken token = ApiEncryptionValidation.CreateEncryptedValidationToken(postString, session);

            Expect.AreEqual(EncryptedTokenValidationStatus.Success, ApiEncryptionValidation.ValidateEncryptedToken(session, token, postString));
        }

        [UnitTest]
        public void Validation_ShouldBeAbleToSetAndValidateValidationToken()
        {
            Prepare();

            SecureSession session = SecureSession.Get(SecureSession.GenerateId());

            string postString = ApiParameters.ParametersToJsonParamsObject("random info");
            SecureServiceProxyClient<Echo> client = new SecureServiceProxyClient<Echo>("http://blah.com");

            HttpWebRequest request = client.GetServiceProxyRequest("Send");
            ApiEncryptionValidation.SetEncryptedValidationToken(request.Headers, postString, session.PublicKey);

            Cookie cookie = new Cookie(SecureSession.CookieName, session.Identifier, "", "blah.cxm");            
            request.CookieContainer.Add(cookie);
            request.Headers[BamHeaders.SecureSession] = session.Identifier;
        
            Expect.IsNotNull(request.Headers);
            Expect.IsNotNull(request.Headers[BamHeaders.Nonce]);
            Expect.IsNotNull(request.Headers[BamHeaders.ValidationToken]);

            Expect.AreEqual(EncryptedTokenValidationStatus.Success, ApiEncryptionValidation.ValidateEncryptedToken(request.Headers, postString));
        }

        [UnitTest]
        public void Validation_ValidateNonceShouldFailIfTooOld()
        {
            Prepare();

            DateTime tenMinutesAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(10));
            Instant nonce = new Instant(tenMinutesAgo);
            EncryptedTokenValidationStatus status = ApiEncryptionValidation.ValidateNonce(nonce.ToString(), 5);
            Expect.IsFalse(status == EncryptedTokenValidationStatus.Success);
            Expect.AreEqual(EncryptedTokenValidationStatus.NonceFailed, status);
        }
    }
}
