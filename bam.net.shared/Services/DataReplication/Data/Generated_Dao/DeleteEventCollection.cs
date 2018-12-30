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
    public class DeleteEventCollection: DaoCollection<DeleteEventColumns, DeleteEvent>
    { 
		public DeleteEventCollection(){}
		public DeleteEventCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DeleteEventCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DeleteEventCollection(Query<DeleteEventColumns, DeleteEvent> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DeleteEventCollection(Database db, Query<DeleteEventColumns, DeleteEvent> q, bool load) : base(db, q, load) { }
		public DeleteEventCollection(Query<DeleteEventColumns, DeleteEvent> q, bool load) : base(q, load) { }
    }
}