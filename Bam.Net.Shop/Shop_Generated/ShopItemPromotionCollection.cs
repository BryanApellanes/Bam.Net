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
    public class ShopItemPromotionCollection: DaoCollection<ShopItemPromotionColumns, ShopItemPromotion>
    { 
		public ShopItemPromotionCollection(){}
		public ShopItemPromotionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopItemPromotionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopItemPromotionCollection(Query<ShopItemPromotionColumns, ShopItemPromotion> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopItemPromotionCollection(Database db, Query<ShopItemPromotionColumns, ShopItemPromotion> q, bool load) : base(db, q, load) { }
		public ShopItemPromotionCollection(Query<ShopItemPromotionColumns, ShopItemPromotion> q, bool load) : base(q, load) { }
    }
}