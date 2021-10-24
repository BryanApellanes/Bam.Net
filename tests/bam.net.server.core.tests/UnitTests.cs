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
using System.Threading;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.CommandLine;
using Bam.Net.Presentation.Html;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Incubation;
using Bam.Net.Server.Renderers;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using Moq;
using FakeItEasy;
using Bam.Net.Testing.Unit;
using Bam.Net.Server.PathHandlers;
using Bam.Net.Data;
using UAParser;

namespace Bam.Net.Server.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTool
    {
        static List<string> _directoriesToDelete = new List<string>();
        static UnitTests()
        {
            AppDomain.CurrentDomain.DomainUnload += (s, e) =>
            {
                _directoriesToDelete.Each(d =>
                {
                    Directory.Delete(d, true);
                });
            };
        }

        [BeforeUnitTests]
        public void Before()
        {
            CleanUpTempDirectory();
        }

        [AfterEachUnitTest]
        public void AfterEach()
        {
            StopServers();
        }

        public void CleanUpTempDirectory()
        {
            string tempDir = "/bam/temp";
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
                Directory.CreateDirectory(tempDir);
                Thread.Sleep(1000);
            }
        }

        public void StopServers()
        {
            _servers.Each(server =>
            {
                server.Stop();
                Thread.Sleep(1000);
            });
        }

        [UnitTest]
        public void WhatDoesEndOfLineLookLike()
        {
            string test = @"this is the first
";
            string test2 = "this is the second\r\n";

            Expect.AreEqual("\n", test.Tail(1));
            Expect.AreEqual("\n", test2.Tail(1));
        }

        [UnitTest]
        public void HeadShouldFunctionAsDesigned()
        {
            string testString = "The Quick Brown Fox Jumps Over The Lazy Dog";
            string the;
            string rest = testString.Head(3, out the).Trim();

            Expect.AreEqual("The", the);
            Expect.AreEqual("Quick Brown Fox Jumps Over The Lazy Dog", rest);
        }

        [UnitTest]
        public void TailShouldFunctionAsDesigned()
        {
            string testString = "The Quick Brown Fox Jumps Over The Lazy Dog";
            string dog;
            string rest = testString.Tail(3, out dog).Trim();

            Expect.AreEqual("Dog", dog);
            Expect.AreEqual("The Quick Brown Fox Jumps Over The Lazy", rest);
        }

        [UnitTest]
        public void ShouldBeAbleToSetTheContentRoot()
        {
            BamServer server = new BamServer(BamConf.Load());//BamServerFactory.Default.Create();
            string root = "./{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(4);
            server.ContentRoot = root;
            FileInfo validate = new FileInfo(root);
            string valid = validate.FullName + Path.DirectorySeparatorChar;
            Expect.AreEqual(valid, server.ContentRoot);
            OutLine(server.ContentRoot);
        }

        [UnitTest]
        public void SettingContentRootShouldSetRootForRequestHandlerContent()
        {
            BamServer server = new BamServer(BamConf.Load());//BamServerFactory.Default.Create();
            server.ContentRoot = "./{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(4);
            Expect.AreEqual(server.ContentRoot, server.ContentResponder.Root);
        }

        [UnitTest]
        public void SettingContentRootShouldNotAddMoreThanOneContentResponder()
        {
            BamServer server = new BamServer(BamConf.Load());
            server.ContentRoot = "/{0}"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(4);
            Expect.AreEqual(1, server.Responders.Count(r => r.GetType().Equals(typeof(ContentResponder))));
        }

        [UnitTest]
        public void GetHostPrefixesShouldReturnDefault()
        {
            BamServer server = new BamServer(new BamConf());
            HostPrefix[] results = server.GetHostPrefixes();
            Expect.IsGreaterThan(results.Length, 0, "no host prefixes were returned");
        }

        [UnitTest]
        public void StartShouldFireInitEvents()
        {
            string testAppName = MethodBase.GetCurrentMethod().Name;
            DirectoryInfo dir = new DirectoryInfo("/bam/temp/{0}_"._Format(testAppName).RandomLetters(4));
            CreateTestRootAndSetDefaultConfig(dir);
            BamServer server = CreateServer(dir.FullName);
            bool? ingCalled = false;
            server.Initializing += (bs) =>
            {
                ingCalled = true;
            };

            bool? izedCalled = false;
            server.Initialized += (bs) =>
            {
                izedCalled = true;
            };

            Expect.IsFalse(ingCalled.Value);
            Expect.IsFalse(izedCalled.Value);

            server.Start();
            server.Stop();

            Expect.IsTrue(ingCalled.Value, "Initializing was not fired");
            Expect.IsTrue(izedCalled.Value, "Initialized was not fired");

        }

        [UnitTest]
        public void BamServerShouldHaveDaoRepsonder()
        {
            string testAppName = MethodBase.GetCurrentMethod().Name;
            DirectoryInfo dir = new DirectoryInfo("/bam/temp/{0}_"._Format(testAppName).RandomLetters(4));
            CreateTestRootAndSetDefaultConfig(dir);
            BamServer server = CreateServer(dir.FullName);
            Expect.AreEqual(1, server.Responders.Where(r => r.GetType().Equals(typeof(DaoResponder))).Count());
        }

        [UnitTest]
        public void AppsInitializationEventsShouldFireOnServerStart()
        {
            string testAppName = MethodBase.GetCurrentMethod().Name;
            DirectoryInfo dir = CreateTestRootAndSetDefaultConfig(testAppName);
            BamServer server = CreateServer(dir.FullName);
            bool? ingFired = false;
            server.ContentResponder.AppContentRespondersInitializing += (content) =>
            {
                ingFired = true;
            };

            bool? izedFired = false;
            server.ContentResponder.AppContentRespondersInitialized += (content) =>
            {
                izedFired = true;
            };

            Expect.IsFalse(ingFired.Value);
            Expect.IsFalse(izedFired.Value);

            server.Start();

            Expect.IsTrue(ingFired.Value);
            Expect.IsTrue(izedFired.Value);

            server.Stop();
            Thread.Sleep(1000);
        }

        [UnitTest]
        public void TemplateInitializationEventsShouldFireOnStart()
        {
            string testAppName = MethodBase.GetCurrentMethod().Name;
            DirectoryInfo dir = new DirectoryInfo("/bam/temp/{0}_"._Format(testAppName).RandomLetters(4));
            CreateTestRootAndSetDefaultConfig(dir);
            BamServer server = CreateServer(dir.FullName);
            bool? ingFired = false;
            server.ContentResponder.CommonTemplateRendererInitializing += (content) =>
            {
                ingFired = true;
            };

            bool? izedFired = false;
            server.ContentResponder.CommonTemplateRendererInitialized += (content) =>
            {
                izedFired = true;
            };

            Expect.IsFalse(ingFired.Value);
            Expect.IsFalse(izedFired.Value);

            server.Start();

            Expect.IsTrue(ingFired.Value);
            Expect.IsTrue(izedFired.Value);
            Out("Dust template contents: \r\n", ConsoleColor.Cyan);

            server.Stop();
            Thread.Sleep(1000);
        }

        [UnitTest]
        public void BamConfSchemaInitializersShouldNotBeNull()
        {
            BamConf conf = new BamConf();
            Expect.IsNotNull(conf.SchemaInitializers);
            Expect.IsTrue(conf.SchemaInitializers.Length > 0);
        }

        [UnitTest]
        public void BamConfSchemaInitializersShouldSetServerCopy()
        {
            BamConf conf = new BamConf();
            BamServer server = new BamServer(conf);
            Expect.IsNotNull(server.SchemaInitializers);
            Expect.IsTrue(server.SchemaInitializers.Length > 0);
            Expect.AreEqual(conf.SchemaInitializers.Length, server.SchemaInitializers.Length);
        }

        [UnitTest]
        public void AppContentConfShouldNotBeNull()
        {
            ContentResponder content = new ContentResponder(BamConf.Load(), CreateLogger());
            AppContentResponder appContent = new AppContentResponder(content, new AppConf("Monkey"));
            Expect.IsNotNull(appContent.AppConf);
        }

        [UnitTest]
        public void AppContentNameShouldNotBeNull()
        {
            ContentResponder content = new ContentResponder(BamConf.Load(), CreateLogger());
            AppContentResponder appContent = new AppContentResponder(content, new AppConf("Monkey"));
            Expect.IsNotNull(appContent.ApplicationName);
            Expect.AreEqual("Monkey", appContent.ApplicationName);
        }

        [UnitTest]
        public void ContentRootShouldMatchConf()
        {
            BamConf conf = new BamConf();
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf, CreateLogger());
            DirectoryInfo check = new DirectoryInfo(content.Root); // don't compare strings because the content flips backslashes with forward slashes
            Expect.AreEqual("{0}/"._Format(root.FullName), check.FullName); // content.Root adds a trailing slash

        }

        [UnitTest]
        public void AppContentInitializeShouldSetupFiles()
        {
            BamConf conf = new BamConf();
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf, CreateLogger());
            AppConf appConf = new AppConf("monkey");
            AppContentResponder appContent = new AppContentResponder(content, appConf);
            // should create the folder <conf.ContentRoot>/apps/monkey
            string appPath = Path.Combine(conf.ContentRoot, "apps", appConf.Name);
            if (Directory.Exists(appPath))
            {
                Directory.Delete(appPath, true);
            }
            Expect.IsFalse(Directory.Exists(appPath));
            appContent.Initialize();
            Expect.IsTrue(Directory.Exists(appPath));
        }

        [UnitTest]
        public void AppContentInitializeShouldCreateAppConfDotJson()
        {
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            BamConf conf = new BamConf()
            {
                ContentRoot = root.FullName
            };
            ContentResponder content = new ContentResponder(conf, CreateLogger());
            AppConf appConf = new AppConf("monkey");
            string layout = "".RandomLetters(4);
            appConf.DefaultLayout = layout;
            AppContentResponder appContent = new AppContentResponder(content, appConf);
            string appConfPath = Path.Combine(conf.ContentRoot, "apps", appConf.Name, "appConf.json");
            Expect.IsFalse(File.Exists(appConfPath));

            appContent.Initialize();
            Expect.IsTrue(File.Exists(appConfPath), "appConf.json did not get created");
            AppConf check = appConfPath.FromJsonFile<AppConf>();
            Expect.AreEqual(layout, check.DefaultLayout);

        }


        [UnitTest]
        public void ShouldBeAbleToCreateAppByName()
        {
            BamServer server = CreateServer(MethodBase.GetCurrentMethod().Name);
            string appName = "TestApp_".RandomLetters(4);
            server.CreateApp(appName, null, RandomNumber.Between(8000, 9999));
            DirectoryInfo appDir = new DirectoryInfo(Path.Combine(server.ContentRoot, "apps", appName));
            Expect.IsTrue(appDir.Exists);

            if (Directory.Exists(server.ContentRoot))
            {
                Directory.Delete(server.ContentRoot, true);
            }
        }

        [UnitTest]
        public void CreateAppShouldFireAppropriateEvents()
        {
            BamServer server = CreateServer(MethodBase.GetCurrentMethod().Name);
            bool? ed = false;
            bool? ing = false;

            server.CreatingApp += (s, ac) =>
            {
                ing = true;
            };

            server.CreatedApp += (s, ac) =>
            {
                ed = true;
            };

            Expect.IsFalse(ing.Value);
            Expect.IsFalse(ed.Value);

            string appName = "TestApp_".RandomLetters(4);
            server.CreateApp(appName, null, RandomNumber.Between(8000, 9999));

            Expect.IsTrue(ing.Value);
            Expect.IsTrue(ed.Value);

            if (Directory.Exists(server.ContentRoot))
            {
                Directory.Delete(server.ContentRoot, true);
            }
        }

        [UnitTest]
        public void DustTemplateRendererOutputStreamShouldNotBeNull()
        {
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            ContentResponder content = GetTestContentResponder(root);
            CommonTemplateRenderer renderer = new CommonTemplateRenderer(content);
            Expect.IsNotNull(renderer.OutputStream);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderDustTemplateForType()
        {
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            ContentResponder content = GetTestContentResponder(root);
            DirectoryInfo dustRoot = new DirectoryInfo(Path.Combine(content.Root, "common", "views"));
            CommonTemplateRenderer templateRenderer = new CommonTemplateRenderer(content);
            TestMonkey monkey = new TestMonkey();

            Expect.IsFalse(File.Exists(Path.Combine(dustRoot.FullName, "TestMonkey.dust")), "Template was already there");

            // should render a template into the dustRoot folder
            templateRenderer.Render(monkey);

            Expect.IsTrue(File.Exists(Path.Combine(dustRoot.FullName, "TestMonkey.dust")), "Template was not written as expected");
        }

        [UnitTest]
        public void ShouldBeAbleToCompileSourceWithDustScript()
        {
            string source = "Hello {Name}";
            string compiled = DustScript.Compile(source, "test");
            OutLine(compiled);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderWithDustScript()
        {
            string source = "Hello {Name}";
            string compiled = DustScript.Compile(source, "test");
            string output = DustScript.Render(compiled, "test", new { Name = "Bananas" });
            OutLine(output);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderTemplateFromDirectory()
        {
            // create a test directory
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            // write test templates into it
            string source = "Hello {Name}";
            source.SafeWriteToFile(Path.Combine(root.FullName, "test.dust"));
            // render something using the directory templates
            string output = DustScript.Render(root, "test", new { Name = "Gorilla" });
            OutLine(output);

        }

        [UnitTest]
        public void ShouldBeAbleToCompileDustDirectory()
        {
            DirectoryInfo root = new DirectoryInfo("/bam/temp/{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            ContentResponder content = GetTestContentResponder(root);
            CommonTemplateRenderer templateRenderer = new CommonTemplateRenderer(content);
            TestMonkey monkey = new TestMonkey();
            templateRenderer.Render(monkey);
            AppContentResponder appResponder = new AppContentResponder(content, new AppConf("Test"));
            CommonDustRenderer renderer = new CommonDustRenderer(appResponder);
            Expect.IsTrue(!string.IsNullOrEmpty(renderer.CombinedCompiledTemplates));
            OutLine(renderer.CombinedCompiledTemplates);
        }


        [UnitTest]
        public void GenerateDaoInConfShouldMatchServerSetting()
        {
            BamConf conf = BamConf.Load();
            conf.GenerateDao = false;
            conf.Save(true);
            Expect.IsFalse(conf.GenerateDao);
            BamServer server = new BamServer(conf);
            server.GenerateDao = true;
            conf = server.GetCurrentConf();
            Expect.IsTrue(conf.GenerateDao);
            server.SaveConf(true);
            conf = BamConf.Load(server.ContentRoot);
            Expect.IsTrue(conf.GenerateDao);
        }

        class TestResponder : Responder
        {
            public TestResponder() : base(BamConf.Load()) { }
            public override bool TryRespond(IHttpContext context)
            {
                throw new NotImplementedException();
            }
        }

        [UnitTest]
        public void MayRespondShouldBeTrueIfPrefixAdded()
        {
            Mock<IHttpContext> ctx = Tools.CreateMockContext("http://blah.cxm/Monkey");

            TestResponder test = new TestResponder();
            test.AddRespondToPrefix("Monkey");

            Expect.IsTrue(test.MayRespond(ctx.Object));
        }

        class TestTemplateInitializer : TemplateInitializer
        {
            public TestTemplateInitializer(BamServer server) : base(server) { }
            public bool InitializeCalled
            {
                get;
                set;
            }
            public override void Initialize()
            {
                InitializeCalled = true;
            }

            public override void RenderAppTemplates()
            {
                throw new NotImplementedException();
            }

            public override void RenderCommonTemplates()
            {
                throw new NotImplementedException();
            }
        }

        [UnitTest]
        public void SettingConfigShouldReflectInServerSettings()
        {
            BamServer server = CreateServer("{0}_Content"._Format(MethodBase.GetCurrentMethod().Name));
            BamConf conf = new BamConf()
            {
                GenerateDao = true,
            };
            server.GenerateDao = false;

            Expect.IsFalse(server.GenerateDao);

            Expect.IsTrue(conf.GenerateDao);

            server.SetConf(conf);

            Expect.IsTrue(server.GenerateDao);
        }

        [UnitTest]
        public void DaoResponderMayRespondToDaoProxies()
        {
            DaoResponder responder = new DaoResponder(BamConf.Load());
            Mock<IHttpContext> mockContext = Tools.CreateMockContext("http://blah.com/dao/proxies");
            Expect.IsTrue(responder.MayRespond(mockContext.Object));
        }

        class TestBam
        {
            public void GetPages()
            {
                if (Do != null)
                {
                    Do();
                }
            }

            public Action Do
            {
                get;
                set;
            }
        }
        [UnitTest]
        public void ExecutionRequestShouldBeValidForValidUrls()
        {
            string url = "http://blah.com/get/TestBam/GetPages.json";
            RequestWrapper reqW = new RequestWrapper(new { Headers = new NameValueCollection(), Url = new Uri(url), HttpMethod = "GET", ContentLength = 0, QueryString = new NameValueCollection() });
            ResponseWrapper resW = new ResponseWrapper(new object());

            ExecutionRequest req = new ExecutionRequest(reqW, resW);
            req.ServiceProvider.Set<TestBam>(new TestBam());
            req.RequestUrl = new Uri(url);
            req.ResolveExecutionTargetInfo();

            ValidationResult result = req.Validate();

            Expect.IsTrue(result.Success);
            Expect.AreEqual("TestBam", req.ClassName);
            Expect.AreEqual("GetPages", req.MethodName);
            Expect.AreEqual("json", req.Ext);
        }

        [UnitTest]
        public void ShouldBeAbleToWrapDynamic()
        {
            string url = "http://blah.com";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url) });
            Expect.AreEqual(url, req.Url.OriginalString);
        }

        [UnitTest]
        public void ProxyAliasesShouldNotBeNull()
        {
            string url = "http://blah.com/Monkey/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url), HttpMethod = "GET" });
            ResponseWrapper res = new ResponseWrapper(new object());
            ProxyAlias alias = new ProxyAlias("Monkey", typeof(TestBam));
            ExecutionRequest execRequest = new ExecutionRequest(req, res, new ProxyAlias[] { alias });
            Expect.IsNotNull(execRequest.ProxyAliases);
        }

        [UnitTest]
        public void ProxyAliasesShouldBeEqual()
        {
            ProxyAlias alias1 = new ProxyAlias("Alias", typeof(ProxyAlias));
            ProxyAlias alias2 = new ProxyAlias("Alias", typeof(ProxyAlias));
            Expect.IsTrue(alias1.Equals(alias2));
            Expect.IsTrue(alias2.Equals(alias1));
        }

        [UnitTest]
        public void ProxyAliasListShouldContain()
        {
            ProxyAlias alias1 = new ProxyAlias("Alias", typeof(ProxyAlias));
            ProxyAlias alias2 = new ProxyAlias("Alias", typeof(ProxyAlias));
            List<ProxyAlias> list = new List<ProxyAlias>();
            list.Add(alias1);
            Expect.IsTrue(list.Contains(alias2));
        }

        [UnitTest]
        public void ExecutionRequestShouldResolveClassAlias()
        {
            string url = "http://blah.com/Monkey/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Headers = new NameValueCollection(), Url = new Uri(url), HttpMethod = "GET" });
            ResponseWrapper res = new ResponseWrapper(new object());
            ProxyAlias alias = new ProxyAlias("Monkey", typeof(TestBam));
            ExecutionRequest execRequest = new ExecutionRequest(req, res, new ProxyAlias[] { alias });
            Expect.AreEqual("TestBam", execRequest.ClassName);
        }

        [UnitTest]
        public void ExecutionRequestShouldNotBeInitialized()
        {
            string url = "http://blah.com/Monkey/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Headers = new NameValueCollection(), Url = new Uri(url), HttpMethod = "GET" });
            ResponseWrapper res = new ResponseWrapper(new object());
            ProxyAlias alias = new ProxyAlias("Monkey", typeof(TestBam));
            ExecutionRequest execRequest = new ExecutionRequest(req, res, new ProxyAlias[] { alias });

            Expect.IsFalse(execRequest.IsInitialized);
        }

        [UnitTest]
        public void GetShouldReturnSameInstanceThatWasSet()
        {
            Incubator inc = new Incubator();
            TestBam test = new TestBam();
            inc.Set(typeof(TestBam), test);
            TestBam check = (TestBam)inc.Get(typeof(TestBam), new object[] { });
            Expect.AreSame(test, check);
            Expect.IsTrue(test == check);
        }

        [UnitTest]
        public void ExecutionRequestShouldBeValid()
        {
            string url = "http://blah.com/TestBam/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Headers = new NameValueCollection(), Url = new Uri(url), HttpMethod = "GET", ContentLength = 0, QueryString = new NameValueCollection() });
            ResponseWrapper res = new ResponseWrapper(new object());

            ExecutionRequest execRequest = new ExecutionRequest(req, res);
            execRequest.ServiceProvider.Set(typeof(TestBam), new TestBam());
            ValidationResult validation = execRequest.Validate();

            Expect.IsTrue(validation.Success);
        }

        [UnitTest]
        public void ShouldBeAbleToGetMethodCaseInsensitively()
        {
            Type bam = typeof(AppMetaManager);
            MethodInfo method = bam.GetMethod("getpages", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            Expect.IsNotNull(method);
        }

        class TestExecutor
        {
            public TestExecutor()
            {
                this.Value = "Test_".RandomLetters(6);
            }

            public string Value { get; set; }
            public string DoExecute()
            {
                return Value;
            }

            public string DoExecuteWithParameters(string input)
            {
                return input;
            }
        }
        [UnitTest]
        public void ExecutionRequestShouldExecute()
        {
            string url = "http://blah.com/TestExecutor/DoExecute.json";
            RequestWrapper req = new RequestWrapper(new { Headers = new NameValueCollection(), Url = new Uri(url), HttpMethod = "GET", ContentLength = 0, QueryString = new NameValueCollection() });
            ResponseWrapper res = new ResponseWrapper(new object());
            ExecutionRequest execRequest = new ExecutionRequest(req, res);
            TestExecutor execTarget = new TestExecutor();
            execRequest.ServiceProvider.Set(typeof(TestExecutor), execTarget);
            Expect.IsTrue(execRequest.Execute());
            Expect.AreEqual(execTarget.Value, execRequest.Result);
        }

        [UnitTest]
        public void ExecutionRequestShouldExecuteWithParameters()
        {
            string url = "/TestExecutor/DoExecuteWithParameters.json?input=bananas";
            ExecutionRequest execRequest = CreateExecutionRequest(url);
            TestExecutor execTarget = new TestExecutor();
            execRequest.ServiceProvider.Set(typeof(TestExecutor), execTarget);
            Expect.IsTrue(execRequest.Execute());
            Expect.AreEqual("bananas", execRequest.Result);
        }

        private static ExecutionRequest CreateExecutionRequest(string path)
        {
            Uri uri = new Uri("http://blah.com" + path);
            HttpArgs query = new HttpArgs(uri.Query);
            NameValueCollection nvc = new NameValueCollection();
            query.Keys.Each(k =>
            {
                nvc.Add(k, query[k]);
            });
            RequestWrapper req = new RequestWrapper(new { Headers = new NameValueCollection(), Url = uri, HttpMethod = "GET", ContentLength = 0, QueryString = nvc });
            ResponseWrapper res = new ResponseWrapper(new object());
            ExecutionRequest execRequest = new ExecutionRequest(req, res);
            return execRequest;
        }

        class SwitchTest
        {
            public string Do()
            {
                return "Yay it worked";
            }
        }
        [UnitTest]
        public void ShouldBeAbleToSwitchOutServiceProviderAndReExecute()
        {
            ExecutionRequest request = CreateExecutionRequest("/SwitchTest/Do.json");
            Incubator bad = new Incubator();
            request.ServiceProvider = bad;

            Expect.IsFalse(request.Execute());

            Incubator good = new Incubator();
            good.Set<SwitchTest>(new SwitchTest());
            request.ServiceProvider = good;

            Expect.IsTrue(request.Execute());
            Expect.AreEqual("Yay it worked", request.Result);
        }

        [UnitTest]
        public void ShouldBeAbleToSetProxyAliases()
        {
            BamServer server = CreateServer("Test_"._Format(MethodBase.GetCurrentMethod().Name));
            BamConf conf = new BamConf();
            conf.AddProxyAlias("Test", typeof(TestResponder));
            Expect.AreEqual(1, conf.ProxyAliases.Length);
            server.SetConf(conf);
            Expect.IsTrue(server.ProxyAliases.Length == 1);

            Expect.AreEqual(server.ProxyAliases[0].ClassName, typeof(TestResponder).Name);
        }

        private static DirectoryInfo CreateTestRootAndSetDefaultConfig(string testAppName)
        {
            DirectoryInfo dir = new DirectoryInfo("/bam/temp/{0}_"._Format(testAppName).RandomLetters(4));
            CreateTestRootAndSetDefaultConfig(dir);
            return dir;
        }

        private static ContentResponder GetTestContentResponder(DirectoryInfo root)
        {
            BamConf conf = new BamConf();
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf, CreateLogger());
            return content;
        }

        static List<BamServer> _servers = new List<BamServer>();
        internal static BamServer CreateServer(string rootDir = "")
        {
            BamServer server = new BamServer(BamConf.Load());
            ConsoleLogger logger = CreateLogger();
            server.MainLogger = logger;
            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = "./Test_".RandomLetters(5);
            }
            server.ContentRoot = rootDir;
            server.DefaultHostPrefix.Port = RandomNumber.Between(8081, 65535);
            server.SaveConf(true);
            _servers.Add(server);
            return server;
        }

        private static ConsoleLogger CreateLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.UseColors = true;
            logger.StartLoggingThread();
            return logger;
        }

        [UnitTest]
        public void ContentRootOfBamServerShouldHaveDefault()
        {
            BamServer server = new BamServer(BamConf.Load());
            Expect.IsNotNull(server.ContentRoot);
            Expect.IsTrue(server.ContentRoot.Length > 0);
            OutLine(server.ContentRoot);
        }

        [UnitTest]
        public void TruncateFrontShouldDropLeadingCharacters()
        {
            string toTruncateFront = "12345ThisIsIt";
            Expect.AreEqual("ThisIsIt", toTruncateFront.TruncateFront(5));
        }


        [UnitTest]
        public void WhatDoesPathGetExtensionReturn()
        {
            string ext = Path.GetExtension("file.zip");
            Expect.AreEqual(".zip", ext);
            OutLine(ext);
        }

        [UnitTest]
        public void WhatDoesFileInfoExtensionReturn()
        {
            FileInfo file = new FileInfo("someFile.db.js");
            OutLine(file.Extension);
            FileInfo file2 = new FileInfo("somefile.db.json");
            OutLine(file2.Extension);
        }

        [UnitTest]
        public void WhatDoesUriEscapingReallyDo()
        {
            string fullUrl = "http://fake.cxm/monkey/doc.htm?gorilla=<div>baloney</div>&balls=tupac&val=with some spaces";

            Message.PrintLine("Uri.EscapeDataString({0})", ConsoleColor.Cyan, fullUrl);
            Message.PrintLine("Result: {0}", ConsoleColor.Yellow, Uri.EscapeDataString(fullUrl));
            Out();
            Message.PrintLine("Uri.EscapeUriString({0})", ConsoleColor.Cyan, fullUrl);
            Message.PrintLine("Result: {0}", ConsoleColor.Yellow, Uri.EscapeUriString(fullUrl));
            Out();
            string queryString = "?key1=value1&key2=value2&key4=#monkey&key5=me@you.com&val=with some spaces";
            Message.PrintLine("Uri.EscapeDataString({0})", ConsoleColor.Blue, queryString);
            Message.PrintLine("Result: {0}", ConsoleColor.Yellow, Uri.EscapeDataString(queryString));
            Out();
            Message.PrintLine("Uri.EscapeUriString({0})", ConsoleColor.Blue, queryString);
            Message.PrintLine("Result: {0}", ConsoleColor.Yellow, Uri.EscapeUriString(queryString));
        }

        [UnitTest]
        public void UriProperties()
        {
            string fullUrl = "http://fake.cxm/monkey/doc.htm?gorilla=<div>baloney</div>&balls=tupac";
            Uri uri = new Uri(fullUrl);
            Message.PrintLine("Raw: {0}", fullUrl, ConsoleColor.Cyan);
            Message.PrintLine("AbsolutePath", uri.AbsolutePath, ConsoleColor.DarkBlue);
            Message.PrintLine("AbsoluteUri: {0}", uri.AbsoluteUri, ConsoleColor.DarkCyan);
            Message.PrintLine("Fragment: {0}", uri.Fragment, ConsoleColor.DarkGray);
            Message.PrintLine("Query: {0}", uri.Query, ConsoleColor.White);
            Message.PrintLine("Authority: {0}", uri.Authority, ConsoleColor.DarkYellow);
            Message.PrintLine("Host: {0}", uri.Host, ConsoleColor.Gray);
            Message.Print("Segments:");
            uri.Segments.Each(seg =>
            {
                Message.PrintLine("\t{0}", seg);
            });
        }

        [UnitTest]
        public void GetPagesShouldIncludeSubdirectories()
        {
            DirectoryInfo root = new DirectoryInfo("./Test_{0}"._Format(MethodBase.GetCurrentMethod().Name));
            if (root.Exists)
            {
                root.Delete(true);
            }

            if (!root.Exists)
            {
                root.Create();
            }

            DirectoryInfo appPages = new DirectoryInfo(Path.Combine(root.FullName, "apps", "test", "pages"));
            if (!appPages.Exists)
            {
                appPages.Create();
            }

            2.Times(i =>
            {
                CreateTestFile(appPages, i);
            });

            2.Times(i =>
            {
                DirectoryInfo subDir = CreateSubDir(appPages, i);
                CreateTestFile(subDir, 1);
                DirectoryInfo subSubDir = CreateSubDir(subDir, 1);
                CreateTestFile(subSubDir, 1);
            });

            BamConf conf = new BamConf();
            conf.ContentRoot = root.FullName;
            AppMetaManager mgr = new AppMetaManager(conf);
            string[] pageNames = mgr.GetPageNames("test");
            Expect.AreEqual(6, pageNames.Length);
            pageNames.Each(pn =>
            {
                Message.PrintLine("{0}", ConsoleColor.Yellow, pn);
            });
        }

        private static DirectoryInfo CreateSubDir(DirectoryInfo dirToCreateSubDirIn, int i)
        {
            DirectoryInfo subDir = new DirectoryInfo(Path.Combine(dirToCreateSubDirIn.FullName, "Dir{0}"._Format(i)));
            if (!subDir.Exists)
            {
                subDir.Create();
            }

            return subDir;
        }

        private static void CreateTestFile(DirectoryInfo dirToCreateFileIn, int i)
        {
            string pageName = "page{0}.html"._Format(i);
            "Test{0}"._Format(i).SafeWriteToFile(Path.Combine(dirToCreateFileIn.FullName, pageName), true);
        }

        [UnitTest]
        public void FileExtensionShouldBeBlank()
        {
            string path = "banana/republic";
            Expect.AreEqual("republic", Path.GetFileName(path));
            Expect.IsTrue(string.IsNullOrEmpty(Path.GetExtension(path)));
        }


        [UnitTest]
        public void SubscribeShouldIncrementSubscribers()
        {
            BamServer server = CreateServer(MethodBase.GetCurrentMethod().Name);
            ILogger logger = new TextFileLogger();
            Expect.AreEqual(0, server.Subscribers.Length);
            server.Subscribe(logger);
            Expect.AreEqual(1, server.Subscribers.Length);
            server.Subscribe(logger);
            Expect.AreEqual(1, server.Subscribers.Length); // should only get added once
            ILogger consoleLogger = new ConsoleLogger();
            server.Subscribe(consoleLogger);
            Expect.AreEqual(2, server.Subscribers.Length);
            Expect.IsTrue(server.IsSubscribed(consoleLogger));
        }

        [UnitTest]
        public void SettingLoggerShouldCauseServerToReinitializeIfItsRunning()
        {
            string testAppName = MethodBase.GetCurrentMethod().Name;
            DirectoryInfo dir = new DirectoryInfo("/bam/temp/{0}_"._Format(testAppName).RandomLetters(4));
            CreateTestRootAndSetDefaultConfig(dir);
            BamServer server = CreateServer(dir.FullName);
            bool? stopped = false;
            bool? initialized = false;
            Expect.IsFalse(server.IsRunning, "Server should not have been running");
            server.Stopped += (s) =>
            {
                stopped = true;
            };
            server.Start();
            server.Initialized += (s) =>
            {
                initialized = true;
            };
            Expect.IsFalse(initialized.Value, "Initialized should have been false");
            Expect.IsFalse(stopped.Value, "Stopped should have been false");
            server.MainLogger = new TextFileLogger();
            Expect.IsTrue(initialized.Value, "Initialized should have been true");
            Expect.IsTrue(stopped.Value, "Stopped should have been true");
            server.Stop();
        }

        [UnitTest]
        public void ServerShouldLoadConfOnInitialize()
        {
            string testAppName = MethodBase.GetCurrentMethod().Name;
            DirectoryInfo dir = new DirectoryInfo("/bam/temp/{0}_"._Format(testAppName).RandomLetters(4));
            CreateTestRootAndSetDefaultConfig(dir);
            BamServer server = CreateServer(dir.FullName);
            bool? ingCalled = false;
            bool? edCalled = false;
            server.LoadingConf += (s, c) =>
            {
                ingCalled = true;
            };

            server.LoadedConf += (s, c) =>
            {
                edCalled = true;
            };

            Expect.IsFalse(ingCalled.Value);
            Expect.IsFalse(edCalled.Value);

            server.Initialize();

            Expect.IsTrue(ingCalled.Value);
            Expect.IsTrue(edCalled.Value);
        }

        static bool? _setContextCalled = false;
        class TakesContextTest : IRequiresHttpContext
        {
            public void Monkey() { }

            IHttpContext _context;
            public IHttpContext HttpContext
            {
                get
                {
                    return _context;
                }
                set
                {
                    _context = value;
                    _setContextCalled = true;
                }
            }

            public object Clone()
            {
                TakesContextTest clone = new TakesContextTest();
                clone.CopyProperties(this);
                return clone;
            }
        }
        [UnitTest]
        public void ShouldSetContext()
        {
            ExecutionRequest execRequest = CreateExecutionRequest("/TakesContextTest/Monkey.json");
            ServiceProxySystem.Register<TakesContextTest>();
            Expect.IsFalse(_setContextCalled.Value);
            execRequest.Execute();
            Expect.IsTrue(_setContextCalled.Value);
        }

        [UnitTest]
        public void ShouldBeAbleToSpecifyBamConfSavePath()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(MethodBase.GetCurrentMethod().Name, "TestSubDir_".RandomLetters(4)));
            if (!dir.Exists)
            {
                dir.Create();
            }
            string jsonFile = Path.Combine(dir.FullName, "{0}.json"._Format(typeof(BamConf).Name));
            string yamlFile = Path.Combine(dir.FullName, "{0}.yaml"._Format(typeof(BamConf).Name));
            Expect.IsFalse(File.Exists(jsonFile));
            Expect.IsFalse(File.Exists(yamlFile));

            BamConf conf = new BamConf();
            conf.Save(dir.FullName, true, ConfFormat.Json);
            conf.Save(dir.FullName, true, ConfFormat.Yaml);

            Expect.IsTrue(File.Exists(jsonFile));
            Expect.IsTrue(File.Exists(yamlFile));

            File.Delete(jsonFile);
            File.Delete(yamlFile);
        }

        [UnitTest]
        public void BamConfShouldLoadFromPathSpecifiedInDefaultConfiguration()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(MethodBase.GetCurrentMethod().Name, "TestSubDir_".RandomLetters(4)));
            if (!dir.Exists)
            {
                dir.Create();
            }

            BamConf tmp = new BamConf();
            tmp.Save(dir.FullName, true, ConfFormat.Json);

            Dictionary<string, string> configOverrides = new Dictionary<string, string>();
            configOverrides.Add(BamConf.ContentRootConfigKey, dir.FullName);
            DefaultConfiguration.SetAppSettings(configOverrides);

            BamConf conf = BamConf.Load();
            string filePath = Path.Combine(dir.FullName, "{0}.json"._Format(typeof(BamConf).Name));
            Expect.AreEqual(filePath, conf.LoadedFrom);

            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }

        [UnitTest]
        public void BamConfShouldSaveJsonFileInPathSpecifiedInDefaultConfiguration()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(MethodBase.GetCurrentMethod().Name, "TestSubDir_".RandomLetters(4)));
            CreateTestRootAndSetDefaultConfig(dir);

            BamConf conf = BamConf.Load();

            Expect.IsNotNullOrEmpty(conf.LoadedFrom);
            Expect.IsTrue(File.Exists(conf.LoadedFrom));

            string filePath = Path.Combine(dir.FullName, "{0}.json"._Format(typeof(BamConf).Name));
            Expect.AreEqual(filePath, conf.LoadedFrom);

            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }

        [UnitTest]
        public void OutputAssemblyLocation()
        {
            Message.PrintLine(Assembly.GetExecutingAssembly().Location);
        }

        [UnitTest]
        public void TestUserAgentParser()
        {
            Parser parser = Parser.GetDefault();
            ClientInfo macInfo = parser.Parse(
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/601.3.9 (KHTML, like Gecko) Version/9.0.2 Safari/601.3.9");
            ClientInfo windowsInfo =
                parser.Parse(
                    "Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)");
            ClientInfo linuxInfo =
                parser.Parse(
                    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
            Message.PrintLine(macInfo.ToJson(true), ConsoleColor.Cyan);
            Message.PrintLine(windowsInfo.ToJson(true), ConsoleColor.Blue);
            Message.PrintLine(linuxInfo.ToJson(true), ConsoleColor.DarkGreen);
        }
        
        private static void CreateTestRootAndSetDefaultConfig(DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                dir.Create();
            }

            Dictionary<string, string> configOverrides = new Dictionary<string, string>();
            configOverrides.Add(BamConf.ContentRootConfigKey, dir.FullName);
            DefaultConfiguration.SetAppSettings(configOverrides);
        }
    }
}
