/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bryan.Common.Data
{
    public class CommonSubItemQuery: Query<CommonSubItemColumns, CommonSubItem>
    { 
		public CommonSubItemQuery(){}
		public CommonSubItemQuery(WhereDelegate<CommonSubItemColumns> where, OrderBy<CommonSubItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CommonSubItemQuery(Func<CommonSubItemColumns, QueryFilter<CommonSubItemColumns>> where, OrderBy<CommonSubItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CommonSubItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public CommonSubItemCollection Execute()
		{
			return new CommonSubItemCollection(this, true);
		}
    }
}