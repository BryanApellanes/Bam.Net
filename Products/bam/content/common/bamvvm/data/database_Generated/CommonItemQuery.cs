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
    public class CommonItemQuery: Query<CommonItemColumns, CommonItem>
    { 
		public CommonItemQuery(){}
		public CommonItemQuery(WhereDelegate<CommonItemColumns> where, OrderBy<CommonItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CommonItemQuery(Func<CommonItemColumns, QueryFilter<CommonItemColumns>> where, OrderBy<CommonItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CommonItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public CommonItemCollection Execute()
		{
			return new CommonItemCollection(this, true);
		}
    }
}