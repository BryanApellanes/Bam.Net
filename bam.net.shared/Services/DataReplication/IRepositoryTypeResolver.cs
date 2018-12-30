using Bam.Net.Services.DataReplication.Data;
using System;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// Resolves types for distributed operations
    /// </summary>
    public interface IRepositoryTypeResolver: ITypeResolver
    {
        Type ResolveType(Operation writeOperation);
    }
}
