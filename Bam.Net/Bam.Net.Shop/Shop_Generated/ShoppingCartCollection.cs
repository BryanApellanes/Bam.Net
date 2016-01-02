/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShoppingCartCollection: DaoCollection<ShoppingCartColumns, ShoppingCart>
    { 
		public ShoppingCartCollection(){}
		public ShoppingCartCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShoppingCartCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShoppingCartCollection(Query<ShoppingCartColumns, ShoppingCart> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShoppingCartCollection(Database db, Query<ShoppingCartColumns, ShoppingCart> q, bool load) : base(db, q, load) { }
		public ShoppingCartCollection(Query<ShoppingCartColumns, ShoppingCart> q, bool load) : base(q, load) { }
    }
}