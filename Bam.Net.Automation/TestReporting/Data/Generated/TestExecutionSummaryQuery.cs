/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
    public class TestExecutionSummaryQuery: Query<TestExecutionSummaryColumns, TestExecutionSummary>
    { 
		public TestExecutionSummaryQuery(){}
		public TestExecutionSummaryQuery(WhereDelegate<TestExecutionSummaryColumns> where, OrderBy<TestExecutionSummaryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestExecutionSummaryQuery(Func<TestExecutionSummaryColumns, QueryFilter<TestExecutionSummaryColumns>> where, OrderBy<TestExecutionSummaryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestExecutionSummaryQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestExecutionSummaryQuery Where(WhereDelegate<TestExecutionSummaryColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestExecutionSummaryQuery Where(WhereDelegate<TestExecutionSummaryColumns> where, OrderBy<TestExecutionSummaryColumns> orderBy = null, Database db = null)
        {
            return new TestExecutionSummaryQuery(where, orderBy, db);
        }

		public TestExecutionSummaryCollection Execute()
		{
			return new TestExecutionSummaryCollection(this, true);
		}
    }
}