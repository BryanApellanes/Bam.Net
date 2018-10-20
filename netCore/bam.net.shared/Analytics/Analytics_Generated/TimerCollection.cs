/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class TimerCollection: DaoCollection<TimerColumns, Timer>
    { 
		public TimerCollection(){}
		public TimerCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TimerCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TimerCollection(Query<TimerColumns, Timer> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TimerCollection(Database db, Query<TimerColumns, Timer> q, bool load) : base(db, q, load) { }
		public TimerCollection(Query<TimerColumns, Timer> q, bool load) : base(q, load) { }
    }
}