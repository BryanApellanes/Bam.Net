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
    public class PasswordCollection: DaoCollection<PasswordColumns, Password>
    { 
		public PasswordCollection(){}
		public PasswordCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PasswordCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordCollection(Query<PasswordColumns, Password> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordCollection(Database db, Query<PasswordColumns, Password> q, bool load) : base(db, q, load) { }
		public PasswordCollection(Query<PasswordColumns, Password> q, bool load) : base(q, load) { }
    }
}