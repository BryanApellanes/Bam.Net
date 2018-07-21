using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class BuildClientInfo
    {
        public string CoreHostName { get; set; }
        public string CorePort { get; set; }
        public string ContentRoot { get; set; }
    }
}
