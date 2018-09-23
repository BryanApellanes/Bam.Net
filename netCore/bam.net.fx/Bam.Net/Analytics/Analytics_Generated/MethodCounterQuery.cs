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
    public class MethodCounterQuery: Query<MethodCounterColumns, MethodCounter>
    { 
		public MethodCounterQuery(){}
		public MethodCounterQuery(WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MethodCounterQuery(Func<MethodCounterColumns, QueryFilter<MethodCounterColumns>> where, OrderBy<MethodCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MethodCounterQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MethodCounterQuery Where(WhereDelegate<MethodCounterColumns> where)
        {
            return Where(where, null, null);
        }

        public static MethodCounterQuery Where(WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy = null, Database db = null)
        {
            return new MethodCounterQuery(where, orderBy, db);
        }

		public MethodCounterCollection Execute()
		{
			return new MethodCounterCollection(this, true);
		}
    }
}