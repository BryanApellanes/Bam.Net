using Bam.Net.Data.Dynamic.Data.Dao.Repository;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    [Serializable]
    public class DynamicNamespaceDescriptor: CompositeKeyRepoData
    {
        public DynamicNamespaceDescriptor()
        {
        }

        static object _defaultLock = new object();
        public static DynamicNamespaceDescriptor GetDefault(DynamicTypeDataRepository repo)
        {
            lock (_defaultLock)
            {
                return repo.GetOneDynamicNamespaceDescriptorWhere(d => d.Namespace == DefaultNamespace);
            }
        }

        public static string DefaultNamespace
        {
            get
            {
                return "Bam.Net.Data.Dynamic.RuntimeTypes";
            }
        }

        [CompositeKey]
        public string Namespace { get; set; }

        public virtual List<DynamicTypeDescriptor> Types { get; set; }
    }
}
