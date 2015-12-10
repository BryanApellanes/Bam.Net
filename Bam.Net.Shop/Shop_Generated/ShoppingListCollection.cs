/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShoppingListCollection: DaoCollection<ShoppingListColumns, ShoppingList>
    { 
		public ShoppingListCollection(){}
		public ShoppingListCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShoppingListCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShoppingListCollection(Query<ShoppingListColumns, ShoppingList> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShoppingListCollection(Database db, Query<ShoppingListColumns, ShoppingList> q, bool load) : base(db, q, load) { }
		public ShoppingListCollection(Query<ShoppingListColumns, ShoppingList> q, bool load) : base(q, load) { }
    }
}