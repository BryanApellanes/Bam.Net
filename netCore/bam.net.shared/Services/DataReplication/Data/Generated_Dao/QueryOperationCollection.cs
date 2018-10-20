/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class QueryOperationCollection: DaoCollection<QueryOperationColumns, QueryOperation>
    { 
		public QueryOperationCollection(){}
		public QueryOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public QueryOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public QueryOperationCollection(Query<QueryOperationColumns, QueryOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public QueryOperationCollection(Database db, Query<QueryOperationColumns, QueryOperation> q, bool load) : base(db, q, load) { }
		public QueryOperationCollection(Query<QueryOperationColumns, QueryOperation> q, bool load) : base(q, load) { }
    }
}