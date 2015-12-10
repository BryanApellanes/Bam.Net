/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.ContinuousIntegration.Data
{
    public class BuildJobCollection: DaoCollection<BuildJobColumns, BuildJob>
    { 
		public BuildJobCollection(){}
		public BuildJobCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public BuildJobCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public BuildJobCollection(Query<BuildJobColumns, BuildJob> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public BuildJobCollection(Database db, Query<BuildJobColumns, BuildJob> q, bool load) : base(db, q, load) { }
		public BuildJobCollection(Query<BuildJobColumns, BuildJob> q, bool load) : base(q, load) { }
    }
}