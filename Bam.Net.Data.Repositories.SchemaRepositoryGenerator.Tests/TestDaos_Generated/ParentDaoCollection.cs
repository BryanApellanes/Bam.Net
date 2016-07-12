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
    public class ParentDaoCollection: DaoCollection<ParentDaoColumns, ParentDao>
    { 
		public ParentDaoCollection(){}
		public ParentDaoCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ParentDaoCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ParentDaoCollection(Query<ParentDaoColumns, ParentDao> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ParentDaoCollection(Database db, Query<ParentDaoColumns, ParentDao> q, bool load) : base(db, q, load) { }
		public ParentDaoCollection(Query<ParentDaoColumns, ParentDao> q, bool load) : base(q, load) { }
    }
}