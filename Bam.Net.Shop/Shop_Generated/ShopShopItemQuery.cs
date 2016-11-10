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
    public class ShopShopItemQuery: Query<ShopShopItemColumns, ShopShopItem>
    { 
		public ShopShopItemQuery(){}
		public ShopShopItemQuery(WhereDelegate<ShopShopItemColumns> where, OrderBy<ShopShopItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopShopItemQuery(Func<ShopShopItemColumns, QueryFilter<ShopShopItemColumns>> where, OrderBy<ShopShopItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopShopItemQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ShopShopItemQuery Where(WhereDelegate<ShopShopItemColumns> where)
        {
            return Where(where, null, null);
        }

        public static ShopShopItemQuery Where(WhereDelegate<ShopShopItemColumns> where, OrderBy<ShopShopItemColumns> orderBy = null, Database db = null)
        {
            return new ShopShopItemQuery(where, orderBy, db);
        }

		public ShopShopItemCollection Execute()
		{
			return new ShopShopItemCollection(this, true);
		}
    }
}