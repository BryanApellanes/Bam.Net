using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestEventArgs: EventArgs
    {
        public UnitTestRunner UnitTestRunner { get; set; }
        public ConsoleMethod CurrentTest { get; set; }
    }
}
