/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class TestSuiteExecutionSummaryCollection: DaoCollection<TestSuiteExecutionSummaryColumns, TestSuiteExecutionSummary>
    { 
		public TestSuiteExecutionSummaryCollection(){}
		public TestSuiteExecutionSummaryCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestSuiteExecutionSummaryCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestSuiteExecutionSummaryCollection(Query<TestSuiteExecutionSummaryColumns, TestSuiteExecutionSummary> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestSuiteExecutionSummaryCollection(Database db, Query<TestSuiteExecutionSummaryColumns, TestSuiteExecutionSummary> q, bool load) : base(db, q, load) { }
		public TestSuiteExecutionSummaryCollection(Query<TestSuiteExecutionSummaryColumns, TestSuiteExecutionSummary> q, bool load) : base(q, load) { }
    }
}