using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    [Serializable]
    public class BamProject
    {
        public string Uuid { get; set; }
        public CsFile[] CsFiles { get; set; }
        public AssemblyReference[] AssemblyReferences { get; set; }
    }
}
