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
    public class QueryStringDataCollection: DaoCollection<QueryStringDataColumns, QueryStringData>
    { 
		public QueryStringDataCollection(){}
		public QueryStringDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public QueryStringDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public QueryStringDataCollection(Query<QueryStringDataColumns, QueryStringData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public QueryStringDataCollection(Database db, Query<QueryStringDataColumns, QueryStringData> q, bool load) : base(db, q, load) { }
		public QueryStringDataCollection(Query<QueryStringDataColumns, QueryStringData> q, bool load) : base(q, load) { }
    }
}