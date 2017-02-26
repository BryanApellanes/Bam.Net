using System;
using System.Collections.Generic;

namespace Bam.Net.Data.Repositories
{
    public interface IQueryFilterable
    {
        IEnumerable<object> Query(Type pocoType, QueryFilter query);
        IEnumerable<T> Query<T>(QueryFilter query) where T : class, new();
    }
}