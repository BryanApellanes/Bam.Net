/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Logging
{
    public abstract class BlockingLogger: Logger
    {
        public override void RestartLoggingThread()
        {
            // this is a blocking logger
        }

        public override void StartLoggingThread()
        {
            //
        }

        public override void StopLoggingThread()
        {
            //
        }

        protected override void QueueLogEvent(LogEvent logEvent)
        {
            CommitLogEvent(logEvent);
        }
    }
}
