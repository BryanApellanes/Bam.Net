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
    public class CartQuery: Query<CartColumns, Cart>
    { 
		public CartQuery(){}
		public CartQuery(WhereDelegate<CartColumns> where, OrderBy<CartColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CartQuery(Func<CartColumns, QueryFilter<CartColumns>> where, OrderBy<CartColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CartQuery(Delegate where, Database db = null) : base(where, db) { }

		public CartCollection Execute()
		{
			return new CartCollection(this, true);
		}
    }
}