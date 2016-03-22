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
    public class ShopItemQuery: Query<ShopItemColumns, ShopItem>
    { 
		public ShopItemQuery(){}
		public ShopItemQuery(WhereDelegate<ShopItemColumns> where, OrderBy<ShopItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopItemQuery(Func<ShopItemColumns, QueryFilter<ShopItemColumns>> where, OrderBy<ShopItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public ShopItemCollection Execute()
		{
			return new ShopItemCollection(this, true);
		}
    }
}