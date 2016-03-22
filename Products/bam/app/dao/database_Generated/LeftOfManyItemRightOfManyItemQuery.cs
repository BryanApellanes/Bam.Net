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
    public class LeftOfManyItemRightOfManyItemQuery: Query<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem>
    { 
		public LeftOfManyItemRightOfManyItemQuery(){}
		public LeftOfManyItemRightOfManyItemQuery(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LeftOfManyItemRightOfManyItemQuery(Func<LeftOfManyItemRightOfManyItemColumns, QueryFilter<LeftOfManyItemRightOfManyItemColumns>> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LeftOfManyItemRightOfManyItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public LeftOfManyItemRightOfManyItemCollection Execute()
		{
			return new LeftOfManyItemRightOfManyItemCollection(this, true);
		}
    }
}