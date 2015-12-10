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
    public class ShopCollection: DaoCollection<ShopColumns, Shop>
    { 
		public ShopCollection(){}
		public ShopCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopCollection(Query<ShopColumns, Shop> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopCollection(Database db, Query<ShopColumns, Shop> q, bool load) : base(db, q, load) { }
		public ShopCollection(Query<ShopColumns, Shop> q, bool load) : base(q, load) { }
    }
}