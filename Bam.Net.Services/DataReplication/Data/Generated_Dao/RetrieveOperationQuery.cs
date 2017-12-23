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
    public class RetrieveOperationQuery: Query<RetrieveOperationColumns, RetrieveOperation>
    { 
		public RetrieveOperationQuery(){}
		public RetrieveOperationQuery(WhereDelegate<RetrieveOperationColumns> where, OrderBy<RetrieveOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public RetrieveOperationQuery(Func<RetrieveOperationColumns, QueryFilter<RetrieveOperationColumns>> where, OrderBy<RetrieveOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public RetrieveOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static RetrieveOperationQuery Where(WhereDelegate<RetrieveOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static RetrieveOperationQuery Where(WhereDelegate<RetrieveOperationColumns> where, OrderBy<RetrieveOperationColumns> orderBy = null, Database db = null)
        {
            return new RetrieveOperationQuery(where, orderBy, db);
        }

		public RetrieveOperationCollection Execute()
		{
			return new RetrieveOperationCollection(this, true);
		}
    }
}