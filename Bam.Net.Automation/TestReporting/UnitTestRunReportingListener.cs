using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using System.Collections.Concurrent;
using Bam.Net.CommandLine;
using Bam.Net.Automation.TestReporting.Data;
using System.Reflection;

namespace Bam.Net.Automation.TestReporting
{
    public class UnitTestRunReportingListener : TestRunListener<UnitTestMethod>
    {
        ConcurrentDictionary<string, TestSuiteExecutionSummary> _testSuiteExecutionLookupByTitle;
        ConcurrentDictionary<string, TestSuiteDefinition> _testSuiteDefinitionLookupByTitle;
        ConcurrentDictionary<string, TestDefinition> _testDefinitionLookupByTitle;
        ConcurrentDictionary<MethodInfo, TestExecution> _testExecutionLookupByMethodInfo;

        public UnitTestRunReportingListener(string testReportHost, int port = 80, ILogger logger = null)
        {
            ProxyFactory proxyFactory = new ProxyFactory();
            TestReportService = proxyFactory.GetProxy<TestReportService>(testReportHost, port);
            TestReportHost = testReportHost;
            _testSuiteExecutionLookupByTitle = new ConcurrentDictionary<string, TestSuiteExecutionSummary>();
            _testSuiteDefinitionLookupByTitle = new ConcurrentDictionary<string, TestSuiteDefinition>();
            _testDefinitionLookupByTitle = new ConcurrentDictionary<string, TestDefinition>();
            _testExecutionLookupByMethodInfo = new ConcurrentDictionary<MethodInfo, TestExecution>();
        }
        
        public override void TestsStarting(object sender, TestEventArgs<UnitTestMethod> args)
        {
            ConsoleMethod test = args.Test;
            TestSuiteDefinition suite = GetTestSuiteDefinition(test);
            SetTestSuiteExecutionSummary(suite);
        }

        public override void TestStarting(object sender, TestEventArgs<UnitTestMethod> args)
        {
            UnitTestMethod test = args.Test.CopyAs<UnitTestMethod>();
            TestSuiteDefinition suite = TestSuiteDefinition.FromMethod(test);
            TestDefinition testDefinition = GetTestDefinition(suite.Title, test.Description);
            SetTestExecution(test);
        }

        public override void TestPassed(object sender, TestEventArgs<UnitTestMethod> args)
        {
            UnitTestMethod test = args.Test.CopyAs<UnitTestMethod>();
            TestExecution testExecution = GetTestExecution(test);
            testExecution.Passed = true;
            TestReportService.SaveTestExecution(testExecution);
        }

        public override void TestFailed(object sender, TestExceptionEventArgs args)
        {
            UnitTestMethod test = args.TestMethod.CopyAs<UnitTestMethod>();
            TestExecution testExecution = GetTestExecution(test);
            testExecution.Passed = false;
            Exception ex = args.Exception?.GetInnerException();
            testExecution.Exception = ex?.Message;
            testExecution.StackTrace = ex?.StackTrace;
            TestReportService.SaveTestExecution(testExecution);
        }

        public override void TestFinished(object sender, TestEventArgs<UnitTestMethod> args)
        {
            UnitTestMethod test = args.Test.CopyAs<UnitTestMethod>();
            TestExecution testExecution = GetTestExecution(test);
            TestReportService.FinishTest(testExecution.Id);
        }

        public override void TestsFinished(object sender, TestEventArgs<UnitTestMethod> args)
        {
            UnitTestMethod test = args.Test.CopyAs<UnitTestMethod>();
            TestSuiteDefinition suite = GetTestSuiteDefinition(test);
            TestSuiteExecutionSummary summary = GetTestSuiteExecutionSummary(suite);
            summary.FinishedTime = DateTime.UtcNow;
            TestReportService.SaveTestSuiteExecutionSummary(summary);
        }

        public override void Listen(ITestRunner<UnitTestMethod> runner)
        {
            TestRunner = runner;
            base.Listen(runner);
        }

        public string TestReportHost { get; set; }
        protected ITestRunner<UnitTestMethod> TestRunner { get; set; }        
        protected ITestReportService TestReportService { get; set; }
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Get a TestSuiteDefinition for the specified test creating it if necessary
        /// and populating the internal cache
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        protected TestSuiteDefinition GetTestSuiteDefinition(ConsoleMethod test)
        {
            TestSuiteDefinition suite = TestSuiteDefinition.FromMethod(test);
            if (!_testSuiteDefinitionLookupByTitle.TryGetValue(suite.Title, out suite))
            {
                GetSuiteDefinitionResponse response = TestReportService.GetSuiteDefinition(suite.Title);
                if (response.Success)
                {
                    _testSuiteDefinitionLookupByTitle.TryAdd(suite.Title, response.SuiteDefinition);
                }
                else
                {
                    Logger.Warning("Failed to define test suite: {0}", response.Message);
                }
            }

            return suite;
        }

        protected TestSuiteExecutionSummary GetTestSuiteExecutionSummary(TestSuiteDefinition suite)
        {
            return SetTestSuiteExecutionSummary(suite);
        }
        /// <summary>
        /// Set a TestSuiteExecutionSummary for the specified test creating it if necessary
        /// and populating the internal cache
        /// </summary>
        /// <param name="suite"></param>
        /// <returns></returns>
        protected TestSuiteExecutionSummary SetTestSuiteExecutionSummary(TestSuiteDefinition suite)
        {
            if (!_testSuiteExecutionLookupByTitle.TryGetValue(suite.Title, out TestSuiteExecutionSummary summary))
            {
                TestSuiteExecutionSummary executionSummary = new TestSuiteExecutionSummary { SuiteDefinitionId = suite.Id, StartedTime = DateTime.UtcNow };
                SaveTestSuiteExecutionSummaryResponse response = TestReportService.SaveTestSuiteExecutionSummary(executionSummary);
                if (response.Success)
                {
                    summary = response.TestSuiteExecutionSummary;
                    _testSuiteExecutionLookupByTitle.TryAdd(suite.Title, summary);
                }
                else
                {
                    Logger.Warning("Failed to create TestSuiteExecutionSummary: {0}", response.Message);
                }
            }
            return summary;
        }

        protected TestDefinition GetTestDefinition(string suiteTitle, string testTitle)
        {
            string key = $"Suite:{suiteTitle},Test:{testTitle}";
            if(!_testDefinitionLookupByTitle.TryGetValue(key, out TestDefinition test))
            {
                GetTestDefinitionResponse response = TestReportService.GetTestDefinition(suiteTitle, testTitle);
                if (response.Success)
                {
                    test = response.TestDefinition;
                    _testDefinitionLookupByTitle.TryAdd(key, test);
                }
                else
                {
                    Logger.Warning("Failed to get TestDefinition: {0}", key);
                }
            }
            return test;
        }
        protected TestExecution GetTestExecution(UnitTestMethod test)
        {
            return SetTestExecution(test);
        }
        protected TestExecution SetTestExecution(UnitTestMethod test)
        {
            if(!_testExecutionLookupByMethodInfo.TryGetValue(test.Method, out TestExecution execution))
            {
                TestSuiteDefinition suiteDefinition = GetTestSuiteDefinition(test);
                TestDefinition testDefinition = GetTestDefinition(suiteDefinition.Title, test.Description);
                TestSuiteExecutionSummary executionSummary = GetTestSuiteExecutionSummary(suiteDefinition);
                SaveTestExecutionResponse saveResponse = TestReportService.StartTest(executionSummary.Id, testDefinition.Id);
                if (saveResponse.Success)
                {
                    execution = saveResponse.TestExecution;
                    _testExecutionLookupByMethodInfo.TryAdd(test.Method, execution);
                }
                else
                {
                    Logger.Warning("Failed to get TestExecution: {0}", saveResponse.Message);
                }
            }
            return execution;
        }
    }
}
