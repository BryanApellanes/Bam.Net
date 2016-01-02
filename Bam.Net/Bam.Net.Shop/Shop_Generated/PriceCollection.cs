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
    public class PriceCollection: DaoCollection<PriceColumns, Price>
    { 
		public PriceCollection(){}
		public PriceCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PriceCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PriceCollection(Query<PriceColumns, Price> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PriceCollection(Database db, Query<PriceColumns, Price> q, bool load) : base(db, q, load) { }
		public PriceCollection(Query<PriceColumns, Price> q, bool load) : base(q, load) { }
    }
}