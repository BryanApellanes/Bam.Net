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
    public class CounterQuery: Query<CounterColumns, Counter>
    { 
		public CounterQuery(){}
		public CounterQuery(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CounterQuery(Func<CounterColumns, QueryFilter<CounterColumns>> where, OrderBy<CounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CounterQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CounterQuery Where(WhereDelegate<CounterColumns> where)
        {
            return Where(where, null, null);
        }

        public static CounterQuery Where(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy = null, Database db = null)
        {
            return new CounterQuery(where, orderBy, db);
        }

		public CounterCollection Execute()
		{
			return new CounterCollection(this, true);
		}
    }
}