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
    public class MethodTimerQuery: Query<MethodTimerColumns, MethodTimer>
    { 
		public MethodTimerQuery(){}
		public MethodTimerQuery(WhereDelegate<MethodTimerColumns> where, OrderBy<MethodTimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MethodTimerQuery(Func<MethodTimerColumns, QueryFilter<MethodTimerColumns>> where, OrderBy<MethodTimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MethodTimerQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MethodTimerQuery Where(WhereDelegate<MethodTimerColumns> where)
        {
            return Where(where, null, null);
        }

        public static MethodTimerQuery Where(WhereDelegate<MethodTimerColumns> where, OrderBy<MethodTimerColumns> orderBy = null, Database db = null)
        {
            return new MethodTimerQuery(where, orderBy, db);
        }

		public MethodTimerCollection Execute()
		{
			return new MethodTimerCollection(this, true);
		}
    }
}