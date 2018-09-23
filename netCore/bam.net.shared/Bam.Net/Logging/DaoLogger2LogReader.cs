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
	public class DaoLogger2LogReader: DaoLoggerLogReaderBase
	{
		public DaoLogger2LogReader() { }
		public DaoLogger2LogReader(IDaoLogger logger)
		{
			Args.ThrowIfNull(logger, "logger");
			Args.ThrowIf<InvalidOperationException>(!(logger is DaoLogger2), "Specified logger type is invalid, must extend {0}", typeof(DaoLogger2).Name);

			this.Logger = logger;
		}

		#region ILogReader Members

		public override List<LogEntry> GetLogEntries(DateTime from, DateTime to)
		{
			Args.ThrowIfNull(Logger, "Logger");
			Data.EventCollection data = Data.Event.Where(c => c.Time > from && c.Time < to, DaoLogger.Database);
			List<LogEntry> results = new List<LogEntry>();
			foreach(Data.Event eventData in data)
			{
                LogEntry entry = new LogEntry
                {
                    Source = eventData.SourceNameOfSourceNameId.Value,
                    Category = eventData.CategoryNameOfCategoryNameId.Value,
                    EventID = Convert.ToInt32(eventData.Id),
                    User = eventData.UserNameOfUserNameId.Value,
                    Time = eventData.Time.Value,
                    MessageSignature = eventData.SignatureOfSignatureId.Value
                };
                List<Data.Param> args = new List<Data.Param>(eventData.Params);
				args.Sort((one, two) => one.Position.Value.CompareTo(two.Position.Value));
				entry.MessageVariableValues = args.Select(p => p.Value).ToArray();
				entry.Message = entry.MessageSignature._Format(args.ToArray());
				entry.Computer = eventData.ComputerNameOfComputerNameId.Value;
				entry.Severity = (Severity)eventData.Severity.Value;
				results.Add(entry);
			}

			return results;
		}

		#endregion

	}
}
