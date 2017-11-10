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
    public class EventCollection: DaoCollection<EventColumns, Event>
    { 
		public EventCollection(){}
		public EventCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EventCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EventCollection(Query<EventColumns, Event> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EventCollection(Database db, Query<EventColumns, Event> q, bool load) : base(db, q, load) { }
		public EventCollection(Query<EventColumns, Event> q, bool load) : base(q, load) { }
    }
}