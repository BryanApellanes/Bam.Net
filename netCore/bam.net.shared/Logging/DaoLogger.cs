/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Logging.Data;

namespace Bam.Net.Logging
{
    /// <summary>
    /// A basic database logger.  Logs all entries
    /// to a single table called LogEvent
    /// </summary>
    public class DaoLogger: Logger, Bam.Net.Logging.IDaoLogger
    {
		public DaoLogger()
			: base()
		{
		}

		public DaoLogger(Database logTo)
		{
			Database = logTo;
            Database.TryEnsureSchema<Data.LogEvent>();
        }

		public Database Database
		{
			get;
			set;
		}

        public override void CommitLogEvent(LogEvent logEvent)
        {
            Data.LogEvent logData = new Data.LogEvent
            {
                Source = logEvent.Source.First(4000),
                Category = logEvent.Category.First(4000),
                EventId = logEvent.EventID,
                User = logEvent.User.First(4000),
                Time = logEvent.Time,
                MessageSignature = logEvent.MessageSignature.First(4000),
                MessageVariableValues = logEvent.MessageVariableValues.ToDelimited(v => v, ",").First(4000),
                Message = logEvent.Message.First(4000),
                Computer = logEvent.Computer.First(4000),
                Severity = logEvent.Severity.ToString().First(4000)
            };

            logData.Save(Database);
        }
    }
}
