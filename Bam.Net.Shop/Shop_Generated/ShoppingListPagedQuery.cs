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
    public class ShoppingListPagedQuery: PagedQuery<ShoppingListColumns, ShoppingList>
    { 
		public ShoppingListPagedQuery(ShoppingListColumns orderByColumn, ShoppingListQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}