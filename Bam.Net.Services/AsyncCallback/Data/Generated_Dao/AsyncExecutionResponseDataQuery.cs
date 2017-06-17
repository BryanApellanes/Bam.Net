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
    public class AsyncExecutionResponseDataQuery: Query<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData>
    { 
		public AsyncExecutionResponseDataQuery(){}
		public AsyncExecutionResponseDataQuery(WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AsyncExecutionResponseDataQuery(Func<AsyncExecutionResponseDataColumns, QueryFilter<AsyncExecutionResponseDataColumns>> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AsyncExecutionResponseDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AsyncExecutionResponseDataQuery Where(WhereDelegate<AsyncExecutionResponseDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static AsyncExecutionResponseDataQuery Where(WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database db = null)
        {
            return new AsyncExecutionResponseDataQuery(where, orderBy, db);
        }

		public AsyncExecutionResponseDataCollection Execute()
		{
			return new AsyncExecutionResponseDataCollection(this, true);
		}
    }
}