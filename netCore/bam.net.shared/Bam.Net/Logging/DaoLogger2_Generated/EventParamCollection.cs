/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class EventParamCollection: DaoCollection<EventParamColumns, EventParam>
    { 
		public EventParamCollection(){}
		public EventParamCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EventParamCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EventParamCollection(Query<EventParamColumns, EventParam> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EventParamCollection(Database db, Query<EventParamColumns, EventParam> q, bool load) : base(db, q, load) { }
		public EventParamCollection(Query<EventParamColumns, EventParam> q, bool load) : base(q, load) { }
    }
}