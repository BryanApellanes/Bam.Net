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
    public class UserRoleCollection: DaoCollection<UserRoleColumns, UserRole>
    { 
		public UserRoleCollection(){}
		public UserRoleCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserRoleCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserRoleCollection(Query<UserRoleColumns, UserRole> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserRoleCollection(Database db, Query<UserRoleColumns, UserRole> q, bool load) : base(db, q, load) { }
		public UserRoleCollection(Query<UserRoleColumns, UserRole> q, bool load) : base(q, load) { }
    }
}