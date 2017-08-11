using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;

namespace Bam.Net.Automation.Tests
{
    public class TestUnitTestRunListener: UnitTestRunListener
    {
        public bool TestFailedRan { get; set; }
        public bool TestPassedRan { get; set; }
        public bool TestsStartingRan { get; set; }
        public bool TestStartingRan { get; set; }
        public bool TestsFinishedRan { get; set; }
        public bool TestFinishedRan { get; set; }

        public override void TestFailed(object sender, TestExceptionEventArgs args)
        {
            TestFailedRan = true;
        }
        public override void TestPassed(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestPassedRan = true;
        }
        public override void TestsStarting(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestsStartingRan = true;
        }
        public override void TestStarting(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestStartingRan = true;
        }
        public override void TestsFinished(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestsFinishedRan = true;
        }
        public override void TestFinished(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestFinishedRan = true;
        }
    }
}
