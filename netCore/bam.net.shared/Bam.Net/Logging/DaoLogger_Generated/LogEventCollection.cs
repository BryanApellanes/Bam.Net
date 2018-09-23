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
    public class LogEventCollection: DaoCollection<LogEventColumns, LogEvent>
    { 
		public LogEventCollection(){}
		public LogEventCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LogEventCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LogEventCollection(Query<LogEventColumns, LogEvent> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LogEventCollection(Database db, Query<LogEventColumns, LogEvent> q, bool load) : base(db, q, load) { }
		public LogEventCollection(Query<LogEventColumns, LogEvent> q, bool load) : base(q, load) { }
    }
}