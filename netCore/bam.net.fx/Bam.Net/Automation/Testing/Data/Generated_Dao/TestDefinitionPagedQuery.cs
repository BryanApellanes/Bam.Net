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
    public class TestDefinitionPagedQuery: PagedQuery<TestDefinitionColumns, TestDefinition>
    { 
		public TestDefinitionPagedQuery(TestDefinitionColumns orderByColumn, TestDefinitionQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}