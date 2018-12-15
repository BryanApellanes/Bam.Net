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
    public class AsyncExecutionRequestDataQuery: Query<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData>
    { 
		public AsyncExecutionRequestDataQuery(){}
		public AsyncExecutionRequestDataQuery(WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AsyncExecutionRequestDataQuery(Func<AsyncExecutionRequestDataColumns, QueryFilter<AsyncExecutionRequestDataColumns>> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AsyncExecutionRequestDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AsyncExecutionRequestDataQuery Where(WhereDelegate<AsyncExecutionRequestDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static AsyncExecutionRequestDataQuery Where(WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database db = null)
        {
            return new AsyncExecutionRequestDataQuery(where, orderBy, db);
        }

		public AsyncExecutionRequestDataCollection Execute()
		{
			return new AsyncExecutionRequestDataCollection(this, true);
		}
    }
}