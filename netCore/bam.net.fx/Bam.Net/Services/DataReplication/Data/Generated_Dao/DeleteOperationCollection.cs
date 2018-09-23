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
    public class DeleteOperationCollection: DaoCollection<DeleteOperationColumns, DeleteOperation>
    { 
		public DeleteOperationCollection(){}
		public DeleteOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DeleteOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DeleteOperationCollection(Query<DeleteOperationColumns, DeleteOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DeleteOperationCollection(Database db, Query<DeleteOperationColumns, DeleteOperation> q, bool load) : base(db, q, load) { }
		public DeleteOperationCollection(Query<DeleteOperationColumns, DeleteOperation> q, bool load) : base(q, load) { }
    }
}