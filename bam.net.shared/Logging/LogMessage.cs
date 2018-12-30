using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging
{
    public class LogMessage
    {
        public LogMessage() { }
        public LogMessage(string format, params string[] formatArgs)
        {
            Format = format;
            FormatArgs = formatArgs;
        }
        public string Format { get; set; }
        public string[] FormatArgs { get; set; }

        public override string ToString()
        {
            return string.Format(Format, FormatArgs);
        }

        public virtual void Log(ILogger logger)
        {
            logger.AddEntry(Format, FormatArgs);
        }
    }
}
