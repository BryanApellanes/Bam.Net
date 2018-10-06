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
    public class UpdateOperationCollection: DaoCollection<UpdateOperationColumns, UpdateOperation>
    { 
		public UpdateOperationCollection(){}
		public UpdateOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UpdateOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UpdateOperationCollection(Query<UpdateOperationColumns, UpdateOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UpdateOperationCollection(Database db, Query<UpdateOperationColumns, UpdateOperation> q, bool load) : base(db, q, load) { }
		public UpdateOperationCollection(Query<UpdateOperationColumns, UpdateOperation> q, bool load) : base(q, load) { }
    }
}