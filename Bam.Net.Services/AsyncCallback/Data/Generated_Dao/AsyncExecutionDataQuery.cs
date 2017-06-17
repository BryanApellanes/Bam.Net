/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
    public class AsyncExecutionDataQuery: Query<AsyncExecutionDataColumns, AsyncExecutionData>
    { 
		public AsyncExecutionDataQuery(){}
		public AsyncExecutionDataQuery(WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AsyncExecutionDataQuery(Func<AsyncExecutionDataColumns, QueryFilter<AsyncExecutionDataColumns>> where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AsyncExecutionDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AsyncExecutionDataQuery Where(WhereDelegate<AsyncExecutionDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static AsyncExecutionDataQuery Where(WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database db = null)
        {
            return new AsyncExecutionDataQuery(where, orderBy, db);
        }

		public AsyncExecutionDataCollection Execute()
		{
			return new AsyncExecutionDataCollection(this, true);
		}
    }
}