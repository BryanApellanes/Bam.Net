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

namespace Bam.Net.Testing
{
	public class UnitTestResult: RepoData
	{
        public UnitTestResult() : base()
        {
            ComputerName = Environment.MachineName;
        }
        public UnitTestResult(string description, bool passed) : this()
        {
            Description = description;
            Passed = passed;
        }
		public UnitTestResult(ConsoleInvokeableMethod cim):this()
		{
			MethodInfo method = cim.Method;
			MethodName = method.Name;
			Description = cim.Information;
			AssemblyFullName = method.DeclaringType.Assembly.FullName;
			Passed = true;
        }
		public UnitTestResult(TestExceptionEventArgs args)
			: this(args.ConsoleInvokeableMethod)
		{
			Passed = false;
			Exception = args.Exception.Message;
			StackTrace = args.Exception.StackTrace;
		}
        public string ComputerName { get; set; }
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
