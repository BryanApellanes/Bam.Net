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
    public class TestSuiteDefinitionQuery: Query<TestSuiteDefinitionColumns, TestSuiteDefinition>
    { 
		public TestSuiteDefinitionQuery(){}
		public TestSuiteDefinitionQuery(WhereDelegate<TestSuiteDefinitionColumns> where, OrderBy<TestSuiteDefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestSuiteDefinitionQuery(Func<TestSuiteDefinitionColumns, QueryFilter<TestSuiteDefinitionColumns>> where, OrderBy<TestSuiteDefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestSuiteDefinitionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestSuiteDefinitionQuery Where(WhereDelegate<TestSuiteDefinitionColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestSuiteDefinitionQuery Where(WhereDelegate<TestSuiteDefinitionColumns> where, OrderBy<TestSuiteDefinitionColumns> orderBy = null, Database db = null)
        {
            return new TestSuiteDefinitionQuery(where, orderBy, db);
        }

		public TestSuiteDefinitionCollection Execute()
		{
			return new TestSuiteDefinitionCollection(this, true);
		}
    }
}