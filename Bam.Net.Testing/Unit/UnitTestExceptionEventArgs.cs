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
    public class UnitTestExceptionEventArgs: EventArgs
    {
        public UnitTestExceptionEventArgs(ConsoleMethod consoleInvokeableMethod, Exception ex, UnitTestRunner runner)
        {
            this.ConsoleInvokeableMethod = consoleInvokeableMethod;
            this.Exception = ex;
            UnitTestRunner = runner;
        }
        public UnitTestRunner UnitTestRunner { get; set; }
        public Exception Exception { get; set; }
        public ConsoleMethod ConsoleInvokeableMethod { get; set; } 
    }
}
