/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class CartItemQuery: Query<CartItemColumns, CartItem>
    { 
		public CartItemQuery(){}
		public CartItemQuery(WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CartItemQuery(Func<CartItemColumns, QueryFilter<CartItemColumns>> where, OrderBy<CartItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CartItemQuery(Delegate where, Database db = null) : base(where, db) { }

		public CartItemCollection Execute()
		{
			return new CartItemCollection(this, true);
		}
    }
}