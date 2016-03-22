/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class RightOfManyItemQuery: Query<RightOfManyItemColumns, RightOfManyItem>
    { 
		public RightOfManyItemQuery(){}
		public RightOfManyItemQuery(WhereDelegate<RightOfManyItemColumns> where, OrderBy<RightOfManyItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public RightOfManyItemQuery(Func<RightOfManyItemColumns, QueryFilter<RightOfManyItemColumns>> where, OrderBy<RightOfManyItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public RightOfManyItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public RightOfManyItemCollection Execute()
		{
			return new RightOfManyItemCollection(this, true);
		}
    }
}