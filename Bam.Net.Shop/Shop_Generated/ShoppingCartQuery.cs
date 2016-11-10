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
    public class ShoppingCartQuery: Query<ShoppingCartColumns, ShoppingCart>
    { 
		public ShoppingCartQuery(){}
		public ShoppingCartQuery(WhereDelegate<ShoppingCartColumns> where, OrderBy<ShoppingCartColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShoppingCartQuery(Func<ShoppingCartColumns, QueryFilter<ShoppingCartColumns>> where, OrderBy<ShoppingCartColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShoppingCartQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ShoppingCartQuery Where(WhereDelegate<ShoppingCartColumns> where)
        {
            return Where(where, null, null);
        }

        public static ShoppingCartQuery Where(WhereDelegate<ShoppingCartColumns> where, OrderBy<ShoppingCartColumns> orderBy = null, Database db = null)
        {
            return new ShoppingCartQuery(where, orderBy, db);
        }

		public ShoppingCartCollection Execute()
		{
			return new ShoppingCartCollection(this, true);
		}
    }
}