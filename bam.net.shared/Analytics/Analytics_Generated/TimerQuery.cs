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
    public class TimerQuery: Query<TimerColumns, Timer>
    { 
		public TimerQuery(){}
		public TimerQuery(WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TimerQuery(Func<TimerColumns, QueryFilter<TimerColumns>> where, OrderBy<TimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TimerQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TimerQuery Where(WhereDelegate<TimerColumns> where)
        {
            return Where(where, null, null);
        }

        public static TimerQuery Where(WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy = null, Database db = null)
        {
            return new TimerQuery(where, orderBy, db);
        }

		public TimerCollection Execute()
		{
			return new TimerCollection(this, true);
		}
    }
}