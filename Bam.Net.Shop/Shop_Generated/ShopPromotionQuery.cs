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
    public class ShopPromotionQuery: Query<ShopPromotionColumns, ShopPromotion>
    { 
		public ShopPromotionQuery(){}
		public ShopPromotionQuery(WhereDelegate<ShopPromotionColumns> where, OrderBy<ShopPromotionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopPromotionQuery(Func<ShopPromotionColumns, QueryFilter<ShopPromotionColumns>> where, OrderBy<ShopPromotionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopPromotionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ShopPromotionQuery Where(WhereDelegate<ShopPromotionColumns> where)
        {
            return Where(where, null, null);
        }

        public static ShopPromotionQuery Where(WhereDelegate<ShopPromotionColumns> where, OrderBy<ShopPromotionColumns> orderBy = null, Database db = null)
        {
            return new ShopPromotionQuery(where, orderBy, db);
        }

		public ShopPromotionCollection Execute()
		{
			return new ShopPromotionCollection(this, true);
		}
    }
}