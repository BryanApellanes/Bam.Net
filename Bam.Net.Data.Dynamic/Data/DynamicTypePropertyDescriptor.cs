using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    [Serializable]
    public class DynamicTypePropertyDescriptor: RepoData
    {
        public ulong DynamicTypeDescriptorId { get; set; }
        public virtual DynamicTypeDescriptor ParentType { get; set; }
        public string ParentTypeName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyName { get; set; }

        public override int GetHashCode()
        {
            return $"{DynamicTypeDescriptorId}:{ParentTypeName}:{PropertyType}:{PropertyName}".ToSha1Int();
        }

        public override bool Equals(object obj)
        {
            if(obj is DynamicTypePropertyDescriptor d)
            {
                return d.GetHashCode() == GetHashCode();
            }
            return false;
        }
    }
}
