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
    /// <summary>
    /// Represents the result of executing
    /// a single test
    /// </summary>
	[Serializable]
	public class TestExecution: RepoData
	{
		public long TestDefinitionId { get; set; }
		public virtual TestDefinition TestDefinition { get; set; }
        public DateTime? StartedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        /// <summary>
        /// Boolean indicating whether the test passed
        /// </summary>
        public bool Passed { get; set; }
        /// <summary>
        /// The exception message if any
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// The stack trace if any
        /// </summary>
        public string StackTrace { get; set; }

        public long TestSuiteExecutionSummaryId { get; set; }
		public virtual TestSuiteExecutionSummary TestSuiteExecutionSummary { get; set; }
    }
}
