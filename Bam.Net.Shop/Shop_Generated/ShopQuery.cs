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
    public class ShopQuery: Query<ShopColumns, Shop>
    { 
		public ShopQuery(){}
		public ShopQuery(WhereDelegate<ShopColumns> where, OrderBy<ShopColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShopQuery(Func<ShopColumns, QueryFilter<ShopColumns>> where, OrderBy<ShopColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShopQuery(Delegate where, Database db = null) : base(where, db) { }

		public ShopCollection Execute()
		{
			return new ShopCollection(this, true);
		}
    }
}