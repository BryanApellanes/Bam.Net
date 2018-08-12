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
using Bam.Net.Testing.Unit;

namespace Bam.Net.Automation.Testing.Data
{
	[Serializable]
	public class TestDefinition: AuditRepoData
	{
		public ulong TestSuiteDefinitionId { get; set; }
		public virtual TestSuiteDefinition TestSuiteDefinition { get; set; }

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

        public static TestDefinition FromUnitTestMethod(UnitTestMethod testMethod)
        {
            return new TestDefinition
            {
                Title = testMethod.Description,
                TestType = testMethod.Method.DeclaringType.FullName,
                MethodName = testMethod.Method.Name,
                Description = testMethod.Description,
                AssemblyFullName = testMethod.Method.DeclaringType.Assembly.FullName
            };
        }
	}
}
