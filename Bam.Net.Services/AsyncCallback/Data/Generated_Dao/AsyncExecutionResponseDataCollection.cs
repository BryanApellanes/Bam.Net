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
    public class AsyncExecutionResponseDataCollection: DaoCollection<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData>
    { 
		public AsyncExecutionResponseDataCollection(){}
		public AsyncExecutionResponseDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AsyncExecutionResponseDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AsyncExecutionResponseDataCollection(Query<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AsyncExecutionResponseDataCollection(Database db, Query<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData> q, bool load) : base(db, q, load) { }
		public AsyncExecutionResponseDataCollection(Query<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData> q, bool load) : base(q, load) { }
    }
}