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
    public class TestSuiteDefinitionCollection: DaoCollection<TestSuiteDefinitionColumns, TestSuiteDefinition>
    { 
		public TestSuiteDefinitionCollection(){}
		public TestSuiteDefinitionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestSuiteDefinitionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestSuiteDefinitionCollection(Query<TestSuiteDefinitionColumns, TestSuiteDefinition> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestSuiteDefinitionCollection(Database db, Query<TestSuiteDefinitionColumns, TestSuiteDefinition> q, bool load) : base(db, q, load) { }
		public TestSuiteDefinitionCollection(Query<TestSuiteDefinitionColumns, TestSuiteDefinition> q, bool load) : base(q, load) { }
    }
}