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
    public class UserGroupCollection: DaoCollection<UserGroupColumns, UserGroup>
    { 
		public UserGroupCollection(){}
		public UserGroupCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserGroupCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserGroupCollection(Query<UserGroupColumns, UserGroup> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserGroupCollection(Database db, Query<UserGroupColumns, UserGroup> q, bool load) : base(db, q, load) { }
		public UserGroupCollection(Query<UserGroupColumns, UserGroup> q, bool load) : base(q, load) { }
    }
}