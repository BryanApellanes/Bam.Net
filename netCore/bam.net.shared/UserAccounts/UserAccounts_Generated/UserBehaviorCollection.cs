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
    public class UserBehaviorCollection: DaoCollection<UserBehaviorColumns, UserBehavior>
    { 
		public UserBehaviorCollection(){}
		public UserBehaviorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserBehaviorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserBehaviorCollection(Query<UserBehaviorColumns, UserBehavior> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserBehaviorCollection(Database db, Query<UserBehaviorColumns, UserBehavior> q, bool load) : base(db, q, load) { }
		public UserBehaviorCollection(Query<UserBehaviorColumns, UserBehavior> q, bool load) : base(q, load) { }
    }
}