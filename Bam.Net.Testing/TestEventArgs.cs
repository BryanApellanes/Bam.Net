using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public class TestEventArgs<TTestMethod>: EventArgs where TTestMethod : TestMethod
    {
        public ITestRunner<TTestMethod> TestRunner { get; set; }
        public ConsoleMethod CurrentTest { get; set; }
    }
}
