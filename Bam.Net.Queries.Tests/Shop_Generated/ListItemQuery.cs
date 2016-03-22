/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class ListItemQuery: Query<ListItemColumns, ListItem>
    { 
		public ListItemQuery(){}
		public ListItemQuery(WhereDelegate<ListItemColumns> where, OrderBy<ListItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ListItemQuery(Func<ListItemColumns, QueryFilter<ListItemColumns>> where, OrderBy<ListItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ListItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public ListItemCollection Execute()
		{
			return new ListItemCollection(this, true);
		}
    }
}