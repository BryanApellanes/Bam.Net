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
    public class SaveOperationQuery: Query<SaveOperationColumns, SaveOperation>
    { 
		public SaveOperationQuery(){}
		public SaveOperationQuery(WhereDelegate<SaveOperationColumns> where, OrderBy<SaveOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SaveOperationQuery(Func<SaveOperationColumns, QueryFilter<SaveOperationColumns>> where, OrderBy<SaveOperationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SaveOperationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SaveOperationQuery Where(WhereDelegate<SaveOperationColumns> where)
        {
            return Where(where, null, null);
        }

        public static SaveOperationQuery Where(WhereDelegate<SaveOperationColumns> where, OrderBy<SaveOperationColumns> orderBy = null, Database db = null)
        {
            return new SaveOperationQuery(where, orderBy, db);
        }

		public SaveOperationCollection Execute()
		{
			return new SaveOperationCollection(this, true);
		}
    }
}