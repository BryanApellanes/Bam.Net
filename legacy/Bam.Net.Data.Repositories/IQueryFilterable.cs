using System;
using System.Collections.Generic;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// When implemented by a derived class, enables querying of persisted types using
    /// QueryFilter objects.
    /// </summary>
    public interface IQueryFilterable
    {
        IEnumerable<object> Query(Type pocoType, QueryFilter query);
        IEnumerable<T> Query<T>(QueryFilter query) where T : class, new();
    }
}