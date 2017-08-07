/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.CommandLine;

namespace Bam.Net.Automation.TestReporting.Data
{
	[Serializable]
	public class TestDefinition: RepoData
	{
		public long SuiteDefinitionId { get; set; }
		public virtual TestSuiteDefinition SuiteDefinition { get; set; }

		public string Title { get; set; }
		public virtual TestExecution[] TestExecutions { get; set; }
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

	}
}
