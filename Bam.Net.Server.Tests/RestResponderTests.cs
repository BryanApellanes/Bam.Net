/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Server;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using System.Reflection;
using Bam.Net.CommandLine;
using System.IO;
using Bam.Net.DaoRef;
using FakeItEasy;
using System.Security.Principal;
using Bam.Net.Server.Rest;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Server.Tests
{
    [Serializable]
    public class TestClass
    {
        public long? Id { get; set; }
        public string Uuid { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public virtual TestStudent[] TestStudents { get; set; }
    }

    public class TestStudent
    {
        public long? Id { get; set; }
        public long? TestClassId { get; set; }
        public string Name { get; set; }
        public virtual TestClass TestClass { get; set; }
    }
    [Serializable]
    public class RestResponderTests: CommandLineTestInterface
    {
        class TestRestResponder:RestResponder
        {
            public TestRestResponder(BamConf conf, IRepository repo, ILogger logger) : base(conf, repo, logger) { }
            public bool TestPost(IHttpContext context)
            {
                return Post(context);
            }

            public bool TestGet(IHttpContext context)
            {
                return Get(context);
            }

            public bool TestPut(IHttpContext context)
            {
                return Put(context);
            }

            public bool TestDelete(IHttpContext context)
            {
                return Delete(context);
            }
        }

        [UnitTest("RestResponder: Post Test")]
        public void RestResponderPostTest()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DaoRepository repo;
            TestRestResponder responder;
            GetTestRepoAndResponder(methodName, out repo, out responder);

            TestClass testObject = new TestClass();
            string testValue = "some random value: ".RandomLetters(8);
            DateTime testDateTime = DateTime.UtcNow.Trim();
            testObject.StringProperty = testValue;
            testObject.DateTimeProperty = testDateTime;
            IHttpContext fakeContext = GetContext("http://test.cxm/TestClass.json", "POST", testObject);

            bool shouldBeTrue = responder.TestPost(fakeContext);
            shouldBeTrue.IsTrue("Post failed");

            MemoryStream output = GetOutput(fakeContext);
            RestResponse response = output.FromJsonStream<RestResponse>();
            Expect.IsNotNull(response);
            Expect.IsTrue(response.Success);
            TestClass result = response.Data.FromJObject<TestClass>();

            TestClass obj = repo.Query((Func<TestClass, bool>)(o => o.StringProperty.Equals(testValue))).FirstOrDefault();

            Expect.AreEqual(testValue, obj.StringProperty);
            Expect.AreEqual(testDateTime, obj.DateTimeProperty);
            Expect.AreEqual(obj.StringProperty, result.StringProperty);
            Expect.AreEqual(obj.DateTimeProperty, result.DateTimeProperty);
            Expect.IsGreaterThan(obj.Id.Value, 0, "Value wasn't greater than 0");
            OutLine(obj.PropertiesToString(), ConsoleColor.Cyan);
        }

        // ** Retrieve / GET **
        // /{Type}.{ext}?{Query}
        // /{Type}/{Id}.{ext}
        // /{Type}/{Id}/{ChildListProperty}.{ext} 
        [UnitTest("RestResponder: Get By Id Test")]
        public void GetByIdTest()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DaoRepository repo;
            TestRestResponder responder;
            Database database;
            GetTestRepoAndResponder(methodName, out repo, out responder, out database);

            TestClass objectInstance = GetTestClassInstance(methodName, repo);

            IHttpContext fakeContext = GetContext("http://blah.cxm/TestClass/{0}.json"._Format(objectInstance.Id.Value), "GET");
            bool shouldBeTrue = responder.TestGet(fakeContext);
            shouldBeTrue.IsTrue("Get failed");

            MemoryStream output = GetOutput(fakeContext);
            RestResponse response = output.FromJsonStream<RestResponse>();
            Expect.IsNotNull(response);
            TestClass result = response.Data.FromJObject<TestClass>();
            Expect.IsTrue(response.Success);
            Expect.IsNotNull(result);
            Expect.AreEqual(result.Id.Value, objectInstance.Id.Value, "Ids didn't match");
            Expect.AreEqual(objectInstance.StringProperty, result.StringProperty, "StringProperty didn't match");
        }

        [UnitTest("RestResponder: Get Child List Test")]
        public void GetChildListTest()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DaoRepository repo;
            TestRestResponder responder;
            Database database;
            GetTestRepoAndResponder(methodName, out repo, out responder, out database);

            TestClass objectInstance = new TestClass();
            objectInstance.StringProperty = "{0}:: a value ({1})"._Format(methodName, 8.RandomLetters());
            objectInstance.TestStudents = new TestStudent[] { new TestStudent { Name = 5.RandomLetters() }, new TestStudent { Name = 10.RandomLetters() } };
            objectInstance = repo.Save(objectInstance);
            Expect.IsGreaterThan(objectInstance.Id.Value, 0);
            objectInstance.TestStudents.Each(s =>
            {
                Expect.IsGreaterThan(s.Id.Value, 0);
            });

            IHttpContext fakeContext = GetContext("http://blah.cxm/TestClass/{0}/TestStudents.json"._Format(objectInstance.Id.Value), "GET");
            bool shouldBeTrue = responder.TestGet(fakeContext);
            shouldBeTrue.IsTrue("Get failed");

            MemoryStream output = GetOutput(fakeContext);
            RestResponse response = output.FromJsonStream<RestResponse>();
            Expect.IsNotNull(response);
            TestStudent[] result = response.Data.FromJObject<TestStudent[]>();
            Expect.IsTrue(response.Success);
            Expect.AreEqual(2, result.Length);
            result.Each(s =>
            {
                OutLine(s.PropertiesToString());
            });
        }
        [UnitTest("RestResponder: Query Test")]
        public void GetQueryTest()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DaoRepository repo;
            TestRestResponder responder;
            Database database;
            GetTestRepoAndResponder(methodName, out repo, out responder, out database);

            TestClass objectInstance = new TestClass();
            string studentName = 6.RandomLetters();
            objectInstance.StringProperty = "{0}:: a value ({1})"._Format(methodName, 8.RandomLetters());
            objectInstance.TestStudents = new TestStudent[] { new TestStudent { Name = studentName }, new TestStudent { Name = 10.RandomLetters() } };
            objectInstance = repo.Save(objectInstance);
            Expect.IsGreaterThan(objectInstance.Id.Value, 0);
            objectInstance.TestStudents.Each(s =>
            {
                Expect.IsGreaterThan(s.Id.Value, 0);
            });

            IHttpContext fakeContext = GetContext("http://blah.cxm/TestStudent.json?Name={Name}".NamedFormat(new { Name = studentName }), "GET");
            bool shouldBeTrue = responder.TestGet(fakeContext);
            shouldBeTrue.IsTrue("Get failed");

            MemoryStream output = GetOutput(fakeContext);
            RestResponse response = output.FromJsonStream<RestResponse>();
            Expect.IsNotNull(response);
            TestStudent[] result = response.Data.FromJObject<TestStudent[]>();
            Expect.IsTrue(response.Success);
            Expect.AreEqual(1, result.Length);
            result.Each(s =>
            {
                OutLine(s.PropertiesToString());
            });
        }

        [UnitTest("RestResponder: Put Test")]
        public void PutTest()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DaoRepository repo;
            TestRestResponder responder;
            TestClass objectInstance;
            GetTestObjects(methodName, out repo, out responder, out objectInstance);

            string updatedProperty = 15.RandomLetters();
            TestClass copy = objectInstance.CopyTo<TestClass, TestClass>();
            copy.StringProperty = updatedProperty;
            IHttpContext fakeContext = GetContext("http://blah.cxm/TestClass/{0}.json"._Format(objectInstance.Id.Value), "PUT", copy);
            bool shouldBeTrue = responder.TestPut(fakeContext);
            shouldBeTrue.IsTrue("Put failed");

            MemoryStream output = GetOutput(fakeContext);
            RestResponse response = output.FromJsonStream<RestResponse>();
            Expect.IsNotNull(response);
            Expect.IsTrue(response.Success);
            TestClass result = response.Data.FromJObject<TestClass>();
            TestClass retrieved = repo.Retrieve<TestClass>(objectInstance.Id.Value);
            Expect.AreEqual(updatedProperty, result.StringProperty);
            Expect.AreEqual(updatedProperty, retrieved.StringProperty);
        }

        [UnitTest("RestResponder: Delete Test")]
        public void DeleteTest()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            DaoRepository repo;
            TestRestResponder responder;
            TestClass objectInstance;
            GetTestObjects(methodName, out repo, out responder, out objectInstance);

            IHttpContext fakeContext = GetContext("http://blah.cxm/TestClass/{0}.json"._Format(objectInstance.Id.Value), "DELETE");
            bool shouldBeTrue = responder.TestDelete(fakeContext);
            shouldBeTrue.IsTrue("Put failed");

            MemoryStream output = GetOutput(fakeContext);
            RestResponse response = output.FromJsonStream<RestResponse>();
            Expect.IsNotNull(response);
            Expect.IsTrue(response.Success);
            TestClass retrieved = repo.Retrieve<TestClass>(objectInstance.Id.Value);
            Expect.IsNull(retrieved);
        }

        private void GetTestObjects(string methodName, out DaoRepository repo, out TestRestResponder responder, out TestClass objectInstance)
        {
            Database database;
            GetTestRepoAndResponder(methodName, out repo, out responder, out database);

            objectInstance = new TestClass();
            objectInstance.StringProperty = "{0}:: a value ({1})"._Format(methodName, 8.RandomLetters());
            objectInstance = repo.Save(objectInstance);
            Expect.IsGreaterThan(objectInstance.Id.Value, 0);
        }

        private static TestClass GetTestClassInstance(string methodName, DaoRepository repo)
        {
            TestClass objectInstance = new TestClass();
            objectInstance.StringProperty = string.Format("{0}:: a value ".RandomLetters(8), methodName);
            repo.Save(objectInstance);
            Expect.IsGreaterThan(objectInstance.Id.Value, 0);
            return objectInstance;
        }

        private static MemoryStream GetOutput(IHttpContext fakeContext)
        {
            MemoryStream ms = fakeContext.Response.OutputStream as MemoryStream;
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private static IHttpContext GetContext(string url, string httpMethod, TestClass bodyJson = null)
        {
            IRequest fakeRequest = A.Fake<IRequest>();
            IHttpContext fakeContext = A.Fake<IHttpContext>();
            IResponse fakeResponse = A.Fake<IResponse>();
            IPrincipal fakePrincipal = A.Fake<IPrincipal>();
            
            A.CallTo(() => fakeContext.Response).Returns(fakeResponse);
            A.CallTo(() => fakeContext.User).Returns(fakePrincipal);
            A.CallTo(() => fakeContext.Request).Returns(fakeRequest);            

            Uri uri = new Uri(url);
            A.CallTo(() => fakeResponse.OutputStream).Returns(new MemoryStream());
            A.CallTo(() => fakeRequest.Url).Returns(uri);
            NameValueCollection queryString = new NameValueCollection();
            uri.Query.DelimitSplit("?", "&").Each(nvp =>
            {
                string[] nvps = nvp.DelimitSplit("=");
                queryString.Add(nvps[0], nvps[1]);
            });
            A.CallTo(() => fakeRequest.QueryString).Returns(queryString);
            A.CallTo(() => fakeRequest.HttpMethod).Returns(httpMethod);

            if (bodyJson != null)
            {
                A.CallTo(() => fakeRequest.InputStream).Returns(bodyJson.ToJsonStream());
            }
            return fakeContext;
        }
        private void GetTestRepoAndResponder(string methodName, out DaoRepository repo, out TestRestResponder responder)
        {
            Database db;
            GetTestRepoAndResponder(methodName, out repo, out responder, out db);
        }
        private void GetTestRepoAndResponder(string methodName, out DaoRepository repo, out TestRestResponder responder, out Database database)
        {
            string testDirPath = Path.Combine(this.GetAppDataFolder(), methodName);
            ILogger logger = GetLogger();
            database = new SQLiteDatabase(".", methodName);
            repo = new DaoRepository(database, logger);
            repo.WarningsAsErrors = false;
            repo.AddType<TestStudent>();
            repo.AddType<TestClass>();
            responder = new TestRestResponder(BamConf.Load(testDirPath), repo, logger);
        }

        private ILogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.UseColors = true;
            logger.ApplicationName = "ResponderTests";
            logger.StartLoggingThread();
            return logger;
        }
    }
}
