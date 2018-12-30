/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class QueryStringDataQuery: Query<QueryStringDataColumns, QueryStringData>
    { 
		public QueryStringDataQuery(){}
		public QueryStringDataQuery(WhereDelegate<QueryStringDataColumns> where, OrderBy<QueryStringDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public QueryStringDataQuery(Func<QueryStringDataColumns, QueryFilter<QueryStringDataColumns>> where, OrderBy<QueryStringDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public QueryStringDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static QueryStringDataQuery Where(WhereDelegate<QueryStringDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static QueryStringDataQuery Where(WhereDelegate<QueryStringDataColumns> where, OrderBy<QueryStringDataColumns> orderBy = null, Database db = null)
        {
            return new QueryStringDataQuery(where, orderBy, db);
        }

		public QueryStringDataCollection Execute()
		{
			return new QueryStringDataCollection(this, true);
		}
    }
}