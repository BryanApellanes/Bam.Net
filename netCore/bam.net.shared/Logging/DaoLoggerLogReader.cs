/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.Logging
{
	public class DaoLoggerLogReader: DaoLoggerLogReaderBase
	{
		public DaoLoggerLogReader() { }
		public DaoLoggerLogReader(IDaoLogger logger)
		{
			Args.ThrowIfNull(logger, "logger");
			Args.ThrowIf<InvalidOperationException>(!(logger is DaoLogger), "Specified logger type is invalid, must extend {0}", typeof(DaoLogger).Name);

			this.Logger = logger;
		}

		#region ILogReader Members

		public override List<LogEntry> GetLogEntries(DateTime from, DateTime to)
		{
			Args.ThrowIfNull(Logger, "Logger");
			Data.LogEventCollection data = Data.LogEvent.Where(c => c.Time > from && c.Time < to, DaoLogger.Database);
			List<LogEntry> results = new List<LogEntry>();
			foreach(Data.LogEvent eventData in data)
			{
				LogEntry entry = eventData.CopyAs<LogEntry>();
                Enum.TryParse<Severity>(eventData.Severity, out Severity s);
                entry.Severity = s;
				entry.MessageVariableValues = eventData.MessageVariableValues.DelimitSplit(",", ";");
				results.Add(entry);
			}

			return results;
		}

		#endregion
	}
}
