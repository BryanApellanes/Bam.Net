using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    [Serializable]
    public class DataInstancePropertyValue: RepoData
    {
        public long DataInstanceId { get; set; }
        public virtual DataInstance DataInstance { get; set; }
        public string RootHash { get; set; }
        public string InstanceHash { get; set; }
        public string ParentTypeName { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
