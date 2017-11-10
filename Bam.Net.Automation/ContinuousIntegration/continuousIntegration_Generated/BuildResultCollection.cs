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
    public class BuildResultCollection: DaoCollection<BuildResultColumns, BuildResult>
    { 
		public BuildResultCollection(){}
		public BuildResultCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public BuildResultCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public BuildResultCollection(Query<BuildResultColumns, BuildResult> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public BuildResultCollection(Database db, Query<BuildResultColumns, BuildResult> q, bool load) : base(db, q, load) { }
		public BuildResultCollection(Query<BuildResultColumns, BuildResult> q, bool load) : base(q, load) { }
    }
}