using Bam.Net.Services.Distributed.Data;
using System;

namespace Bam.Net.Services.Distributed
{
    public interface IRepositoryTypeResolver: ITypeResolver
    {
        Type ResolveType(Operation writeOperation);
    }
}
