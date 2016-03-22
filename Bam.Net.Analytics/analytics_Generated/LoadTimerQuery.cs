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
    public class LoadTimerQuery: Query<LoadTimerColumns, LoadTimer>
    { 
		public LoadTimerQuery(){}
		public LoadTimerQuery(WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LoadTimerQuery(Func<LoadTimerColumns, QueryFilter<LoadTimerColumns>> where, OrderBy<LoadTimerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LoadTimerQuery(Delegate where, Database db = null) : base(where, db) { }

		public LoadTimerCollection Execute()
		{
			return new LoadTimerCollection(this, true);
		}
    }
}