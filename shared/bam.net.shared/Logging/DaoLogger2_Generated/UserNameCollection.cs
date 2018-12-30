/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class UserNameCollection: DaoCollection<UserNameColumns, UserName>
    { 
		public UserNameCollection(){}
		public UserNameCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserNameCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserNameCollection(Query<UserNameColumns, UserName> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserNameCollection(Database db, Query<UserNameColumns, UserName> q, bool load) : base(db, q, load) { }
		public UserNameCollection(Query<UserNameColumns, UserName> q, bool load) : base(q, load) { }
    }
}