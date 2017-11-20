/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class RoleCollection: DaoCollection<RoleColumns, Role>
    { 
		public RoleCollection(){}
		public RoleCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RoleCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RoleCollection(Query<RoleColumns, Role> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RoleCollection(Database db, Query<RoleColumns, Role> q, bool load) : base(db, q, load) { }
		public RoleCollection(Query<RoleColumns, Role> q, bool load) : base(q, load) { }
    }
}