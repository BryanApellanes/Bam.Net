using System;
using System.Collections.Generic;

namespace Bam.Net.Testing
{
    public interface ITestRunner<TTestMethod> where TTestMethod : TestMethod
    {
        string Tag { get; set; }
        TestRunnerSummary TestSummary { get; set; }

        event EventHandler InvalidTestNumberSpecified;
        event EventHandler NoTestsDiscovered;
        event EventHandler TestFailed;
        event EventHandler TestFinished;
        event EventHandler TestPassed;
        event EventHandler TestsDiscovered;
        event EventHandler TestsFinished;
        event EventHandler TestsStarting;
        event EventHandler TestStarting;

        TestMethodProvider<TTestMethod> TestMethodProvider { get; set; }
        List<TTestMethod> GetTests();
        void RunAllTests();
        void RunSpecifiedTests(string testIdentifiers);
        void RunTest(string testNumber);
        void RunTest(TestMethod test);
        void RunTestRange(string fromNumber, string toNumber);
        void RunTestSet(string[] testNumbers);
    }
}