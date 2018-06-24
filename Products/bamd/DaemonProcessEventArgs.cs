using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class DaemonProcessEventArgs: EventArgs
    {
        public DaemonProcess BambotProcess { get; set; }
        public string Message { get; set; }
    }
}
