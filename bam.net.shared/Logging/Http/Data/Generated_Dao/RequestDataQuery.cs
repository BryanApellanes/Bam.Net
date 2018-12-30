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
    public class RequestDataQuery: Query<RequestDataColumns, RequestData>
    { 
		public RequestDataQuery(){}
		public RequestDataQuery(WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public RequestDataQuery(Func<RequestDataColumns, QueryFilter<RequestDataColumns>> where, OrderBy<RequestDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public RequestDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static RequestDataQuery Where(WhereDelegate<RequestDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static RequestDataQuery Where(WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy = null, Database db = null)
        {
            return new RequestDataQuery(where, orderBy, db);
        }

		public RequestDataCollection Execute()
		{
			return new RequestDataCollection(this, true);
		}
    }
}