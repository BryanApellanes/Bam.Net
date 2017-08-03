using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public abstract class TestRunListener<TTestMethod> : ITestRunListener<TTestMethod> where TTestMethod : TestMethod
    {
        public void TestFailed(object sender, EventArgs e)
        {
            TestFailed(sender, (TestExceptionEventArgs)e);
        }

        public void TestPassed(object sender, EventArgs e)
        {
            TestPassed(sender, (TestEventArgs<TTestMethod>)e);
        }

        public void TestsStarting(object sender, EventArgs e)
        {
            TestsStarting(sender, (TestEventArgs<TTestMethod>)e);
        }

        public void TestsFinished(object sender, EventArgs e)
        {
            TestsFinished(sender, (TestEventArgs<TTestMethod>)e);
        }

        public void TestStarting(object sender, EventArgs e)
        {
            TestStarting(sender, (TestEventArgs<TTestMethod>)e);
        }

        public void TestFinished(object sender, EventArgs e)
        {
            TestFinished(sender, (TestEventArgs<TTestMethod>)e);
        }

        public void Listen(ITestRunner<TTestMethod> runner)
        {
            runner.TestFailed += TestFailed;
            runner.TestPassed += TestPassed;
            runner.TestsStarting += TestsStarting;
            runner.TestStarting += TestStarting;
            runner.TestsFinished += TestsFinished;
            runner.TestFinished += TestFinished;
        }

        public abstract void TestFailed(object sender, TestExceptionEventArgs args);
        public abstract void TestPassed(object sender, TestEventArgs<TTestMethod> args);
        public virtual void TestsStarting(object sender, TestEventArgs<TTestMethod> args) { }
        public virtual void TestStarting(object sender, TestEventArgs<TTestMethod> args) { }
        public virtual void TestsFinished(object sender, TestEventArgs<TTestMethod> args) { }
        public virtual void TestFinished(object sender, TestEventArgs<TTestMethod> args) { }
    }
}

