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
using Bam.Net.Logging;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using FakeItEasy;
using FakeItEasy.Creation;
using System.Reflection;
using Bam.Net.Web;
using Bam.Net.Testing.Unit;

namespace Bam.Net.ServiceProxy.Tests
{
    public partial class ServiceProxyTestContainer
    {
        static ServiceProxyTestContainer()
        {
            FilesToDelete = new List<string>();

        }

		public static void ClearAppsAndStopServers()
		{
			ClearApps();
            ServiceProxyTestHelpers.StopServers();
		}

        public static void CleanUp()
        {
            FilesToDelete.Each(file =>
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            });

            Prepare();
            ApiKeyCollection keys = ApiKey.LoadAll();
            keys.Delete();
            SecureSessionCollection sessions = SecureSession.LoadAll();
            sessions.Delete();

            ApplicationCollection all = Secure.Application.LoadAll();
            all.Delete();
			ClearAppsAndStopServers();
        }

        public void EnsureRepository()
        {
            ConsoleLogger logger = new ConsoleLogger();
            SecureChannel.InitializeDatabase(logger);
        }

        [UnitTest]
        public void SecureExecutionRequest_ShouldExecute()
        {
			EnsureRepository();
            IHttpContext context = CreateFakeContext(MethodInfo.GetCurrentMethod().Name);

            string input = "monkey";
            string jsonParams = ApiParameters.ParametersToJsonParamsArray(new object[] { input }).ToJson();

            Incubator testIncubator = new Incubator();
            testIncubator.Set<Echo>(new Echo());
            SecureExecutionRequest request = new SecureExecutionRequest(context, "Echo", "Send", jsonParams);
            request.ServiceProvider = testIncubator;

            AesKeyVectorPair kvp = new AesKeyVectorPair();
            // ensure the symettric key is set
            request.Session.SetSymmetricKey(kvp.Key, kvp.IV);
            // 

            request.Execute();

            Expect.IsTrue(request.Result.GetType() == typeof(string)); // should be base64 cipher of json result

            string result = request.GetResultAs<string>();
            Expect.AreEqual(input, result);

			CleanUp();
        }

        private static List<string> FilesToDelete
        {
            get;
            set;
        }

        private static IHttpContext CreateFakeContext(string sessionName)
        {
            IHttpContext context = A.Fake<IHttpContext>();
            IResponse response = A.Fake<IResponse>();
            response.Headers = new WebHeaderCollection();
            response.Headers[Headers.SecureSession] = sessionName;
            context.Request = A.Fake<IRequest>();
            context.Response = A.Fake<IResponse>();
            string fileName = MethodInfo.GetCurrentMethod().Name.RandomLetters(6);
            
            FileInfo testFile = new FileInfo(fileName);
            FilesToDelete.Add(testFile.FullName);
            "some junk".SafeWriteToFile(testFile.FullName, true);

            A.CallTo(() => context.Request.InputStream).Returns(new FileStream(testFile.FullName, FileMode.OpenOrCreate, FileAccess.Read));
            A.CallTo(() => context.Request.Url).Returns(new Uri("http://localhost:8080/POST/SecureChannel/Invoke.json?"));
            return context;
        }
    }
}
