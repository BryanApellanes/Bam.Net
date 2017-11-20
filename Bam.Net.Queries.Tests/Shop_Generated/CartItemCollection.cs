/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class CartItemCollection: DaoCollection<CartItemColumns, CartItem>
    { 
		public CartItemCollection(){}
		public CartItemCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CartItemCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CartItemCollection(Query<CartItemColumns, CartItem> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CartItemCollection(Database db, Query<CartItemColumns, CartItem> q, bool load) : base(db, q, load) { }
		public CartItemCollection(Query<CartItemColumns, CartItem> q, bool load) : base(q, load) { }
    }
}