/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class UserDataCollection: DaoCollection<UserDataColumns, UserData>
    { 
		public UserDataCollection(){}
		public UserDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserDataCollection(Query<UserDataColumns, UserData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserDataCollection(Database db, Query<UserDataColumns, UserData> q, bool load) : base(db, q, load) { }
		public UserDataCollection(Query<UserDataColumns, UserData> q, bool load) : base(q, load) { }
    }
}