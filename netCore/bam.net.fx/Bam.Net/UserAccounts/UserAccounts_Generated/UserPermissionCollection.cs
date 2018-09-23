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
    public class UserPermissionCollection: DaoCollection<UserPermissionColumns, UserPermission>
    { 
		public UserPermissionCollection(){}
		public UserPermissionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserPermissionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserPermissionCollection(Query<UserPermissionColumns, UserPermission> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserPermissionCollection(Database db, Query<UserPermissionColumns, UserPermission> q, bool load) : base(db, q, load) { }
		public UserPermissionCollection(Query<UserPermissionColumns, UserPermission> q, bool load) : base(q, load) { }
    }
}