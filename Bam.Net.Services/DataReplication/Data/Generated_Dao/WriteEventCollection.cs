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
    public class WriteEventCollection: DaoCollection<WriteEventColumns, WriteEvent>
    { 
		public WriteEventCollection(){}
		public WriteEventCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public WriteEventCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WriteEventCollection(Query<WriteEventColumns, WriteEvent> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WriteEventCollection(Database db, Query<WriteEventColumns, WriteEvent> q, bool load) : base(db, q, load) { }
		public WriteEventCollection(Query<WriteEventColumns, WriteEvent> q, bool load) : base(q, load) { }
    }
}