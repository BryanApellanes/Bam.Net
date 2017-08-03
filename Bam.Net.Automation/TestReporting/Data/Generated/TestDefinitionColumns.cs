using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
    public class TestDefinitionColumns: QueryFilter<TestDefinitionColumns>, IFilterToken
    {
        public TestDefinitionColumns() { }
        public TestDefinitionColumns(string columnName)
            : base(columnName)
        { }
		
		public TestDefinitionColumns KeyColumn
		{
			get
			{
				return new TestDefinitionColumns("Id");
			}
		}	

				
        public TestDefinitionColumns Id
        {
            get
            {
                return new TestDefinitionColumns("Id");
            }
        }
        public TestDefinitionColumns Uuid
        {
            get
            {
                return new TestDefinitionColumns("Uuid");
            }
        }
        public TestDefinitionColumns Cuid
        {
            get
            {
                return new TestDefinitionColumns("Cuid");
            }
        }
        public TestDefinitionColumns Title
        {
            get
            {
                return new TestDefinitionColumns("Title");
            }
        }
        public TestDefinitionColumns Created
        {
            get
            {
                return new TestDefinitionColumns("Created");
            }
        }

        public TestDefinitionColumns SuiteDefinitionId
        {
            get
            {
                return new TestDefinitionColumns("SuiteDefinitionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(TestDefinition);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}