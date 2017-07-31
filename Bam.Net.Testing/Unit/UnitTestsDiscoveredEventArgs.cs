using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestsDiscoveredEventArgs: EventArgs
    {
        public UnitTestsDiscoveredEventArgs()
        {
            Tests = new List<ConsoleMethod>();
        }
        public Assembly Assembly { get; set; }
        public UnitTestRunner UnitTestRunner { get; set; }
        public List<ConsoleMethod> Tests { get; set; }
    }
}
