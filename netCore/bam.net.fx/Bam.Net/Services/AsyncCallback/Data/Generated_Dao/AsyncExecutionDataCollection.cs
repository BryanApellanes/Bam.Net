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
    public class AsyncExecutionDataCollection: DaoCollection<AsyncExecutionDataColumns, AsyncExecutionData>
    { 
		public AsyncExecutionDataCollection(){}
		public AsyncExecutionDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AsyncExecutionDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AsyncExecutionDataCollection(Query<AsyncExecutionDataColumns, AsyncExecutionData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AsyncExecutionDataCollection(Database db, Query<AsyncExecutionDataColumns, AsyncExecutionData> q, bool load) : base(db, q, load) { }
		public AsyncExecutionDataCollection(Query<AsyncExecutionDataColumns, AsyncExecutionData> q, bool load) : base(q, load) { }
    }
}