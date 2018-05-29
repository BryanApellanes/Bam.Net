using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CommandLine
{
    public class ProcessExitEventArgs: EventArgs
    {
        public EventArgs EventArgs { get; set; }
        public ProcessOutput ProcessOutput { get; set; }
    }
}
