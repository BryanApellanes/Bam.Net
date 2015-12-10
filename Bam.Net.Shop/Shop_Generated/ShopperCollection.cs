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
    public class ShopperCollection: DaoCollection<ShopperColumns, Shopper>
    { 
		public ShopperCollection(){}
		public ShopperCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopperCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopperCollection(Query<ShopperColumns, Shopper> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopperCollection(Database db, Query<ShopperColumns, Shopper> q, bool load) : base(db, q, load) { }
		public ShopperCollection(Query<ShopperColumns, Shopper> q, bool load) : base(q, load) { }
    }
}