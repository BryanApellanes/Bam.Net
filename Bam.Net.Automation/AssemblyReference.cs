using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    [Serializable]
    public class AssemblyReference
    {
        public AssemblyReference()
        {
            Uuid = System.Guid.NewGuid().ToString();
        }
        public string Uuid { get; set; }
        public string AssemblyName { get; set; }
    }    
}
