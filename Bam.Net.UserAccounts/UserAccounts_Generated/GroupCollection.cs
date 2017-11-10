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
    public class GroupCollection: DaoCollection<GroupColumns, Group>
    { 
		public GroupCollection(){}
		public GroupCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public GroupCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public GroupCollection(Query<GroupColumns, Group> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public GroupCollection(Database db, Query<GroupColumns, Group> q, bool load) : base(db, q, load) { }
		public GroupCollection(Query<GroupColumns, Group> q, bool load) : base(q, load) { }
    }
}