using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class TestSuiteDefinitionColumns: QueryFilter<TestSuiteDefinitionColumns>, IFilterToken
    {
        public TestSuiteDefinitionColumns() { }
        public TestSuiteDefinitionColumns(string columnName)
            : base(columnName)
        { }
		
		public TestSuiteDefinitionColumns KeyColumn
		{
			get
			{
				return new TestSuiteDefinitionColumns("Id");
			}
		}	

				
        public TestSuiteDefinitionColumns Id
        {
            get
            {
                return new TestSuiteDefinitionColumns("Id");
            }
        }
        public TestSuiteDefinitionColumns Uuid
        {
            get
            {
                return new TestSuiteDefinitionColumns("Uuid");
            }
        }
        public TestSuiteDefinitionColumns Cuid
        {
            get
            {
                return new TestSuiteDefinitionColumns("Cuid");
            }
        }
        public TestSuiteDefinitionColumns Title
        {
            get
            {
                return new TestSuiteDefinitionColumns("Title");
            }
        }
        public TestSuiteDefinitionColumns CreatedBy
        {
            get
            {
                return new TestSuiteDefinitionColumns("CreatedBy");
            }
        }
        public TestSuiteDefinitionColumns ModifiedBy
        {
            get
            {
                return new TestSuiteDefinitionColumns("ModifiedBy");
            }
        }
        public TestSuiteDefinitionColumns Modified
        {
            get
            {
                return new TestSuiteDefinitionColumns("Modified");
            }
        }
        public TestSuiteDefinitionColumns Deleted
        {
            get
            {
                return new TestSuiteDefinitionColumns("Deleted");
            }
        }
        public TestSuiteDefinitionColumns Created
        {
            get
            {
                return new TestSuiteDefinitionColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(TestSuiteDefinition);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}