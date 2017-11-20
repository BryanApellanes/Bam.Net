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
    public class TestExecutionCollection: DaoCollection<TestExecutionColumns, TestExecution>
    { 
		public TestExecutionCollection(){}
		public TestExecutionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestExecutionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestExecutionCollection(Query<TestExecutionColumns, TestExecution> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestExecutionCollection(Database db, Query<TestExecutionColumns, TestExecution> q, bool load) : base(db, q, load) { }
		public TestExecutionCollection(Query<TestExecutionColumns, TestExecution> q, bool load) : base(q, load) { }
    }
}