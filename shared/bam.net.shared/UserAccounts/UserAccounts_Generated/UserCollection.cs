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
    public class UserCollection: DaoCollection<UserColumns, User>
    { 
		public UserCollection(){}
		public UserCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserCollection(Query<UserColumns, User> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserCollection(Database db, Query<UserColumns, User> q, bool load) : base(db, q, load) { }
		public UserCollection(Query<UserColumns, User> q, bool load) : base(q, load) { }
    }
}