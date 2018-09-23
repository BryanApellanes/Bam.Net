/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Logging
{
    public abstract class BlockingLogger: Logger
    {
        public override ILogger RestartLoggingThread()
        {
            // this is a blocking logger
            return this;
        }

        public override ILogger StartLoggingThread()
        {
            return this;
        }

        public override ILogger StopLoggingThread()
        {
            return this;
        }

        protected override void QueueLogEvent(LogEvent logEvent)
        {
            CommitLogEvent(logEvent);
        }
    }
}
