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
    public class LockOutCollection: DaoCollection<LockOutColumns, LockOut>
    { 
		public LockOutCollection(){}
		public LockOutCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LockOutCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LockOutCollection(Query<LockOutColumns, LockOut> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LockOutCollection(Database db, Query<LockOutColumns, LockOut> q, bool load) : base(db, q, load) { }
		public LockOutCollection(Query<LockOutColumns, LockOut> q, bool load) : base(q, load) { }
    }
}