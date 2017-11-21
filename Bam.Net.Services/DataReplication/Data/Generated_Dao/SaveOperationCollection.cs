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
    public class SaveOperationCollection: DaoCollection<SaveOperationColumns, SaveOperation>
    { 
		public SaveOperationCollection(){}
		public SaveOperationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SaveOperationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SaveOperationCollection(Query<SaveOperationColumns, SaveOperation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SaveOperationCollection(Database db, Query<SaveOperationColumns, SaveOperation> q, bool load) : base(db, q, load) { }
		public SaveOperationCollection(Query<SaveOperationColumns, SaveOperation> q, bool load) : base(q, load) { }
    }
}