/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
    public class SuiteDefinitionCollection: DaoCollection<SuiteDefinitionColumns, SuiteDefinition>
    { 
		public SuiteDefinitionCollection(){}
		public SuiteDefinitionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SuiteDefinitionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SuiteDefinitionCollection(Query<SuiteDefinitionColumns, SuiteDefinition> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SuiteDefinitionCollection(Database db, Query<SuiteDefinitionColumns, SuiteDefinition> q, bool load) : base(db, q, load) { }
		public SuiteDefinitionCollection(Query<SuiteDefinitionColumns, SuiteDefinition> q, bool load) : base(q, load) { }
    }
}