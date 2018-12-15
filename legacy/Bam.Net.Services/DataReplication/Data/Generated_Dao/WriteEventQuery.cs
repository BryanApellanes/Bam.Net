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
    public class WriteEventQuery: Query<WriteEventColumns, WriteEvent>
    { 
		public WriteEventQuery(){}
		public WriteEventQuery(WhereDelegate<WriteEventColumns> where, OrderBy<WriteEventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public WriteEventQuery(Func<WriteEventColumns, QueryFilter<WriteEventColumns>> where, OrderBy<WriteEventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public WriteEventQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static WriteEventQuery Where(WhereDelegate<WriteEventColumns> where)
        {
            return Where(where, null, null);
        }

        public static WriteEventQuery Where(WhereDelegate<WriteEventColumns> where, OrderBy<WriteEventColumns> orderBy = null, Database db = null)
        {
            return new WriteEventQuery(where, orderBy, db);
        }

		public WriteEventCollection Execute()
		{
			return new WriteEventCollection(this, true);
		}
    }
}