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
    public class PasswordQuestionCollection: DaoCollection<PasswordQuestionColumns, PasswordQuestion>
    { 
		public PasswordQuestionCollection(){}
		public PasswordQuestionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PasswordQuestionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordQuestionCollection(Query<PasswordQuestionColumns, PasswordQuestion> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordQuestionCollection(Database db, Query<PasswordQuestionColumns, PasswordQuestion> q, bool load) : base(db, q, load) { }
		public PasswordQuestionCollection(Query<PasswordQuestionColumns, PasswordQuestion> q, bool load) : base(q, load) { }
    }
}