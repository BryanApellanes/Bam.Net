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
    public class UpdateOperationQuery: Query<UpdateOperationColumns, UpdateOperation>
    { 
		public UpdateOperationQuery(){}
		public UpdateOperationQuery(WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UpdateOperationQuery(Func<UpdateOperationColumns, QueryFilter<UpdateOperationColumns>> where, OrderBy<UpdateOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UpdateOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UpdateOperationQuery Where(WhereDelegate<UpdateOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static UpdateOperationQuery Where(WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy = null, Database db = null)
        {
            return new UpdateOperationQuery(where, orderBy, db);
        }

		public UpdateOperationCollection Execute()
		{
			return new UpdateOperationCollection(this, true);
		}
    }
}