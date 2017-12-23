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
    public class DeleteOperationQuery: Query<DeleteOperationColumns, DeleteOperation>
    { 
		public DeleteOperationQuery(){}
		public DeleteOperationQuery(WhereDelegate<DeleteOperationColumns> where, OrderBy<DeleteOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DeleteOperationQuery(Func<DeleteOperationColumns, QueryFilter<DeleteOperationColumns>> where, OrderBy<DeleteOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DeleteOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DeleteOperationQuery Where(WhereDelegate<DeleteOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static DeleteOperationQuery Where(WhereDelegate<DeleteOperationColumns> where, OrderBy<DeleteOperationColumns> orderBy = null, Database db = null)
        {
            return new DeleteOperationQuery(where, orderBy, db);
        }

		public DeleteOperationCollection Execute()
		{
			return new DeleteOperationCollection(this, true);
		}
    }
}