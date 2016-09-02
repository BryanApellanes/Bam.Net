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
    public class ShopItemAttributeQuery: Query<ShopItemAttributeColumns, ShopItemAttribute>
    { 
		public ShopItemAttributeQuery(){}
		public ShopItemAttributeQuery(WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopItemAttributeQuery(Func<ShopItemAttributeColumns, QueryFilter<ShopItemAttributeColumns>> where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopItemAttributeQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ShopItemAttributeQuery Where(WhereDelegate<ShopItemAttributeColumns> where)
        {
            return Where(where, null, null);
        }

        public static ShopItemAttributeQuery Where(WhereDelegate<ShopItemAttributeColumns> where, OrderBy<ShopItemAttributeColumns> orderBy = null, Database db = null)
        {
            return new ShopItemAttributeQuery(where, orderBy, db);
        }

		public ShopItemAttributeCollection Execute()
		{
			return new ShopItemAttributeCollection(this, true);
		}
    }
}