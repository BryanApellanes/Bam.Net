using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
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
        public TestDefinitionColumns TestType
        {
            get
            {
                return new TestDefinitionColumns("TestType");
            }
        }
        public TestDefinitionColumns MethodName
        {
            get
            {
                return new TestDefinitionColumns("MethodName");
            }
        }
        public TestDefinitionColumns Description
        {
            get
            {
                return new TestDefinitionColumns("Description");
            }
        }
        public TestDefinitionColumns AssemblyFullName
        {
            get
            {
                return new TestDefinitionColumns("AssemblyFullName");
            }
        }
        public TestDefinitionColumns CreatedBy
        {
            get
            {
                return new TestDefinitionColumns("CreatedBy");
            }
        }
        public TestDefinitionColumns ModifiedBy
        {
            get
            {
                return new TestDefinitionColumns("ModifiedBy");
            }
        }
        public TestDefinitionColumns Modified
        {
            get
            {
                return new TestDefinitionColumns("Modified");
            }
        }
        public TestDefinitionColumns Deleted
        {
            get
            {
                return new TestDefinitionColumns("Deleted");
            }
        }
        public TestDefinitionColumns Created
        {
            get
            {
                return new TestDefinitionColumns("Created");
            }
        }

        public TestDefinitionColumns TestSuiteDefinitionId
        {
            get
            {
                return new TestDefinitionColumns("TestSuiteDefinitionId");
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