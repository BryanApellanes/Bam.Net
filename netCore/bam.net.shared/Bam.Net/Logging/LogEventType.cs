/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Logging
{
    /// <summary>
    /// The same values as Verbosity level.
    /// Both exist for clarity in specific
    /// contexts.
    /// </summary>
    public enum LogEventType : int
    {
        None = 0,
        Fatal = 1,
        Error = 2,
        Warning = 3,
        Information = 4,
        Custom = 5
    }
}
