using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Documentation.Data
{
    [Serializable]
    public class ServiceDescriptor: RepoData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ObjectDescriptor> Objects { get; set; }
        public virtual List<OperationDescriptor> Operations { get; set; }
    }
}
