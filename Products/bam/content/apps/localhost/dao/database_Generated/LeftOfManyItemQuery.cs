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
    public class LeftOfManyItemQuery: Query<LeftOfManyItemColumns, LeftOfManyItem>
    { 
		public LeftOfManyItemQuery(){}
		public LeftOfManyItemQuery(WhereDelegate<LeftOfManyItemColumns> where, OrderBy<LeftOfManyItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LeftOfManyItemQuery(Func<LeftOfManyItemColumns, QueryFilter<LeftOfManyItemColumns>> where, OrderBy<LeftOfManyItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LeftOfManyItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public LeftOfManyItemCollection Execute()
		{
			return new LeftOfManyItemCollection(this, true);
		}
    }
}