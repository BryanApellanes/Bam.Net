/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Testing
{
    public class TestExceptionEventArgs: EventArgs
    {
        public TestExceptionEventArgs() { }
        public TestExceptionEventArgs(TestMethod testMethod, Exception ex)
        {
            TestMethod = testMethod;
            Exception = ex;
        }
        public Exception Exception { get; set; }
        public TestMethod TestMethod { get; set; } 
    }
}
