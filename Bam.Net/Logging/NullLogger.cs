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
    /// The logger that gets created for Log.Default when
    /// logging is not configured in the configuration file
    /// </summary>
    public class NullLogger: Logger
    {
        public NullLogger()
            : base()
        {
        }

        public override bool IsNull
        {
            get
            {
                return true;
            }
        }
        
        public override void CommitLogEvent(LogEvent logEvent)
        {
            // null logger is a place holder to prevent null reference
            // exceptions
            // doesn't actually do logging
        }
    }
}
