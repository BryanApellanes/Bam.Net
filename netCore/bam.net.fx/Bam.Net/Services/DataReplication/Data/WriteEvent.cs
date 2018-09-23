using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.DataReplication.Data
{
    /// <summary>
    /// A recordable instance of a data write
    /// event that occurred whether create, update or delete
    /// </summary>
    [Serializable]
    public class WriteEvent: RepoData
    {
        public OperationIntent Intent { get; set; }
        public string TypeNamespace { get; set; }
        public string Type { get; set; }

        public string InstanceCuid { get; set; }
        /// <summary>
        /// The properties that were written
        /// </summary>
        public List<DataProperty> Properties { get; set; }        
    }
}
