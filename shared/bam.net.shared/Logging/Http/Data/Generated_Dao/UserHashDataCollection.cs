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
    public class UserHashDataCollection: DaoCollection<UserHashDataColumns, UserHashData>
    { 
		public UserHashDataCollection(){}
		public UserHashDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserHashDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserHashDataCollection(Query<UserHashDataColumns, UserHashData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserHashDataCollection(Database db, Query<UserHashDataColumns, UserHashData> q, bool load) : base(db, q, load) { }
		public UserHashDataCollection(Query<UserHashDataColumns, UserHashData> q, bool load) : base(q, load) { }
    }
}