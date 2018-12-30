/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class QueryOperationQuery: Query<QueryOperationColumns, QueryOperation>
    { 
		public QueryOperationQuery(){}
		public QueryOperationQuery(WhereDelegate<QueryOperationColumns> where, OrderBy<QueryOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public QueryOperationQuery(Func<QueryOperationColumns, QueryFilter<QueryOperationColumns>> where, OrderBy<QueryOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public QueryOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static QueryOperationQuery Where(WhereDelegate<QueryOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static QueryOperationQuery Where(WhereDelegate<QueryOperationColumns> where, OrderBy<QueryOperationColumns> orderBy = null, Database db = null)
        {
            return new QueryOperationQuery(where, orderBy, db);
        }

		public QueryOperationCollection Execute()
		{
			return new QueryOperationCollection(this, true);
		}
    }
}