using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    [Serializable]
    public class DataInstancePropertyValue: CompositeKeyRepoData
    {
        public ulong DataInstanceId { get; set; }
        public virtual DataInstance DataInstance { get; set; }
        public string RootHash { get; set; }
        public string InstanceHash { get; set; }

        [CompositeKey]
        public string ParentTypeNamespace { get; set; }

        [CompositeKey]
        public string ParentTypeName { get; set; }

        [CompositeKey]
        public string PropertyName { get; set; }

        [CompositeKey]
        public string Value { get; set; }


    }
}
