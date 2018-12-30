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
    public class ReplicationOperationQuery: Query<ReplicationOperationColumns, ReplicationOperation>
    { 
		public ReplicationOperationQuery(){}
		public ReplicationOperationQuery(WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ReplicationOperationQuery(Func<ReplicationOperationColumns, QueryFilter<ReplicationOperationColumns>> where, OrderBy<ReplicationOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ReplicationOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ReplicationOperationQuery Where(WhereDelegate<ReplicationOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static ReplicationOperationQuery Where(WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy = null, Database db = null)
        {
            return new ReplicationOperationQuery(where, orderBy, db);
        }

		public ReplicationOperationCollection Execute()
		{
			return new ReplicationOperationCollection(this, true);
		}
    }
}