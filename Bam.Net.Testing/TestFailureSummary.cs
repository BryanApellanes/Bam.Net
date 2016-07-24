using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing
{
    public class TestFailureSummary
    {
        public TestFailureSummary()
        {
            FailedTests = new List<ConsoleInvokeableMethod>();
        }
        public List<ConsoleInvokeableMethod> FailedTests { get; set; }
    }
}
