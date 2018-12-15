/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class LoadCounterQuery: Query<LoadCounterColumns, LoadCounter>
    { 
		public LoadCounterQuery(){}
		public LoadCounterQuery(WhereDelegate<LoadCounterColumns> where, OrderBy<LoadCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LoadCounterQuery(Func<LoadCounterColumns, QueryFilter<LoadCounterColumns>> where, OrderBy<LoadCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LoadCounterQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LoadCounterQuery Where(WhereDelegate<LoadCounterColumns> where)
        {
            return Where(where, null, null);
        }

        public static LoadCounterQuery Where(WhereDelegate<LoadCounterColumns> where, OrderBy<LoadCounterColumns> orderBy = null, Database db = null)
        {
            return new LoadCounterQuery(where, orderBy, db);
        }

		public LoadCounterCollection Execute()
		{
			return new LoadCounterCollection(this, true);
		}
    }
}