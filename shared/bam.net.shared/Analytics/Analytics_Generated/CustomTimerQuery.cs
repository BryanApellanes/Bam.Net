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
    public class CustomTimerQuery: Query<CustomTimerColumns, CustomTimer>
    { 
		public CustomTimerQuery(){}
		public CustomTimerQuery(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CustomTimerQuery(Func<CustomTimerColumns, QueryFilter<CustomTimerColumns>> where, OrderBy<CustomTimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CustomTimerQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CustomTimerQuery Where(WhereDelegate<CustomTimerColumns> where)
        {
            return Where(where, null, null);
        }

        public static CustomTimerQuery Where(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy = null, Database db = null)
        {
            return new CustomTimerQuery(where, orderBy, db);
        }

		public CustomTimerCollection Execute()
		{
			return new CustomTimerCollection(this, true);
		}
    }
}