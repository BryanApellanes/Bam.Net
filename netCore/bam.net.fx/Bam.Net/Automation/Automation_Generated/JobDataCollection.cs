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
    public class JobDataCollection: DaoCollection<JobDataColumns, JobData>
    { 
		public JobDataCollection(){}
		public JobDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public JobDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public JobDataCollection(Query<JobDataColumns, JobData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public JobDataCollection(Database db, Query<JobDataColumns, JobData> q, bool load) : base(db, q, load) { }
		public JobDataCollection(Query<JobDataColumns, JobData> q, bool load) : base(q, load) { }
    }
}