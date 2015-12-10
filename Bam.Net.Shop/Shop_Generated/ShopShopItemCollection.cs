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
    public class ShopShopItemCollection: DaoCollection<ShopShopItemColumns, ShopShopItem>
    { 
		public ShopShopItemCollection(){}
		public ShopShopItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopShopItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopShopItemCollection(Query<ShopShopItemColumns, ShopShopItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopShopItemCollection(Database db, Query<ShopShopItemColumns, ShopShopItem> q, bool load) : base(db, q, load) { }
		public ShopShopItemCollection(Query<ShopShopItemColumns, ShopShopItem> q, bool load) : base(q, load) { }
    }
}