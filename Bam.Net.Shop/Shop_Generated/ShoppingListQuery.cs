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
    public class ShoppingListQuery: Query<ShoppingListColumns, ShoppingList>
    { 
		public ShoppingListQuery(){}
		public ShoppingListQuery(WhereDelegate<ShoppingListColumns> where, OrderBy<ShoppingListColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ShoppingListQuery(Func<ShoppingListColumns, QueryFilter<ShoppingListColumns>> where, OrderBy<ShoppingListColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ShoppingListQuery(Delegate where, Database db = null) : base(where, db) { }

		public ShoppingListCollection Execute()
		{
			return new ShoppingListCollection(this, true);
		}
    }
}