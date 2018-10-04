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
    public class TestExecutionQuery: Query<TestExecutionColumns, TestExecution>
    { 
		public TestExecutionQuery(){}
		public TestExecutionQuery(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestExecutionQuery(Func<TestExecutionColumns, QueryFilter<TestExecutionColumns>> where, OrderBy<TestExecutionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestExecutionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestExecutionQuery Where(WhereDelegate<TestExecutionColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestExecutionQuery Where(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null, Database db = null)
        {
            return new TestExecutionQuery(where, orderBy, db);
        }

		public TestExecutionCollection Execute()
		{
			return new TestExecutionCollection(this, true);
		}
    }
}