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
    public class RetrieveOperationCollection: DaoCollection<RetrieveOperationColumns, RetrieveOperation>
    { 
		public RetrieveOperationCollection(){}
		public RetrieveOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RetrieveOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RetrieveOperationCollection(Query<RetrieveOperationColumns, RetrieveOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RetrieveOperationCollection(Database db, Query<RetrieveOperationColumns, RetrieveOperation> q, bool load) : base(db, q, load) { }
		public RetrieveOperationCollection(Query<RetrieveOperationColumns, RetrieveOperation> q, bool load) : base(q, load) { }
    }
}