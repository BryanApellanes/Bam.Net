using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Services.Automation;
using Bam.Net.Testing;

namespace Bam.Net.Application
{
    [Serializable]
    public class ConsoleActions: CommandLineTestInterface
    {
        [ConsoleAction]
        public void DeleteLog()
        {
            string logName = Prompt("Which log do you want to delete?");
            ServiceExe.DeleteLog(false, logName);
        }
    }
}
