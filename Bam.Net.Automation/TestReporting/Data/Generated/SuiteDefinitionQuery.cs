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
    public class SuiteDefinitionQuery: Query<SuiteDefinitionColumns, SuiteDefinition>
    { 
		public SuiteDefinitionQuery(){}
		public SuiteDefinitionQuery(WhereDelegate<SuiteDefinitionColumns> where, OrderBy<SuiteDefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SuiteDefinitionQuery(Func<SuiteDefinitionColumns, QueryFilter<SuiteDefinitionColumns>> where, OrderBy<SuiteDefinitionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SuiteDefinitionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SuiteDefinitionQuery Where(WhereDelegate<SuiteDefinitionColumns> where)
        {
            return Where(where, null, null);
        }

        public static SuiteDefinitionQuery Where(WhereDelegate<SuiteDefinitionColumns> where, OrderBy<SuiteDefinitionColumns> orderBy = null, Database db = null)
        {
            return new SuiteDefinitionQuery(where, orderBy, db);
        }

		public SuiteDefinitionCollection Execute()
		{
			return new SuiteDefinitionCollection(this, true);
		}
    }
}