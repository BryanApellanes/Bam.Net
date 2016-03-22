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
    public class ShoppingCartItemQuery: Query<ShoppingCartItemColumns, ShoppingCartItem>
    { 
		public ShoppingCartItemQuery(){}
		public ShoppingCartItemQuery(WhereDelegate<ShoppingCartItemColumns> where, OrderBy<ShoppingCartItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShoppingCartItemQuery(Func<ShoppingCartItemColumns, QueryFilter<ShoppingCartItemColumns>> where, OrderBy<ShoppingCartItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShoppingCartItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public ShoppingCartItemCollection Execute()
		{
			return new ShoppingCartItemCollection(this, true);
		}
    }
}