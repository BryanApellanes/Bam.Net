/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Automation.Testing.Data
{
	[Serializable]
	public class TestSuiteExecutionSummary : AuditRepoData
    {
        public TestSuiteExecutionSummary()
        {
            ComputerName = Environment.MachineName;
        }
        public string Branch { get; set; }
        public string ComputerName { get; set; }
        public DateTime? StartedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public ulong TestSuiteDefinitionId { get; set; }
        public virtual TestSuiteDefinition TestSuiteDefinition { get; set; }
		public virtual TestExecution[] TestExecutions { get; set; }

        public DateTime? LocalStartedTime
        {
            get
            {
                return StartedTime?.ToLocalTime();
            }
        }

        public DateTime? LocalFinishedTime
        {
            get
            {
                return FinishedTime?.ToLocalTime();
            }
        }
	}
}
