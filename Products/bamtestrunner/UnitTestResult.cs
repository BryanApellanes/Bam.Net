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

namespace Bam.Net.Testing
{
	public class UnitTestResult: RepoData
	{
		public UnitTestResult() { }
		public UnitTestResult(ConsoleInvokeableMethod cim)
		{
			MethodInfo method = cim.Method;
			this.MethodName = method.Name;
			this.Description = cim.Information;
			this.AssemblyFullName = method.DeclaringType.Assembly.FullName;
			this.Passed = true;
		}
		public UnitTestResult(TestExceptionEventArgs args)
			: this(args.ConsoleInvokeableMethod)
		{
			this.Passed = false;
			this.Exception = args.Exception.Message;
			this.StackTrace = args.Exception.StackTrace;
		}

		public bool Passed { get; set; }
		public string MethodName { get; set; }
		public string Description { get; set; }
		public string AssemblyFullName { get; set; }
		public string Exception { get; set; }
		public string StackTrace { get; set; }
	}
}
