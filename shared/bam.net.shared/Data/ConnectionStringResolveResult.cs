/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bam.Net.Data
{
    public class ConnectionStringResolveResult
    {
        public ConnectionStringResolveResult(ConnectionStringSettings settings, Exception ex = null)
        {
            this.Settings = settings;
            this.Exception = ex;
            this.Success = ex == null;
        }

        public ConnectionStringSettings Settings { get; private set; }
        public Exception Exception { get; private set; }
        public bool Success { get; private set; }
    }
}
