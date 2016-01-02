/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    /// <summary>
    /// A custom text file logger that logs events in
    /// a csv (comma separated values) format
    /// </summary>
    public class CsvLogger: TextFileLogger
    {
        class CsvLogEvent
        {
            public CsvLogEvent(LogEvent logEvent)
            {
                this.Source = logEvent.Source;
                this.Message = logEvent.Message;
                this.Computer = logEvent.Computer;
                this.Severity = logEvent.Severity.ToString();
                this.Category = logEvent.Category;
                this.EventId = logEvent.EventID.ToString();
                this.User = logEvent.User;
                this.Time = logEvent.Time.ToLongDateString();
            }
            public string Source { get; set; }
            public string Message { get; set; }
            public string Computer { get; set; }
            public string Severity { get; set; }
            public string Category { get; set; }
            public string EventId { get; set; }
            public string User { get; set; }
            public string Time { get; set; }
        }

        public CsvLogger()
            : base()
        {
            this.FileExtension = "csv";
        }

        protected override string GetLogText(LogEvent logEvent)
        {
            return new CsvLogEvent(logEvent).ToCsvLine();
        }

        protected override StringBuilder HandleDetails(LogEvent ev)
        {
            return new StringBuilder(ev.Message);
        }

        protected override void HandleStackTrace(Exception ex, StringBuilder message, StringBuilder stack)
        {
            // turn off the stack trace
        }
    }
}
