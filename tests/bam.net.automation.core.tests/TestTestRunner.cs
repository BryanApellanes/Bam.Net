using Bam.Net.Logging;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.Tests
{
    public class TestTestRunner : Loggable, ITestRunner<UnitTestMethod>
    {
        public TestRunnerSummary TestSummary { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TestMethodProvider<UnitTestMethod> TestMethodProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Tag { get; set; }
        public event EventHandler InvalidTestNumberSpecified;
        public event EventHandler NoTestsDiscovered;
        public event EventHandler TestFailed;
        public event EventHandler TestFinished;
        public event EventHandler TestPassed;
        public event EventHandler TestsDiscovered;
        public event EventHandler TestsFinished;
        public event EventHandler TestsStarting;
        public event EventHandler TestStarting;

        public void FireAllEventsForTestingPurposes()
        {
            FireEvent(InvalidTestNumberSpecified);
            FireEvent(NoTestsDiscovered);
            FireEvent(TestFailed);
            FireEvent(TestFinished);
            FireEvent(TestPassed);
            FireEvent(TestsDiscovered);
            FireEvent(TestsFinished);
            FireEvent(TestsStarting);
            FireEvent(TestStarting);
        }

        public List<UnitTestMethod> GetTests()
        {
            throw new NotImplementedException();
        }

        public void RunAllTests()
        {
            throw new NotImplementedException();
        }

        public void RunTestGroup(string testGroup)
        {
            throw new NotImplementedException();
        }

        public void RunSpecifiedTests(string testIdentifiers)
        {
            throw new NotImplementedException();
        }

        public void RunTest(string testNumber)
        {
            throw new NotImplementedException();
        }

        public void RunTest(TestMethod test)
        {
            throw new NotImplementedException();
        }

        public void RunTestRange(string fromNumber, string toNumber)
        {
            throw new NotImplementedException();
        }

        public void RunTestSet(string[] testNumbers)
        {
            throw new NotImplementedException();
        }
    }
}
