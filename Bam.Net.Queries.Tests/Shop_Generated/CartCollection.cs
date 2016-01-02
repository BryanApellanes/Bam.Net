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
    public class CartCollection: DaoCollection<CartColumns, Cart>
    { 
		public CartCollection(){}
		public CartCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CartCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CartCollection(Query<CartColumns, Cart> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CartCollection(Database db, Query<CartColumns, Cart> q, bool load) : base(db, q, load) { }
		public CartCollection(Query<CartColumns, Cart> q, bool load) : base(q, load) { }
    }
}