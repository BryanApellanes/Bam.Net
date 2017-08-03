using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing
{
    public class TestRunnerSummary
    {
        public TestRunnerSummary()
        {
            FailedTests = new List<FailedTest>();
            PassedTests = new List<ConsoleMethod>();
        }
        public List<FailedTest> FailedTests { get; set; }
        public List<ConsoleMethod> PassedTests { get; set; }
    }
}
