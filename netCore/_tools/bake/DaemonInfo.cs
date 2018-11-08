using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class DaemonInfo
    {
        public bool Copy { get; set; }
        public string Host { get; set; }
        public string Name { get; set; }
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
