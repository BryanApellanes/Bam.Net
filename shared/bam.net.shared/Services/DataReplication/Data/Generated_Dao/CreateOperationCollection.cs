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
    public class CreateOperationCollection: DaoCollection<CreateOperationColumns, CreateOperation>
    { 
		public CreateOperationCollection(){}
		public CreateOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CreateOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CreateOperationCollection(Query<CreateOperationColumns, CreateOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CreateOperationCollection(Database db, Query<CreateOperationColumns, CreateOperation> q, bool load) : base(db, q, load) { }
		public CreateOperationCollection(Query<CreateOperationColumns, CreateOperation> q, bool load) : base(q, load) { }
    }
}