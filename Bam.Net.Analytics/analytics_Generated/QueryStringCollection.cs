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
    public class QueryStringCollection: DaoCollection<QueryStringColumns, QueryString>
    { 
		public QueryStringCollection(){}
		public QueryStringCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public QueryStringCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public QueryStringCollection(Query<QueryStringColumns, QueryString> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public QueryStringCollection(Database db, Query<QueryStringColumns, QueryString> q, bool load) : base(db, q, load) { }
		public QueryStringCollection(Query<QueryStringColumns, QueryString> q, bool load) : base(q, load) { }
    }
}