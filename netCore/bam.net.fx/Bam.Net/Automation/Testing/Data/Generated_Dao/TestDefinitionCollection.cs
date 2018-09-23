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
    public class TestDefinitionCollection: DaoCollection<TestDefinitionColumns, TestDefinition>
    { 
		public TestDefinitionCollection(){}
		public TestDefinitionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestDefinitionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestDefinitionCollection(Query<TestDefinitionColumns, TestDefinition> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestDefinitionCollection(Database db, Query<TestDefinitionColumns, TestDefinition> q, bool load) : base(db, q, load) { }
		public TestDefinitionCollection(Query<TestDefinitionColumns, TestDefinition> q, bool load) : base(q, load) { }
    }
}