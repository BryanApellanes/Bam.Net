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
    public class ClickCounterQuery: Query<ClickCounterColumns, ClickCounter>
    { 
		public ClickCounterQuery(){}
		public ClickCounterQuery(WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ClickCounterQuery(Func<ClickCounterColumns, QueryFilter<ClickCounterColumns>> where, OrderBy<ClickCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ClickCounterQuery(Delegate where, Database db = null) : base(where, db) { }

		public ClickCounterCollection Execute()
		{
			return new ClickCounterCollection(this, true);
		}
    }
}