using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public class TestsDiscoveredEventArgs<TTestMethod>: EventArgs where TTestMethod : TestMethod
    {
        public TestsDiscoveredEventArgs()
        {
            Tests = new List<TestMethod>();
        }
        public Assembly Assembly { get; set; }
        public ITestRunner<TTestMethod> TestRunner { get; set; }
        public List<TestMethod> Tests { get; set; }
    }
}
