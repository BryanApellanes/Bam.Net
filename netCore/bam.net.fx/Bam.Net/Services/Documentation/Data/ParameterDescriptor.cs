using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Documentation.Data
{
    [Serializable]
    public class ParameterDescriptor: RepoData
    {
        public long OperationDescriptorId { get; set; }
        public virtual OperationDescriptor OperationDescriptor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ObjectDescriptorId { get; set; }
        public virtual ObjectDescriptor Type { get; set; }
        public bool Required { get; set; } 
    }
}
