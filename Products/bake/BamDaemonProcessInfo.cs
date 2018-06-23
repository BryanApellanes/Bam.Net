using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BamDaemonProcessInfo
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public Dictionary<string, string> AppSettings { get; set; }
    }
}
