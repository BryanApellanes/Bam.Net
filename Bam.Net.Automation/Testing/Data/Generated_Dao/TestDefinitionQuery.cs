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
    public class TestDefinitionQuery: Query<TestDefinitionColumns, TestDefinition>
    { 
		public TestDefinitionQuery(){}
		public TestDefinitionQuery(WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestDefinitionQuery(Func<TestDefinitionColumns, QueryFilter<TestDefinitionColumns>> where, OrderBy<TestDefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestDefinitionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestDefinitionQuery Where(WhereDelegate<TestDefinitionColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestDefinitionQuery Where(WhereDelegate<TestDefinitionColumns> where, OrderBy<TestDefinitionColumns> orderBy = null, Database db = null)
        {
            return new TestDefinitionQuery(where, orderBy, db);
        }

		public TestDefinitionCollection Execute()
		{
			return new TestDefinitionCollection(this, true);
		}
    }
}