using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public class TestEventArgs<TTestMethod>: EventArgs where TTestMethod : TestMethod
    {
        public TestEventArgs() { }
        public ITestRunner<TTestMethod> TestRunner { get; set; }
        public ConsoleMethod Test { get; set; }
        public Assembly Assembly { get; set; }
    }
}
