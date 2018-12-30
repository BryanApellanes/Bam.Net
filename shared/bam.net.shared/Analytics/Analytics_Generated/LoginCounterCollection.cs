/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class LoginCounterCollection: DaoCollection<LoginCounterColumns, LoginCounter>
    { 
		public LoginCounterCollection(){}
		public LoginCounterCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoginCounterCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoginCounterCollection(Query<LoginCounterColumns, LoginCounter> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoginCounterCollection(Database db, Query<LoginCounterColumns, LoginCounter> q, bool load) : base(db, q, load) { }
		public LoginCounterCollection(Query<LoginCounterColumns, LoginCounter> q, bool load) : base(q, load) { }
    }
}