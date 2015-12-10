/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.CommandLine
{
    public class TestExceptionEventArgs: EventArgs
    {
        public TestExceptionEventArgs(ConsoleInvokeableMethod consoleInvokeableMethod, Exception ex)
        {
            this.ConsoleInvokeableMethod = consoleInvokeableMethod;
            this.Exception = ex;
        }

        public Exception Exception { get; set; }
        public ConsoleInvokeableMethod ConsoleInvokeableMethod { get; set; } 
    }
}
