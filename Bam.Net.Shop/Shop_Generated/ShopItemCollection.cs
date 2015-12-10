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
    public class ShopItemCollection: DaoCollection<ShopItemColumns, ShopItem>
    { 
		public ShopItemCollection(){}
		public ShopItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopItemCollection(Query<ShopItemColumns, ShopItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopItemCollection(Database db, Query<ShopItemColumns, ShopItem> q, bool load) : base(db, q, load) { }
		public ShopItemCollection(Query<ShopItemColumns, ShopItem> q, bool load) : base(q, load) { }
    }
}