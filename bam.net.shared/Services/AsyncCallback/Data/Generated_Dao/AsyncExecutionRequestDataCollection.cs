/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
    public class AsyncExecutionRequestDataCollection: DaoCollection<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData>
    { 
		public AsyncExecutionRequestDataCollection(){}
		public AsyncExecutionRequestDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AsyncExecutionRequestDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AsyncExecutionRequestDataCollection(Query<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AsyncExecutionRequestDataCollection(Database db, Query<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData> q, bool load) : base(db, q, load) { }
		public AsyncExecutionRequestDataCollection(Query<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData> q, bool load) : base(q, load) { }
    }
}