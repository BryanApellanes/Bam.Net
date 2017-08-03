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
    public class TestExecutionSummaryPagedQuery: PagedQuery<TestExecutionSummaryColumns, TestExecutionSummary>
    { 
		public TestExecutionSummaryPagedQuery(TestExecutionSummaryColumns orderByColumn, TestExecutionSummaryQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}