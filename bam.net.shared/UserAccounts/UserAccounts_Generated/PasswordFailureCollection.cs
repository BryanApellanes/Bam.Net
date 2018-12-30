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
    public class PasswordFailureCollection: DaoCollection<PasswordFailureColumns, PasswordFailure>
    { 
		public PasswordFailureCollection(){}
		public PasswordFailureCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PasswordFailureCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordFailureCollection(Query<PasswordFailureColumns, PasswordFailure> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordFailureCollection(Database db, Query<PasswordFailureColumns, PasswordFailure> q, bool load) : base(db, q, load) { }
		public PasswordFailureCollection(Query<PasswordFailureColumns, PasswordFailure> q, bool load) : base(q, load) { }
    }
}