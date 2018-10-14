using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class TestExecutionColumns: QueryFilter<TestExecutionColumns>, IFilterToken
    {
        public TestExecutionColumns() { }
        public TestExecutionColumns(string columnName)
            : base(columnName)
        { }
		
		public TestExecutionColumns KeyColumn
		{
			get
			{
				return new TestExecutionColumns("Id");
			}
		}	

				
        public TestExecutionColumns Id
        {
            get
            {
                return new TestExecutionColumns("Id");
            }
        }
        public TestExecutionColumns Uuid
        {
            get
            {
                return new TestExecutionColumns("Uuid");
            }
        }
        public TestExecutionColumns Cuid
        {
            get
            {
                return new TestExecutionColumns("Cuid");
            }
        }
        public TestExecutionColumns StartedTime
        {
            get
            {
                return new TestExecutionColumns("StartedTime");
            }
        }
        public TestExecutionColumns FinishedTime
        {
            get
            {
                return new TestExecutionColumns("FinishedTime");
            }
        }
        public TestExecutionColumns Passed
        {
            get
            {
                return new TestExecutionColumns("Passed");
            }
        }
        public TestExecutionColumns Exception
        {
            get
            {
                return new TestExecutionColumns("Exception");
            }
        }
        public TestExecutionColumns StackTrace
        {
            get
            {
                return new TestExecutionColumns("StackTrace");
            }
        }
        public TestExecutionColumns Tag
        {
            get
            {
                return new TestExecutionColumns("Tag");
            }
        }
        public TestExecutionColumns CreatedBy
        {
            get
            {
                return new TestExecutionColumns("CreatedBy");
            }
        }
        public TestExecutionColumns ModifiedBy
        {
            get
            {
                return new TestExecutionColumns("ModifiedBy");
            }
        }
        public TestExecutionColumns Modified
        {
            get
            {
                return new TestExecutionColumns("Modified");
            }
        }
        public TestExecutionColumns Deleted
        {
            get
            {
                return new TestExecutionColumns("Deleted");
            }
        }
        public TestExecutionColumns Created
        {
            get
            {
                return new TestExecutionColumns("Created");
            }
        }

        public TestExecutionColumns TestDefinitionId
        {
            get
            {
                return new TestExecutionColumns("TestDefinitionId");
            }
        }
        public TestExecutionColumns TestSuiteExecutionSummaryId
        {
            get
            {
                return new TestExecutionColumns("TestSuiteExecutionSummaryId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(TestExecution);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}