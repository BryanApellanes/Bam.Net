using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class PsExec
    {
        static PsExec()
        {
            Path = "C:\\bam\\tools\\PsExec.exe";
        }

        public static string Path { get; set; }

        public static ProcessOutput Run(string computerName, string command, int timeout = 600000)
        {
            return $"{Path} \\\\{computerName} {command}".Run(timeout);
        }

        public static ProcessOutput Run(string computerName, string command, Action<string> standout, Action<string> errorout, int timeout = 600000)
        {
            return $"{Path} \\\\{computerName} {command}".Run(standout, errorout, timeout);
        }
    }
}
