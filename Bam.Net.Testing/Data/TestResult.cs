/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.CommandLine;
using System.Runtime.Serialization;
using Bam.Net.Testing;

namespace Bam.Net.Testing.Data
{
	public class TestResult: AuditRepoData
	{
        public TestResult() : base()
        {
            TestType = Testing.TestType.Unit.ToString();
        }
        public TestResult(string description, bool passed) : this()
        {
            Description = description;
            Passed = passed;
        }
		public TestResult(ConsoleMethod cim, TestType testType = Testing.TestType.Unit):this()
		{
			MethodInfo method = cim.Method;
			MethodName = method.Name;
			Description = cim.Information;
			AssemblyFullName = method.DeclaringType.Assembly.FullName;
			Passed = true;
            TestType = testType.ToString();
        }
		public TestResult(TestExceptionEventArgs args)
			: this(args.TestMethod)
		{
			Passed = false;
			Exception = args.Exception.Message;
			StackTrace = args.Exception.StackTrace;
		}

        public string TestType { get; set; }        
        /// <summary>
        /// Boolean indicating whether the test passed
        /// </summary>
		public bool Passed { get; set; }
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
        /// The exception message if any
        /// </summary>
		public string Exception { get; set; }
        /// <summary>
        /// The stack trace if any
        /// </summary>
		public string StackTrace { get; set; }
	}
}
