using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Distributed.Data;
using Bam.Net.Services.DistributedService.Data;

namespace Bam.Net.Services.DistributedService
{
    public interface ITypeResolver
    {
        Type ResolveType(Operation writeOperation);
        Type ResolveType(string nameSpace, string typeName);
    }
}
