/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Logging
{
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
