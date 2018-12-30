using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Documentation.Data
{
    [Serializable]
    public class OperationDescriptor: RepoData
    {
        public string Name { get; set; }
        public virtual ObjectDescriptor ResponseType { get; set; }
        public virtual List<ParameterDescriptor> Parameters { get; set; }
    }
}
