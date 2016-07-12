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
    public class HouseDaoParentDaoCollection: DaoCollection<HouseDaoParentDaoColumns, HouseDaoParentDao>
    { 
		public HouseDaoParentDaoCollection(){}
		public HouseDaoParentDaoCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HouseDaoParentDaoCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HouseDaoParentDaoCollection(Query<HouseDaoParentDaoColumns, HouseDaoParentDao> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HouseDaoParentDaoCollection(Database db, Query<HouseDaoParentDaoColumns, HouseDaoParentDao> q, bool load) : base(db, q, load) { }
		public HouseDaoParentDaoCollection(Query<HouseDaoParentDaoColumns, HouseDaoParentDao> q, bool load) : base(q, load) { }
    }
}