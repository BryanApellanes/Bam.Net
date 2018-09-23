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
    public class CreateOperationQuery: Query<CreateOperationColumns, CreateOperation>
    { 
		public CreateOperationQuery(){}
		public CreateOperationQuery(WhereDelegate<CreateOperationColumns> where, OrderBy<CreateOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CreateOperationQuery(Func<CreateOperationColumns, QueryFilter<CreateOperationColumns>> where, OrderBy<CreateOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CreateOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CreateOperationQuery Where(WhereDelegate<CreateOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static CreateOperationQuery Where(WhereDelegate<CreateOperationColumns> where, OrderBy<CreateOperationColumns> orderBy = null, Database db = null)
        {
            return new CreateOperationQuery(where, orderBy, db);
        }

		public CreateOperationCollection Execute()
		{
			return new CreateOperationCollection(this, true);
		}
    }
}