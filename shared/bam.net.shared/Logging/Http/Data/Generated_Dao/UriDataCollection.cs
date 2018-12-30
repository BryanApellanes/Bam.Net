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
    public class UriDataCollection: DaoCollection<UriDataColumns, UriData>
    { 
		public UriDataCollection(){}
		public UriDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UriDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UriDataCollection(Query<UriDataColumns, UriData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UriDataCollection(Database db, Query<UriDataColumns, UriData> q, bool load) : base(db, q, load) { }
		public UriDataCollection(Query<UriDataColumns, UriData> q, bool load) : base(q, load) { }
    }
}