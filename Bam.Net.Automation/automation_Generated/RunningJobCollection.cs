/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class RunningJobCollection: DaoCollection<RunningJobColumns, RunningJob>
    { 
		public RunningJobCollection(){}
		public RunningJobCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RunningJobCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RunningJobCollection(Query<RunningJobColumns, RunningJob> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RunningJobCollection(Database db, Query<RunningJobColumns, RunningJob> q, bool load) : base(db, q, load) { }
		public RunningJobCollection(Query<RunningJobColumns, RunningJob> q, bool load) : base(q, load) { }
    }
}