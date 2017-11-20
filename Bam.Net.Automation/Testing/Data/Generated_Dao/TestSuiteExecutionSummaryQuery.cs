/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class TestSuiteExecutionSummaryQuery: Query<TestSuiteExecutionSummaryColumns, TestSuiteExecutionSummary>
    { 
		public TestSuiteExecutionSummaryQuery(){}
		public TestSuiteExecutionSummaryQuery(WhereDelegate<TestSuiteExecutionSummaryColumns> where, OrderBy<TestSuiteExecutionSummaryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestSuiteExecutionSummaryQuery(Func<TestSuiteExecutionSummaryColumns, QueryFilter<TestSuiteExecutionSummaryColumns>> where, OrderBy<TestSuiteExecutionSummaryColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestSuiteExecutionSummaryQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestSuiteExecutionSummaryQuery Where(WhereDelegate<TestSuiteExecutionSummaryColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestSuiteExecutionSummaryQuery Where(WhereDelegate<TestSuiteExecutionSummaryColumns> where, OrderBy<TestSuiteExecutionSummaryColumns> orderBy = null, Database db = null)
        {
            return new TestSuiteExecutionSummaryQuery(where, orderBy, db);
        }

		public TestSuiteExecutionSummaryCollection Execute()
		{
			return new TestSuiteExecutionSummaryCollection(this, true);
		}
    }
}