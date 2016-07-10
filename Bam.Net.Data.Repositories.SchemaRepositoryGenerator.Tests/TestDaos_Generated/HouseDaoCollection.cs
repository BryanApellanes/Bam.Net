/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class HouseDaoCollection: DaoCollection<HouseDaoColumns, HouseDao>
    { 
		public HouseDaoCollection(){}
		public HouseDaoCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HouseDaoCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HouseDaoCollection(Query<HouseDaoColumns, HouseDao> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HouseDaoCollection(Database db, Query<HouseDaoColumns, HouseDao> q, bool load) : base(db, q, load) { }
		public HouseDaoCollection(Query<HouseDaoColumns, HouseDao> q, bool load) : base(q, load) { }
    }
}