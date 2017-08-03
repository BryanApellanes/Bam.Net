using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
    public class TestExecutionSummaryColumns: QueryFilter<TestExecutionSummaryColumns>, IFilterToken
    {
        public TestExecutionSummaryColumns() { }
        public TestExecutionSummaryColumns(string columnName)
            : base(columnName)
        { }
		
		public TestExecutionSummaryColumns KeyColumn
		{
			get
			{
				return new TestExecutionSummaryColumns("Id");
			}
		}	

				
        public TestExecutionSummaryColumns Id
        {
            get
            {
                return new TestExecutionSummaryColumns("Id");
            }
        }
        public TestExecutionSummaryColumns Uuid
        {
            get
            {
                return new TestExecutionSummaryColumns("Uuid");
            }
        }
        public TestExecutionSummaryColumns Cuid
        {
            get
            {
                return new TestExecutionSummaryColumns("Cuid");
            }
        }
        public TestExecutionSummaryColumns SuiteDefinitionId
        {
            get
            {
                return new TestExecutionSummaryColumns("SuiteDefinitionId");
            }
        }
        public TestExecutionSummaryColumns Created
        {
            get
            {
                return new TestExecutionSummaryColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(TestExecutionSummary);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}