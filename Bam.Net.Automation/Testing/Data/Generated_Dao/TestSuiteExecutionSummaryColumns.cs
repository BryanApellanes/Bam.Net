using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class TestSuiteExecutionSummaryColumns: QueryFilter<TestSuiteExecutionSummaryColumns>, IFilterToken
    {
        public TestSuiteExecutionSummaryColumns() { }
        public TestSuiteExecutionSummaryColumns(string columnName)
            : base(columnName)
        { }
		
		public TestSuiteExecutionSummaryColumns KeyColumn
		{
			get
			{
				return new TestSuiteExecutionSummaryColumns("Id");
			}
		}	

				
        public TestSuiteExecutionSummaryColumns Id
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Id");
            }
        }
        public TestSuiteExecutionSummaryColumns Uuid
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Uuid");
            }
        }
        public TestSuiteExecutionSummaryColumns Cuid
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Cuid");
            }
        }
        public TestSuiteExecutionSummaryColumns Branch
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Branch");
            }
        }
        public TestSuiteExecutionSummaryColumns ComputerName
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("ComputerName");
            }
        }
        public TestSuiteExecutionSummaryColumns StartedTime
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("StartedTime");
            }
        }
        public TestSuiteExecutionSummaryColumns FinishedTime
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("FinishedTime");
            }
        }
        public TestSuiteExecutionSummaryColumns TestSuiteDefinitionId
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("TestSuiteDefinitionId");
            }
        }
        public TestSuiteExecutionSummaryColumns CreatedBy
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("CreatedBy");
            }
        }
        public TestSuiteExecutionSummaryColumns ModifiedBy
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("ModifiedBy");
            }
        }
        public TestSuiteExecutionSummaryColumns Modified
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Modified");
            }
        }
        public TestSuiteExecutionSummaryColumns Deleted
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Deleted");
            }
        }
        public TestSuiteExecutionSummaryColumns Created
        {
            get
            {
                return new TestSuiteExecutionSummaryColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(TestSuiteExecutionSummary);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}