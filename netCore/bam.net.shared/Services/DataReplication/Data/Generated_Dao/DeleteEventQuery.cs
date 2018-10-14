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
    public class DeleteEventQuery: Query<DeleteEventColumns, DeleteEvent>
    { 
		public DeleteEventQuery(){}
		public DeleteEventQuery(WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DeleteEventQuery(Func<DeleteEventColumns, QueryFilter<DeleteEventColumns>> where, OrderBy<DeleteEventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DeleteEventQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DeleteEventQuery Where(WhereDelegate<DeleteEventColumns> where)
        {
            return Where(where, null, null);
        }

        public static DeleteEventQuery Where(WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy = null, Database db = null)
        {
            return new DeleteEventQuery(where, orderBy, db);
        }

		public DeleteEventCollection Execute()
		{
			return new DeleteEventCollection(this, true);
		}
    }
}