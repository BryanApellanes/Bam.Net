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
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Web;
using Bam.Net.Testing.Unit;
using NSubstitute;
using Bam.Net.Data.SQLite;

namespace Bam.Net.ServiceProxy.Tests
{
    [Serializable]
    public partial class ServiceProxyTestContainer : CommandLineTestInterface
    {
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");
			DefaultMethod = typeof(ServiceProxyTestContainer).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(ServiceProxyTestContainer).Assembly);
			}
			else
			{
				Interactive();
			}
		}

        [UnitTest("Test HeaderCollection")]
        public void TestHeaderCollection()
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Monkey", "banana");
            headers.Set("Banana", "Monkey");

            headers.Add("Monkey", "AddAgain");
            headers.Set("Monkey", "Set");

            headers.Add("Banana", "AddAgain");
            headers.Set("Banana", "SetAgain");

            headers.Add("AddDoesntExist", "AddDoesntExist");
            headers.Set("SetDoesntExist", "SetDoesntExist");

            foreach (string key in headers.Keys)
            {
                OutLineFormat("{0}={1}", key, headers[key]);
            }
            
        }

        class TestApiKeyClient: SecureServiceProxyClient<string>
        {
            public TestApiKeyClient() : base("") { }

            public WebRequest TestGetWebRequest()
            {
                return GetWebRequest(new Uri("http://test.cxm/"));
            }
            protected override WebRequest GetWebRequest(Uri address)
            {
                return base.GetWebRequest(address);
            }

            public void TestStartSession()
            {
                base.StartSession();
            }
        }

        [AfterEachUnitTest]
        public void StopServersAfterUnitTests()
        {
            ServiceProxyTestHelpers.StopServers();
        }

        [UnitTest]
        public void ApiKeyProviderShouldNotBeNull()
        {
            TestApiKeyClient client = new TestApiKeyClient();
            Expect.IsNotNull(((ApiKeyResolver)client.ApiKeyResolver).ApiKeyProvider, "ApiKeyProvider was null");
        }

        [UnitTest]
        public void SecureServiceProxyClientBaseAddressShouldBeSet()
        {
            string baseAddress = "http://localhost:8989/";
            SecureServiceProxyClient<Echo> sspc = new SecureServiceProxyClient<Echo>(baseAddress);
            Expect.AreEqual(baseAddress, sspc.BaseAddress);
        }

        [UnitTest]
        public void PostShouldFireEvents()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartTestServerGetEchoClient(out server, out sspc);

            bool? firedIngEvent = false;
            bool? firedEdEvent = false;
            
            sspc.Posting += (s, a) =>
            {
                firedIngEvent = true;
            };
            sspc.Posted += (s, assembly) =>
            {
                firedEdEvent = true;
            };
            try
            {
                sspc.Post("TestStringParameter", new object[] { "monkey" }); // array forces resolution to the correct method
            }
            catch (Exception ex)
            {
                server.Stop();
                Expect.Fail(ex.Message);
            }

            server.Stop();

            Expect.IsTrue(firedIngEvent.Value, "Posting event didn't fire");
            Expect.IsTrue(firedEdEvent.Value, "Posted event didn't fire");
        }

        [UnitTest]
        public void PostShouldBeCancelable()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartTestServerGetEchoClient(out server, out sspc);

            bool? firedIngEvent = false;
            bool? firedEdEvent = false;
            sspc.Posting += (s, a) =>
            {
                firedIngEvent = true;
                a.CancelInvoke = true; // should cancel the call
            };
            sspc.Posted += (s,a) =>
            {
                firedEdEvent = true;
            };

            try
            {
                sspc.Post("TestStringParameter", new object[] { "monkey" });
            }
            catch (Exception ex)
            {
                server.Stop();
                Expect.Fail(ex.Message);
            }

            server.Stop();
            
            Expect.IsTrue(firedIngEvent.Value, "Posting didn't fire");
            Expect.IsFalse(firedEdEvent.Value, "Posted fired but should not have");
        }


        [UnitTest]
        public void GetShouldFireEvents()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartTestServerGetEchoClient(out server, out sspc);

            bool? firedIngEvent = false;
            bool? firedEdEvent = false;

            sspc.Getting += (s, a) =>
            {
                firedIngEvent = true;
            };
            sspc.Got += (s, a) =>
            {
                firedEdEvent = true;
            };
            try
            {
                sspc.Get("TestStringParameter", new object[] { "monkey" }); // array forces resolution to the correct method
            }
            catch (Exception ex)
            {
                server.Stop();
                Expect.Fail(ex.Message);
            }
            
            server.Stop();

            Expect.IsTrue(firedIngEvent.Value, "Getting event didn't fire");
            Expect.IsTrue(firedEdEvent.Value, "Got event didn't fire");
        }

        [UnitTest]
        public void SecureServiceProxyInvokeShouldFireSessionStarting()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEchoClient(out server, out sspc);
            bool? sessionStartingCalled = false;
            sspc.SessionStarting += (c) =>
            {
                sessionStartingCalled = true;
            };

            sspc.Invoke<string>("Send", new object[] { "banana" });
            server.Stop();
            Expect.IsTrue(sessionStartingCalled.Value, "SessionStarting did not fire");
        }

        [UnitTest]
        public void SecureServiceProxyInvokeShouldEstablishSessionIfSecureChannelServerRegistered()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEchoClient(out server, out sspc);

            Expect.IsFalse(sspc.SessionEstablished);
            string value = "InputValue_".RandomLetters(8);
            string result = sspc.Invoke<string>("Send", new object[] { value });
            server.Stop();

            string msg = sspc.SessionStartException != null ? sspc.SessionStartException.Message : string.Empty;
            Expect.IsNull(sspc.SessionStartException, "SessionStartException: {0}"._Format(msg));
            Expect.IsTrue(sspc.SessionEstablished, "Session was not established");

            server.Stop();
        }

        [UnitTest]
        public void SecureServiceProxyInvokeShouldSucceed()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEchoClient(out server, out sspc);

            string value = "InputValue_".RandomLetters(8);
            string result = sspc.Invoke<string>("Send", new object[] { value });
            server.Stop();
            Expect.AreEqual(value, result);
        }

        [UnitTest]
        public void OutputTimeValues()
        {
            DateTime now = DateTime.UtcNow;
            OutLineFormat("Month: {0}", now.Month);
            OutLineFormat("Day: {0}", now.Day);
            OutLineFormat("Year: {0}", now.Year);
            OutLineFormat("Hour: {0}", now.Hour);
            OutLineFormat("Minute: {0}", now.Minute);
            OutLineFormat("Second: {0}", now.Second);
            OutLineFormat("Millisecond: {0}", now.Millisecond);
        }

        [UnitTest]
        public void InstantDiffTest()
        {
            Instant now = new Instant();
            Instant then = new Instant(DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(2)));
            Instant fromNow = new Instant(DateTime.UtcNow.AddMinutes(4));

            int thenDiff = now.DiffInMilliseconds(then);
            int fromNowDiff = now.DiffInMilliseconds(fromNow);

            Expect.IsGreaterThan(thenDiff, 0);
            Expect.IsGreaterThan(fromNowDiff, 0);

            OutLineFormat("Then Diff: {0}", thenDiff);
            OutLineFormat("From Now Diff: {0}", fromNowDiff);

            TimeSpan thenSpan = TimeSpan.FromMilliseconds(thenDiff);
            TimeSpan fromNowSpan = TimeSpan.FromMilliseconds(fromNowDiff);

            OutLineFormat("Then minutes diff: {0}", thenSpan.Minutes);
            OutLineFormat("From now minutes diff: {0}", fromNowSpan.Minutes);
        }

        [UnitTest]
        public void InstantToStringParse()
        {
            Instant normalNow = new Instant(DateTime.Now);
            string toStringed = normalNow.ToString();
            OutLineFormat("ToStringed: {0}", toStringed);
            Instant parsed = Instant.FromString(toStringed);

            Expect.AreEqual(0, parsed.DiffInMilliseconds(normalNow));
        }

        [UnitTest]
        public void StartSession()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEchoClient(out server, out sspc);

            sspc.StartSession();
            server.Stop();
        }
        
        [UnitTest]
        public void GetShouldBeCancelable()
        {
            BamServer server;
            SecureServiceProxyClient<Echo> sspc;
            ServiceProxyTestHelpers.StartTestServerGetEchoClient(out server, out sspc);

            bool? firedIngEvent = false;
            bool? firedEdEvent = false;
            sspc.Getting += (s, a) =>
            {
                firedIngEvent = true;
                a.CancelInvoke = true; // should cancel the call
            };
            sspc.Got += (s, a) =>
            {
                firedEdEvent = true;
            };

            try
            {
                sspc.Get("TestStringParameter", new object[] { "monkey" });

            }
            catch (Exception ex)
            {
                server.Stop();
                Expect.Fail(ex.Message);
            }

            server.Stop();

            Expect.IsTrue(firedIngEvent.Value, "Getting didn't fire");
            Expect.IsFalse(firedEdEvent.Value, "Got fired but should not have");
        }


        class TestExecutionRequest: ExecutionRequest
        {
            public TestExecutionRequest(string c, string m, string f)
                : base(c, m, f)
            {
                Incubator inc = new Incubator();
                inc.Set(new Echo());
                ServiceProvider = inc;
                Instance = new Echo();
                MethodInfo = typeof(Echo).GetMethod(m);
                Request = Substitute.For<IRequest>();
                Request.Url.Returns(new Uri($"http://localhost/{c}/{m}.{f}"));
                Request.QueryString.Returns(new System.Collections.Specialized.NameValueCollection());
            }

            public bool Called { get; set; }
            public override ValidationResult Validate()
            {
                Called = true;
                return new ValidationResult();
            }
        }

        [UnitTest]
        public void ExecuteShouldCallValidate()
        {
            TestExecutionRequest to = new TestExecutionRequest("Echo", "Send", "json");
            Expect.IsFalse(to.Called);
            to.Execute();
            Expect.IsTrue(to.Called);
        }

        [UnitTest]
        public void MissingClassShouldReturnClassNotSpecifiedResult()
        {
            ExecutionRequest er = new ExecutionRequest(null, "TestMethod", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailures.ToList().Contains(ValidationFailures.ClassNameNotSpecified));
            OutFormat(result.Message);
        }

        class TestClass
        {
            public void TestMethod(object param)
            {

            }

            public string ShouldWork()
            {
                return "Yay";
            }

            [Local]
            public virtual void LocalMethod()
            {
            }
        }

        [UnitTest]
        public void MissingMethodShouldReturnMethodNotSpecified()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailures.ToList().Contains(ValidationFailures.MethodNameNotSpecified));
            OutLineFormat(result.Message);
        }

        [UnitTest]
        public void LocalMethodShouldNotValidateIfNotLoopback()
        {
            ServiceProxySystem.Register<TestClass>();
            IRequest request = Substitute.For<IRequest>();
            request.QueryString.Returns(new System.Collections.Specialized.NameValueCollection());
            request.UserHostAddress.Returns("192.168.0.80:80");
            IResponse response = Substitute.For<IResponse>();
            IHttpContext context = Substitute.For<IHttpContext>();
            context.Request.Returns(request);
            context.Response.Returns(response);
            ExecutionRequest er = new ExecutionRequest("TestClass", "LocalMethod", "json") { Context = context };
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailures.ToList().Contains(ValidationFailures.RemoteExecutionNotAllowed));
        }

        [UnitTest]
        public void LocalMethodShouldValidateIfLocalClient()
        {
            ServiceProxySystem.Register<TestClass>();
            IRequest request = Substitute.For<IRequest>();
            request.QueryString.Returns(new System.Collections.Specialized.NameValueCollection());
            request.UserHostAddress.Returns("127.0.0.1:80");
            IResponse response = Substitute.For<IResponse>();
            IHttpContext context = Substitute.For<IHttpContext>();
            context.Request.Returns(request);
            context.Response.Returns(response);
            ExecutionRequest er = new ExecutionRequest("TestClass", "LocalMethod", "json") { Context = context };
            ValidationResult result = er.Validate();
            Expect.IsTrue(result.Success);
        }

        [UnitTest]
        public void UnregisteredClassShoudReturnClassNotRegistered()
        {
			ServiceProxySystem.Unregister<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "ShouldWork", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailures.ToList().Contains(ValidationFailures.ClassNotRegistered));
            OutLineFormat(result.Message);
        }

        [UnitTest]
        public void MethodNotFoundShouldBeReturned()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "MissingMethod", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailures.ToList().Contains(ValidationFailures.MethodNotFound));
            OutLineFormat(result.Message);
        }

        [UnitTest]
        public void ParameterCountMismatchShouldBeReturned()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "TestMethod", "json");
            er.Arguments = new object[] { new { }, new { } };
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailures.ToList().Contains(ValidationFailures.ParameterCountMismatch));
            OutLineFormat(result.Message);
        }

        [UnitTest]
        public void ExecuteShouldSucceed()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "ShouldWork", "json");
            ValidationResult result = er.Validate();            
            Expect.IsTrue(result.Success);
            er.Execute();
            Expect.IsTrue(er.Result.Equals("Yay"));
            OutLineFormat(er.Result.ToString());
        }

        [UnitTest]
        public void ShouldHaveEncryptAndApiKeyRequired()
        {
            Type type = typeof(ApiKeyRequiredEcho);
            Expect.IsTrue(type.HasCustomAttributeOfType<EncryptAttribute>());
            Expect.IsTrue(type.HasCustomAttributeOfType<ApiKeyRequiredAttribute>());
        }

        public class InvalidKeyProvider: ApiKeyProvider
        {
            public override string GetApplicationClientId(IApplicationNameProvider nameProvider)
            {
                return "InvalidClientId_".RandomLetters(8);
            }

            public override string GetApplicationApiKey(string applicationClientId, int index)
            {
                return "InvalidApiKey_".RandomLetters(4);
            }
        }

        [UnitTest]
        public void SecureServiceProxyInvokeWithInvalidTokenShouldFail()
        {
			CleanUp();
            BamServer server;
            SecureChannel.Debug = true;
            SecureServiceProxyClient<ApiKeyRequiredEcho> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetApiKeyRequiredEchoClient(out server, out sspc);
            
            string value = "InputValue_".RandomLetters(8);
            bool? thrown = false;
            sspc.InvocationException += (client, ex) =>
            {
                thrown = true;
            };

            ApiKeyResolver resolver = new ApiKeyResolver(new InvalidKeyProvider());
            sspc.ApiKeyResolver = resolver;
            string result = sspc.Invoke<string>("Send", new object[] { value });            

            Expect.IsTrue(thrown.Value);
			CleanUp();
        }

        [UnitTest]
        public void StaticApiKeyProviderShouldAlwaysReturnSameKey()
        {
            string id = "TheClientIdGoesHere";
            string key = "The Api Key Goes Here";
            StaticApiKeyProvider apiKeyProvider = new StaticApiKeyProvider(id, key);

            ApiKeyInfo keyInfo = apiKeyProvider.GetApiKeyInfo(new DefaultConfigurationApplicationNameProvider());
            
            Expect.AreEqual(id, keyInfo.ApplicationClientId);
            Expect.AreEqual(key, keyInfo.ApiKey);
        }
		
        [UnitTest]
        public void SecureServiceProxyInvokeWithApiKeyShouldSucceed()
        {
			CleanUp();
            string methodName = MethodBase.GetCurrentMethod().Name;
            BamServer server;
            SecureChannel.Debug = true;
            
            string baseAddress;
            ServiceProxyTestHelpers.CreateServer(out baseAddress, out server);
            ServiceProxyTestHelpers.Servers.Add(server); // makes sure it gets stopped after test run
            SecureServiceProxyClient<ApiKeyRequiredEcho> sspc = new SecureServiceProxyClient<ApiKeyRequiredEcho>(baseAddress);

            IApplicationNameProvider nameProvider = new TestApplicationNameProvider(methodName);
            IApiKeyProvider keyProvider = new LocalApiKeyProvider();
            ApiKeyResolver keyResolver = new ApiKeyResolver(keyProvider, nameProvider);

            SecureChannel channel = new SecureChannel();
            channel.ApiKeyResolver = keyResolver;

            server.AddCommonService<SecureChannel>(channel);
            server.AddCommonService<ApiKeyRequiredEcho>();

            server.Start();

            string value = "InputValue_".RandomLetters(8);
            bool? thrown = false;
            sspc.InvocationException += (client, ex) =>
            {
                thrown = true;
            };

            sspc.ApiKeyResolver = keyResolver;
            string result = sspc.Invoke<string>("Send", new object[] { value });
            
            Expect.IsFalse(thrown.Value, "Exception was thrown");
            Expect.AreEqual(value, result);
			CleanUp();
        }


        [UnitTest]
        public void ApiKey_ExecutionRequestShouldValidateApiKey()
        {
			RegisterDb();
            ServiceProxySystem.Register<ApiKeyRequiredEcho>();
            string testName = MethodBase.GetCurrentMethod().Name;
            IApplicationNameProvider nameProvider = new TestApplicationNameProvider(testName.RandomLetters(6));
            IApiKeyProvider keyProvider = new LocalApiKeyProvider();

            ExecutionRequest er = new ExecutionRequest("ApiKeyRequiredEcho", "Send", "json");
            er.ApiKeyResolver = new ApiKeyResolver(keyProvider, nameProvider);

            er.Request = new ServiceProxyTestHelpers.JsonTestRequest();
            string data = ApiParameters.ParametersToJsonParamsObjectString("some random data");
            er.InputString = data;

            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            List<ValidationFailures> failures = new List<ValidationFailures>(result.ValidationFailures);
            Expect.IsTrue(failures.Contains(ValidationFailures.InvalidApiKeyToken));
        }

        [UnitTest]
        public void ApiKey_ExecutionRequestValidationShouldSucceedIfGoodToken()
        {
			Prepare();
            ServiceProxySystem.Register<ApiKeyRequiredEcho>();

            string methodName = MethodBase.GetCurrentMethod().Name;
            IApplicationNameProvider nameProvider = new TestApplicationNameProvider(methodName);
            IApiKeyProvider keyProvider = new LocalApiKeyProvider();

            string className = "ApiKeyRequiredEcho";
            string method= "Send";
            string data = ApiParameters.ParametersToJsonParamsArray("some random data").ToJson();
            ExecutionRequest er = new ExecutionRequest(className, method, "json");
            er.JsonParams = data;
            er.ApiKeyResolver = new ApiKeyResolver(keyProvider, nameProvider);
            er.Request = new ServiceProxyTestHelpers.FormUrlEncodedTestRequest();   
            
            er.ApiKeyResolver.SetKeyToken(er.Request.Headers, ApiParameters.GetStringToHash(className, method, data));

            ValidationResult result = er.Validate();
            Expect.IsTrue(result.Success);
        }

        [UnitTest]
        public void ApiKey_ExecutionRequestValidationShouldFailIfBadToken()
        {
			RegisterDb();
            ServiceProxySystem.Register<ApiKeyRequiredEcho>();

            string methodName = MethodBase.GetCurrentMethod().Name;
            IApplicationNameProvider nameProvider = new TestApplicationNameProvider(methodName.RandomLetters(4));
            IApiKeyProvider keyProvider = new LocalApiKeyProvider();

            ExecutionRequest er = new ExecutionRequest("ApiKeyRequiredEcho", "Send", "json")
            {
                ApiKeyResolver = new ApiKeyResolver(keyProvider, nameProvider),
                Request = new ServiceProxyTestHelpers.JsonTestRequest()
            };
            string data = ApiParameters.ParametersToJsonParamsObjectString("some random data");
            er.InputString = data;
            ApiKeyResolver resolver = new ApiKeyResolver(keyProvider, nameProvider);
            resolver.SetKeyToken(er.Request.Headers, data);

            er.Request.Headers[CustomHeaders.KeyToken] = "bad token value";

            ValidationResult result = er.Validate();
            
            Expect.IsFalse(result.Success, "Validation should have failed");
            List<ValidationFailures> failures = new List<ValidationFailures>(result.ValidationFailures);            
            Expect.IsTrue(failures.Contains(ValidationFailures.InvalidApiKeyToken), "ValidationFailure should have been InvalidApiKeyToken");
        }

        static bool? _registeredDb;
        public static void RegisterDb()
        {
            SQLiteDatabase db = new SQLiteDatabase();
            Db.For<Secure.Application>(db);
            Db.For<Account>(UserAccountsDatabase.Default);
            Db.TryEnsureSchema<Secure.Application>(db);
            SQLiteRegistrar.Register<Secure.Application>();
            _registeredDb = true;
        }

        [UnitTest]
        public void ShouldBeAbleToCreateApplication()
        {
			RegisterDb();
            Expect.IsTrue(_registeredDb.Value);
            ApplicationCreateResult result = CreateTestApp();
            Expect.AreEqual(ApplicationCreateStatus.Success, result.Status);
            Expect.IsNotNull(result.Application);
            Expect.IsNullOrEmpty(result.Message);
        }

        [UnitTest]
        public void ShouldReturnNameInUseIfAppNameInUse()
        {
			RegisterDb();
            ApplicationCreateResult first = CreateTestApp();
            ApplicationCreateResult second = Secure.Application.Create(first.Application.Name);
            Expect.AreEqual(ApplicationCreateStatus.NameInUse, second.Status);
        }

        private static ApplicationCreateResult CreateTestApp()
        {
            LocalApiKeyManager.Default.UserResolver = new TestUserResolver();
            ApplicationCreateResult result = Secure.Application.Create("Test_AppName_".RandomLetters(6));
            return result;
        }

        [UnitTest]
        public void ApplicationShouldHaveKey()
        {
			RegisterDb();
            Expect.IsTrue(_registeredDb.Value);
            ApplicationCreateResult result = CreateTestApp();
            Expect.IsNotNull(result.Application);
            Expect.IsTrue(result.Application.ApiKeysByApplicationId.Count > 0);
        }

        [UnitTest]
        public void TimeGenerateVsNewGuid()
        {
            string generatedId;
            TimeSpan generateTime = Exec.Time(() =>
            {
                generatedId = ServiceProxySystem.GenerateId();
            });

            string guid;
            TimeSpan guidTime = Exec.Time(() =>
            {
                guid = Guid.NewGuid().ToString();
            });

            string sha256;
            TimeSpan guidTimeSha256 = Exec.Time(() =>
            {
                sha256 = Guid.NewGuid().ToString().Sha256();
            });

            OutLineFormat("Generate took: {0}", generateTime.ToString());
            OutLineFormat("-NewGuid took: {0}", guidTime.ToString());
            OutLineFormat("HashGuid took: {0}", guidTimeSha256.ToString());
        }

        [ConsoleAction("Output Keys")]
        public void GetPublicKeyXml()
        {
            OutLine("Public Key", ConsoleColor.Cyan);
            OutLine(RsaKeyPair.Default.PublicKeyXml, ConsoleColor.Green);
            OutLine("Private Key", ConsoleColor.Cyan);
            OutLine(RsaKeyPair.Default.PrivateKeyXml, ConsoleColor.Yellow);
        }

        [ConsoleAction]
        public void LookAtGeneratedRsaPemKeys()
        {
            AsymmetricCipherKeyPair keys = RsaKeyGen.GenerateKeyPair(RsaKeyLength._1024);
            OutLine(keys.Public.ToPem());
        }

        [ConsoleAction]
        public void OutputDaoUsersContextAssemblyQaulifiedName()
        {
            string name = typeof(UserAccountsContext).AssemblyQualifiedName;
            Out(name);
            name.SafeWriteToFile(".\\UserAccountsContext", true);
            "notepad .\\UserAccountsContext".Run();
        }
    }

}
