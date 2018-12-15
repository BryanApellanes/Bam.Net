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
using System.CodeDom.Compiler;
using Bam.Net.Configuration;
using Bam.Net.Encryption;
using Bam.Net.CommandLine;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.Data.Schema;
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

            Expect.IsNotNullOrEmpty(nvc[CustomHeaders.KeyToken], "Key token was not set");
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

            string token = nvc[CustomHeaders.KeyToken];
            bool isValid = resolver.IsValidKeyToken(data, token);
            Expect.IsTrue(isValid, "token was not valid");
			ClearApps();
        }
    }
}
