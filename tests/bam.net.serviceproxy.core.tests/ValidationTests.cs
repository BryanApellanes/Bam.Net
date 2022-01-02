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
using Bam.Net.Data.SQLite;
using Bam.Net.Server.ServiceProxy;

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
            string postString = null; // TODO: fix this //  ApiArgumentEncoder.ParametersToJsonParamsObjectString("random information");

            EncryptedValidationToken token = ApiEncryptionValidation.CreateEncryptedValidationToken(postString, session);
        }

        public static void Prepare()
        {
            RegisterDb();
            Db.For<UserAccounts.Data.Account>(UserAccounts.UserAccountsDatabase.Default);
            ConsoleLogger logger = new ConsoleLogger();
            //SecureChannel.InitializeDatabase(logger);            
            ClearApps();
        }

/*        [UnitTest]
        public void Validation_ShouldBeAbleToValidateToken()
        {
            Prepare();

            IHttpContext context = CreateFakeContext(MethodBase.GetCurrentMethod().Name);
            SecureSession session = SecureSession.Get(context);
            string postString = null; // TODO: fix this // ApiArgumentEncoder.ParametersToJsonParamsObjectString("random information");

            EncryptedValidationToken token = ApiEncryptionValidation.CreateEncryptedValidationToken(postString, session);

            Expect.AreEqual(EncryptedTokenValidationStatus.Success, ApiEncryptionValidation.ValidateEncryptedToken(session, token, postString));
        }*/

/*        [UnitTest]
        public void Validation_ShouldBeAbleToSetAndValidateValidationTokenHttpHeaders()
        {
            Prepare();

            SecureSession session = SecureSession.Get(SecureSession.GenerateId());

            string postString = null; // TODO: fix this // ApiArgumentEncoder.ParametersToJsonParamsObjectString("random info");
            SecureServiceProxyClient<Echo> client = new SecureServiceProxyClient<Echo>("http://blah.com");

            HttpWebRequest request = null;// client.GetServiceProxyRequest("Send");
            ApiEncryptionValidation.SetEncryptedValidationToken(request.Headers, postString, session.PublicKey);

            Cookie cookie = new Cookie(SecureSession.CookieName, session.Identifier, "", "blah.cxm");
            request.CookieContainer.Add(cookie);
            request.Headers[Headers.SecureSessionId] = session.Identifier;

            Expect.IsNotNull(request.Headers);
            Expect.IsNotNull(request.Headers[Headers.Nonce]);
            Expect.IsNotNull(request.Headers[Headers.ValidationToken]);

            Expect.AreEqual(EncryptedTokenValidationStatus.Success, ApiEncryptionValidation.ValidateEncryptedToken(request.Headers, postString));
        }*/

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

        static string _failureMessage = "I will never let it happen";
        public class DontAllowAttribute : RequestFilterAttribute
        {
            public override bool RequestIsAllowed(ServiceProxyInvocation request, out string failureMessage)
            {
                failureMessage = _failureMessage;
                return false;
            }
        }

        public class ClassWithTestFilterOnAMethod
        {
            [DontAllow]
            public void Method()
            {

            }
        }

        [UnitTest]
        public void Validation_FailsBecauseRequestFilter()
        {
            string url = "http://blah.com/ClassWithTestFilterOnAMethod/Method.json";
            RequestWrapper req = new RequestWrapper(new { Headers = new NameValueCollection(), Url = new Uri(url), HttpMethod = "GET", ContentLength = 0, QueryString = new NameValueCollection() });
            ResponseWrapper res = new ResponseWrapper(new object());

            ServiceProxyInvocation execRequest = new ServiceProxyInvocation();// req, res);
            execRequest.WebServiceRegistry.Set(typeof(ClassWithTestFilterOnAMethod), new ClassWithTestFilterOnAMethod());
            ServiceProxyInvocationValidationResult validation = execRequest.Validate();
            Expect.AreEqual(_failureMessage, validation.Message);
            Expect.IsFalse(validation.Success);
        }
    }
}
