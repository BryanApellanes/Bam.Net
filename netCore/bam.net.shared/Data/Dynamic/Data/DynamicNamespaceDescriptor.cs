using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    [Serializable]
    public class DynamicNamespaceDescriptor: RepoData
    {
        public DynamicNamespaceDescriptor()
        {
        }
        public static string DefaultNamespace
        {
            get
            {
                return "Bam.Net.Data.Dynamic.RuntimeTypes";
            }
        }
        public string Namespace { get; set; }
        public virtual List<DynamicTypeDescriptor> Types { get; set; }
    }
}
