/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.ExceptionHandling
{
    public class ExceptionEventArgs: EventArgs
    {
        public ExceptionEventArgs(Exception ex)
        {
            this.Exception = ex;
        }

        public Exception Exception { get; private set; }
    }
}
