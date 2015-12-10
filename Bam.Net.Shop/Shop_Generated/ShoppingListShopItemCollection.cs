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
    public class ShoppingListShopItemCollection: DaoCollection<ShoppingListShopItemColumns, ShoppingListShopItem>
    { 
		public ShoppingListShopItemCollection(){}
		public ShoppingListShopItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShoppingListShopItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShoppingListShopItemCollection(Query<ShoppingListShopItemColumns, ShoppingListShopItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShoppingListShopItemCollection(Database db, Query<ShoppingListShopItemColumns, ShoppingListShopItem> q, bool load) : base(db, q, load) { }
		public ShoppingListShopItemCollection(Query<ShoppingListShopItemColumns, ShoppingListShopItem> q, bool load) : base(q, load) { }
    }
}