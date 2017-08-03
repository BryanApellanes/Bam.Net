/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
    public class TestExecutionSummaryCollection: DaoCollection<TestExecutionSummaryColumns, TestExecutionSummary>
    { 
		public TestExecutionSummaryCollection(){}
		public TestExecutionSummaryCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestExecutionSummaryCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestExecutionSummaryCollection(Query<TestExecutionSummaryColumns, TestExecutionSummary> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestExecutionSummaryCollection(Database db, Query<TestExecutionSummaryColumns, TestExecutionSummary> q, bool load) : base(db, q, load) { }
		public TestExecutionSummaryCollection(Query<TestExecutionSummaryColumns, TestExecutionSummary> q, bool load) : base(q, load) { }
    }
}