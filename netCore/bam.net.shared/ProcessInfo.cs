using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    /// <summary>
    /// Provides information about the current process.
    /// </summary>
    public class ProcessInfo
    {
        public ProcessInfo()
        {
            Process currentProcess = Process.GetCurrentProcess();
            MachineName = Environment.MachineName;
            ProcessId = currentProcess.Id;
            StartTime = currentProcess.StartTime;
            FilePath = Assembly.GetEntryAssembly().GetFilePath();
            CommandLine = Environment.CommandLine;
        }

        public string MachineName { get; set; }
        public int ProcessId { get; set; }
        public DateTime StartTime { get; set; }
        public string FilePath { get; set; }
        public string CommandLine { get; set; }
    }
}
