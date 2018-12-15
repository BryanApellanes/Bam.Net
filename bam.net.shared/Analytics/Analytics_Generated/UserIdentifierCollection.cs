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
    public class UserIdentifierCollection: DaoCollection<UserIdentifierColumns, UserIdentifier>
    { 
		public UserIdentifierCollection(){}
		public UserIdentifierCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserIdentifierCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserIdentifierCollection(Query<UserIdentifierColumns, UserIdentifier> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserIdentifierCollection(Database db, Query<UserIdentifierColumns, UserIdentifier> q, bool load) : base(db, q, load) { }
		public UserIdentifierCollection(Query<UserIdentifierColumns, UserIdentifier> q, bool load) : base(q, load) { }
    }
}