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
        /// <summary>
        /// Boolean indicating whether the test passed
        /// </summary>
        public bool Passed { get; set; }
        /// <summary>
        /// The exception message if any
        /// </summary>
        public string Exception { get; set; }

		public long TestSummaryId { get; set; }
		public virtual TestExecutionSummary TestSummary { get; set; }

        public string TestType { get; set; }
        /// <summary>
        /// The name of the test method 
        /// </summary>
		public string MethodName { get; set; }
        /// <summary>
        /// The information value of the test method if any
        /// </summary>
		public string Description { get; set; }
        /// <summary>
        /// The full name of the assembly the test was in
        /// </summary>
		public string AssemblyFullName { get; set; }
        /// <summary>
        /// The stack trace if any
        /// </summary>
		public string StackTrace { get; set; }
    }
}
