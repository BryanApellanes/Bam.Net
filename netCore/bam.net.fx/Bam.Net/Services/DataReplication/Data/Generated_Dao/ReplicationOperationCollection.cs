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
    public class ReplicationOperationCollection: DaoCollection<ReplicationOperationColumns, ReplicationOperation>
    { 
		public ReplicationOperationCollection(){}
		public ReplicationOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ReplicationOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ReplicationOperationCollection(Query<ReplicationOperationColumns, ReplicationOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ReplicationOperationCollection(Database db, Query<ReplicationOperationColumns, ReplicationOperation> q, bool load) : base(db, q, load) { }
		public ReplicationOperationCollection(Query<ReplicationOperationColumns, ReplicationOperation> q, bool load) : base(q, load) { }
    }
}