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
    public class DaughterDaoCollection: DaoCollection<DaughterDaoColumns, DaughterDao>
    { 
		public DaughterDaoCollection(){}
		public DaughterDaoCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DaughterDaoCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DaughterDaoCollection(Query<DaughterDaoColumns, DaughterDao> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DaughterDaoCollection(Database db, Query<DaughterDaoColumns, DaughterDao> q, bool load) : base(db, q, load) { }
		public DaughterDaoCollection(Query<DaughterDaoColumns, DaughterDao> q, bool load) : base(q, load) { }
    }
}