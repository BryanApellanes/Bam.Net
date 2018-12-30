using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Caching
{
    public class QueryCache<T> where T : class, new()
    {
        ConcurrentDictionary<QueryContext, IEnumerable<T>> _typedQueryResults;
        public QueryCache()
        {
            _typedQueryResults = new ConcurrentDictionary<QueryContext, IEnumerable<T>>();            
        }

        public IEnumerable<T> Results(IQueryFilterable source, QueryFilter filter)
        {
            QueryContext queryContext = new QueryContext(source, filter);
            if (!_typedQueryResults.TryGetValue(queryContext, out IEnumerable<T> results))
            {
                results = Reload(queryContext);
            }
            return results;
        }

        public IEnumerable<T> Reload(IQueryFilterable source, QueryFilter filter)
        {
            return Reload(new QueryContext(source, filter));
        }

        public IEnumerable<T> Reload(QueryContext queryContext)
        {
            IEnumerable<T> results = queryContext.Retrieve<T>();
            _typedQueryResults.TryAdd(queryContext, results);
            return results;
        }
    }

    public class QueryCache
    {
        ConcurrentDictionary<QueryContext, IEnumerable<object>> _queryResults;
        public QueryCache()
        {
            _queryResults = new ConcurrentDictionary<QueryContext, IEnumerable<object>>();
        }

        public IEnumerable<object> Results(Type type, IQueryFilterable source, QueryFilter filter)
        {
            QueryContext queryContext = new QueryContext(source, filter);
            if (!_queryResults.TryGetValue(queryContext, out IEnumerable<object> results))
            {
                results = Reload(type, queryContext);
            }
            return results;
        }

        public IEnumerable<object> Reload(Type type, IQueryFilterable source, QueryFilter filter)
        {
            return Reload(type, new QueryContext(source, filter));
        }

        public event EventHandler<QueryCacheEventArgs> Reloading;

        public event EventHandler<QueryCacheEventArgs> Reloaded;

        public IEnumerable<object> Reload(Type type, QueryContext queryContext)
        {
            Reloading?.Invoke(this, new QueryCacheEventArgs { QueryContext = queryContext });
            IEnumerable<object> results = queryContext.Retrieve(type);
            _queryResults.TryAdd(queryContext, results);
            Reloaded?.Invoke(this, new QueryCacheEventArgs { QueryContext = queryContext });
            return results;
        }
    }
}
