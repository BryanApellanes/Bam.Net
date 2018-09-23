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
    public class DeferredJobDataCollection: DaoCollection<DeferredJobDataColumns, DeferredJobData>
    { 
		public DeferredJobDataCollection(){}
		public DeferredJobDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DeferredJobDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DeferredJobDataCollection(Query<DeferredJobDataColumns, DeferredJobData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DeferredJobDataCollection(Database db, Query<DeferredJobDataColumns, DeferredJobData> q, bool load) : base(db, q, load) { }
		public DeferredJobDataCollection(Query<DeferredJobDataColumns, DeferredJobData> q, bool load) : base(q, load) { }
    }
}