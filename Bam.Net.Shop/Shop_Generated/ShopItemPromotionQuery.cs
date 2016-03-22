/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShopItemPromotionQuery: Query<ShopItemPromotionColumns, ShopItemPromotion>
    { 
		public ShopItemPromotionQuery(){}
		public ShopItemPromotionQuery(WhereDelegate<ShopItemPromotionColumns> where, OrderBy<ShopItemPromotionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopItemPromotionQuery(Func<ShopItemPromotionColumns, QueryFilter<ShopItemPromotionColumns>> where, OrderBy<ShopItemPromotionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopItemPromotionQuery(Delegate where, Database db = null) : base(where, db) { }

		public ShopItemPromotionCollection Execute()
		{
			return new ShopItemPromotionCollection(this, true);
		}
    }
}