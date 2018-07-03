using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class WindowsServiceInfo
    {
        public WindowsServiceInfo()
        {
            Host = string.Empty;
            Name = string.Empty;
            FileName = string.Empty;
            AppSettings = new Dictionary<string, string>();
        }

        public string Host { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public bool UseCredentials { get; set; }
        public Dictionary<string, string> AppSettings { get; set; }
    }
}
