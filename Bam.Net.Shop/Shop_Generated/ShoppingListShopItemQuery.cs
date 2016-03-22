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
    public class ShoppingListShopItemQuery: Query<ShoppingListShopItemColumns, ShoppingListShopItem>
    { 
		public ShoppingListShopItemQuery(){}
		public ShoppingListShopItemQuery(WhereDelegate<ShoppingListShopItemColumns> where, OrderBy<ShoppingListShopItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShoppingListShopItemQuery(Func<ShoppingListShopItemColumns, QueryFilter<ShoppingListShopItemColumns>> where, OrderBy<ShoppingListShopItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShoppingListShopItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public ShoppingListShopItemCollection Execute()
		{
			return new ShoppingListShopItemCollection(this, true);
		}
    }
}