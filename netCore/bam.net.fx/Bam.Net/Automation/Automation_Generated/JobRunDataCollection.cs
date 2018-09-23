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
    public class JobRunDataCollection: DaoCollection<JobRunDataColumns, JobRunData>
    { 
		public JobRunDataCollection(){}
		public JobRunDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public JobRunDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public JobRunDataCollection(Query<JobRunDataColumns, JobRunData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public JobRunDataCollection(Database db, Query<JobRunDataColumns, JobRunData> q, bool load) : base(db, q, load) { }
		public JobRunDataCollection(Query<JobRunDataColumns, JobRunData> q, bool load) : base(q, load) { }
    }
}