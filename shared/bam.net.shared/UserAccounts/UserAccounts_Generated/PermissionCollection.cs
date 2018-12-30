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
    public class PermissionCollection: DaoCollection<PermissionColumns, Permission>
    { 
		public PermissionCollection(){}
		public PermissionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PermissionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PermissionCollection(Query<PermissionColumns, Permission> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PermissionCollection(Database db, Query<PermissionColumns, Permission> q, bool load) : base(db, q, load) { }
		public PermissionCollection(Query<PermissionColumns, Permission> q, bool load) : base(q, load) { }
    }
}