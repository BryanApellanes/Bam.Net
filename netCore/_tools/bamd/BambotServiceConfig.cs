using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BambotServiceConfig
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public string Args { get; set; }
        public bool Copy { get; set; }
        public dynamic AppSettings { get; set; }
    }
}
