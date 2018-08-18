using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Caching
{
    public class QueryContext
    {
        public QueryContext(IQueryFilterable datasource, QueryFilter filter)
        {
            DataSource = datasource;
            Filter = filter;
        }
        public QueryFilter Filter { get; set; }
        public IQueryFilterable DataSource { get; set; }
        public IEnumerable<T> Retrieve<T>() where T : class, new()
        {
            return DataSource.Query<T>(this.Filter);
        }

        public IEnumerable<object> Retrieve(Type type)
        {
            return DataSource.Query(type, Filter);
        }

        public override bool Equals(object obj)
        {
            QueryContext ctx = obj as QueryContext;
            if(ctx != null)
            {
                return ctx.Filter.Equals(Filter) & ctx.DataSource.Equals(DataSource);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Filter.GetHashCode() + DataSource.GetHashCode();
        }
    }
}
