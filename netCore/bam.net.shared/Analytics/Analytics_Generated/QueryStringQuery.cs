/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class QueryStringQuery: Query<QueryStringColumns, QueryString>
    { 
		public QueryStringQuery(){}
		public QueryStringQuery(WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public QueryStringQuery(Func<QueryStringColumns, QueryFilter<QueryStringColumns>> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public QueryStringQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static QueryStringQuery Where(WhereDelegate<QueryStringColumns> where)
        {
            return Where(where, null, null);
        }

        public static QueryStringQuery Where(WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null)
        {
            return new QueryStringQuery(where, orderBy, db);
        }

		public QueryStringCollection Execute()
		{
			return new QueryStringCollection(this, true);
		}
    }
}