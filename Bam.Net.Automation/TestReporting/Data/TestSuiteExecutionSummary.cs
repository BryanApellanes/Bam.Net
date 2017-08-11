/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Automation.TestReporting.Data
{
	[Serializable]
	public class TestSuiteExecutionSummary : RepoData
    {
        public TestSuiteExecutionSummary()
        {
            ComputerName = Environment.MachineName;
        }
        public string Branch { get; set; }
        public string ComputerName { get; set; }
        public DateTime? StartedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public long SuiteDefinitionId { get; set; }
        public virtual TestSuiteDefinition SuiteDefinition { get; set; }
		public virtual TestExecution[] TestExecutions { get; set; }

        public DateTime LocalStartedTime
        {
            get
            {
                return StartedTime.Value.ToLocalTime();
            }
        }

        public DateTime LocalFinishedTime
        {
            get
            {
                return FinishedTime.Value.ToLocalTime();
            }
        }
	}
}
