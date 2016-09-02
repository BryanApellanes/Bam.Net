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
    public class ShopperQuery: Query<ShopperColumns, Shopper>
    { 
		public ShopperQuery(){}
		public ShopperQuery(WhereDelegate<ShopperColumns> where, OrderBy<ShopperColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopperQuery(Func<ShopperColumns, QueryFilter<ShopperColumns>> where, OrderBy<ShopperColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopperQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ShopperQuery Where(WhereDelegate<ShopperColumns> where)
        {
            return Where(where, null, null);
        }

        public static ShopperQuery Where(WhereDelegate<ShopperColumns> where, OrderBy<ShopperColumns> orderBy = null, Database db = null)
        {
            return new ShopperQuery(where, orderBy, db);
        }

		public ShopperCollection Execute()
		{
			return new ShopperCollection(this, true);
		}
    }
}