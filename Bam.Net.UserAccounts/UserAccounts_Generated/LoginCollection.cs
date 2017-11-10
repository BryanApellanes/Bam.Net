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
    public class LoginCollection: DaoCollection<LoginColumns, Login>
    { 
		public LoginCollection(){}
		public LoginCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoginCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoginCollection(Query<LoginColumns, Login> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoginCollection(Database db, Query<LoginColumns, Login> q, bool load) : base(db, q, load) { }
		public LoginCollection(Query<LoginColumns, Login> q, bool load) : base(q, load) { }
    }
}