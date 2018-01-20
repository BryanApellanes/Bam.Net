using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.AssemblyManagement.Data
{
    [Serializable]
    public class AssemblyReferenceDescriptor: KeyHashRepoData
    {
        internal string ReferencerName { get; set; }
        internal string ReferencedName { get; set; }
        public virtual List<AssemblyDescriptor> AssemblyDescriptors { get; set; }
        [CompositeKey]
        public string ReferencerHash { get; set; }
        [CompositeKey]
        public string ReferencedHash { get; set; }        
    }
}
