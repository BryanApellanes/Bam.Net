using Bam.Net.Services.Distributed.Data;
using System;

namespace Bam.Net.Services.Distributed
{
    /// <summary>
    /// Resolves types for distributed operations
    /// </summary>
    public interface IRepositoryTypeResolver: ITypeResolver
    {
        Type ResolveType(Operation writeOperation);
    }
}
