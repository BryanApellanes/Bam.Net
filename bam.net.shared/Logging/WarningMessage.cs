using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging
{
    public class WarningMessage: LogMessage
    {
        public WarningMessage(string format, params string[] args) : base(format, args)
        {
        }

        public override void Log(ILogger logger)
        {
            logger.AddEntry(Format, LogEventType.Warning, FormatArgs);
        }
    }
}
