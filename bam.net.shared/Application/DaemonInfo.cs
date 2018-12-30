using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    /// <summary>
    /// Settings used by a daemon or service. 
    /// </summary>
    public class DaemonInfo
    {
        public DaemonInfo()
        {
            Copy = true;
        }

        public bool Copy { get; set; }
        public string Host { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.  This should be the full local path
        /// to the file on the destination host.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }
        public Dictionary<string, string> AppSettings { get; set; }

        public DaemonProcess ToDaemonProcess(string workingDirectory)
        {
            DaemonProcess process = ToDaemonProcess();
            process.FileName = Path.Combine(workingDirectory, FileName);
            process.WorkingDirectory = workingDirectory;
            return process;
        }
        
        public DaemonProcess ToDaemonProcess()
        {
            return new DaemonProcess
            {
                Name = Name,
                FileName = FileName,
                Arguments = Arguments,
                WorkingDirectory = WorkingDirectory
            };
        }
    }
}
