using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging
{
    public class ErrorMessage: LogMessage
    {
        public ErrorMessage(string format, Exception ex, params string[] args)
        {
            Format = format;
            Exception = ex;
            FormatArgs = args;
        }

        public Exception Exception { get; set; }
        public override void Log(ILogger logger)
        {
            logger.AddEntry(Format, Exception, FormatArgs);
        }
    }
}
