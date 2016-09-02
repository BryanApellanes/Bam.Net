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
    public class ShopItemAttributeValueQuery: Query<ShopItemAttributeValueColumns, ShopItemAttributeValue>
    { 
		public ShopItemAttributeValueQuery(){}
		public ShopItemAttributeValueQuery(WhereDelegate<ShopItemAttributeValueColumns> where, OrderBy<ShopItemAttributeValueColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopItemAttributeValueQuery(Func<ShopItemAttributeValueColumns, QueryFilter<ShopItemAttributeValueColumns>> where, OrderBy<ShopItemAttributeValueColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopItemAttributeValueQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ShopItemAttributeValueQuery Where(WhereDelegate<ShopItemAttributeValueColumns> where)
        {
            return Where(where, null, null);
        }

        public static ShopItemAttributeValueQuery Where(WhereDelegate<ShopItemAttributeValueColumns> where, OrderBy<ShopItemAttributeValueColumns> orderBy = null, Database db = null)
        {
            return new ShopItemAttributeValueQuery(where, orderBy, db);
        }

		public ShopItemAttributeValueCollection Execute()
		{
			return new ShopItemAttributeValueCollection(this, true);
		}
    }
}