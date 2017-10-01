using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.DistributedHashTable.Data;
using Bam.Net.CoreServices.DistributedHashTable.Data;

namespace Bam.Net.CoreServices.DistributedHashTable
{
    public interface ITypeResolver
    {
        Type ResolveType(Operation writeOperation);
        Type ResolveType(string nameSpace, string typeName);
    }
}
