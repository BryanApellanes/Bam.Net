using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class PsKill
    {
        static PsKill()
        {
            Path = "C:\\bam\\tools\\PsKill.exe";
        }

        public static string Path { get; set; }

        public static ProcessOutput Run(string computerName, string processIdOrName, int timeout = 600000, string userName = null, string password = null)
        {
            StringBuilder command = new StringBuilder();
            command.Append($"{Path} \\\\{computerName}");
            if (!string.IsNullOrEmpty(userName))
            {
                command.Append($" -u {userName}");
            }
            if (!string.IsNullOrEmpty(password))
            {
                command.Append($" -p {password}");
            }
            command.Append($" {processIdOrName}");

            return command.ToString().Run(timeout);
        }
    }
}
