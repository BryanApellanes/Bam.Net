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
    public class ShopItemShopItemAttributeCollection: DaoCollection<ShopItemShopItemAttributeColumns, ShopItemShopItemAttribute>
    { 
		public ShopItemShopItemAttributeCollection(){}
		public ShopItemShopItemAttributeCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopItemShopItemAttributeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopItemShopItemAttributeCollection(Query<ShopItemShopItemAttributeColumns, ShopItemShopItemAttribute> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopItemShopItemAttributeCollection(Database db, Query<ShopItemShopItemAttributeColumns, ShopItemShopItemAttribute> q, bool load) : base(db, q, load) { }
		public ShopItemShopItemAttributeCollection(Query<ShopItemShopItemAttributeColumns, ShopItemShopItemAttribute> q, bool load) : base(q, load) { }
    }
}