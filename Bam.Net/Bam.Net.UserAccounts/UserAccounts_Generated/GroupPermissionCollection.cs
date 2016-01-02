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
    public class GroupPermissionCollection: DaoCollection<GroupPermissionColumns, GroupPermission>
    { 
		public GroupPermissionCollection(){}
		public GroupPermissionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public GroupPermissionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public GroupPermissionCollection(Query<GroupPermissionColumns, GroupPermission> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public GroupPermissionCollection(Database db, Query<GroupPermissionColumns, GroupPermission> q, bool load) : base(db, q, load) { }
		public GroupPermissionCollection(Query<GroupPermissionColumns, GroupPermission> q, bool load) : base(q, load) { }
    }
}