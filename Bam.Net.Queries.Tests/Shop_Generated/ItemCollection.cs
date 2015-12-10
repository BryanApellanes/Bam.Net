/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class ItemCollection: DaoCollection<ItemColumns, Item>
    { 
		public ItemCollection(){}
		public ItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ItemCollection(Query<ItemColumns, Item> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ItemCollection(Database db, Query<ItemColumns, Item> q, bool load) : base(db, q, load) { }
		public ItemCollection(Query<ItemColumns, Item> q, bool load) : base(q, load) { }
    }
}