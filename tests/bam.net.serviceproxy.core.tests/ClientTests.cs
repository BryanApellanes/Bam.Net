/*
	Copyright © Bryan Apellanes 2015  
*/

using System.Collections.Specialized;
using System.Reflection;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Web;
using Bam.Net.Testing.Unit;

namespace Bam.Net.ServiceProxy.Tests
{
    public partial class ServiceProxyTestContainer
    {
        [UnitTest]
        public void GetProxiedMethodsShouldHaveResults()
        {
            MethodInfo[] methods = ServiceProxySystem.GetProxiedMethods(typeof(EncryptedEcho));
            Expect.IsGreaterThan(methods.Length, 0, "expected more than zero methods");
        }


        class TestApplicationNameProvider : IApplicationNameProvider
        {
            public TestApplicationNameProvider(string name)
            {
                ApplicationCreateResult result = Secure.Application.Create(name);
                Expect.AreEqual(ApplicationCreateStatus.Success, result.Status, result.Message);
                this.Name = name;
            }
            protected string Name
            {
                get;
                set;
            }
            public string GetApplicationName()
            {
                return Name;
            }
        }

        public static void ClearApps()
        {
            ApplicationCollection apps = Secure.Application.LoadAll();
            apps.Delete();
        }

        public static void ClientTestSetup()
        {
            RegisterDb();
            LocalApiKeyManager.Default.UserResolver = new TestUserResolver();
        }

        [UnitTest] // RegisterDb is defined in Program.cs
        public void ApiKey_ShouldCreateToken()
        {
			ClientTestSetup();
            string methodName = MethodBase.GetCurrentMethod().Name;
            ApiKeyResolver resolver = new ApiKeyResolver(new LocalApiKeyProvider(), new TestApplicationNameProvider(methodName));
            string data = "some random data";
            string token = resolver.CreateKeyToken(data);
            Expect.IsNotNullOrEmpty(token, "Token was null or empty");
			ClearApps();
        }

        [UnitTest] // RegisterDb is defined in Program.cs
        public void ApiKey_ShouldSetToken()
        {
			ClientTestSetup();
            string testName = MethodBase.GetCurrentMethod().Name;
            NameValueCollection nvc = new NameValueCollection();
            string data = "Some random data";

            IApiKeyProvider keyProvider = new LocalApiKeyProvider();
            ApiKeyResolver resolver = new ApiKeyResolver(keyProvider, new TestApplicationNameProvider(testName));
            resolver.SetKeyToken(nvc, data);

            Expect.IsNotNullOrEmpty(nvc[Headers.KeyToken], "Key token was not set");
			ClearApps();
        }

        [UnitTest] // RegisterDb is defined in Program.cs
        public void ApiKey_ShouldSetValidToken()
        {
			ClientTestSetup();
            string testName = MethodBase.GetCurrentMethod().Name;
            NameValueCollection nvc = new NameValueCollection();
            string data = "Some random data";
            IApiKeyProvider keyProvider = new LocalApiKeyProvider();
            TestApplicationNameProvider nameProvider = new TestApplicationNameProvider(testName);
            ApiKeyResolver resolver = new ApiKeyResolver(keyProvider, nameProvider);

            resolver.SetKeyToken(nvc, data);

            string token = nvc[Headers.KeyToken];
            bool isValid = resolver.IsValidKeyToken(data, token);
            Expect.IsTrue(isValid, "token was not valid");
			ClearApps();
        }
    }
}
