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
    public class ItemQuery: Query<ItemColumns, Item>
    { 
		public ItemQuery(){}
		public ItemQuery(WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ItemQuery(Func<ItemColumns, QueryFilter<ItemColumns>> where, OrderBy<ItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public ItemCollection Execute()
		{
			return new ItemCollection(this, true);
		}
    }
}