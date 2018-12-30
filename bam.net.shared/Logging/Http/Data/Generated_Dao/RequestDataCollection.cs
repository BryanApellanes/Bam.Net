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
    public class RequestDataCollection: DaoCollection<RequestDataColumns, RequestData>
    { 
		public RequestDataCollection(){}
		public RequestDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RequestDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequestDataCollection(Query<RequestDataColumns, RequestData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequestDataCollection(Database db, Query<RequestDataColumns, RequestData> q, bool load) : base(db, q, load) { }
		public RequestDataCollection(Query<RequestDataColumns, RequestData> q, bool load) : base(q, load) { }
    }
}