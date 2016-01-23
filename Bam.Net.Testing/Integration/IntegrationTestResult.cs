using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing.Integration
{
    public class IntegrationTestResult: UnitTestResult
    {
        public IntegrationTestResult() { }
        public IntegrationTestResult(ConsoleInvokeableMethod cim) : base(cim) { }
        public IntegrationTestResult(TestExceptionEventArgs args) : base(args) { }

  //      public UnitTestResult(ConsoleInvokeableMethod cim)
  //      {
  //          MethodInfo method = cim.Method;
  //          this.MethodName = method.Name;
  //          this.Description = cim.Information;
  //          this.AssemblyFullName = method.DeclaringType.Assembly.FullName;
  //          this.Passed = true;
  //      }
  //      public UnitTestResult(TestExceptionEventArgs args)
		//	: this(args.ConsoleInvokeableMethod)
		//{
  //          this.Passed = false;
  //          this.Exception = args.Exception.Message;
  //          this.StackTrace = args.Exception.StackTrace;
  //      }
    }
}
