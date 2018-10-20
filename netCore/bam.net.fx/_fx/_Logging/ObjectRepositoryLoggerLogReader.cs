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
	public class ObjectRepositoryLoggerLogReader: ILogReader
	{
		public ObjectRepositoryLoggerLogReader() { }
		public ObjectRepositoryLoggerLogReader(ObjectRepositoryLogger logger)
		{
			this.Logger = logger;
		}
		#region ILogReader Members

		public ILogger Logger
		{
			get;
			set;
		}

		public List<LogEntry> GetLogEntries(DateTime from, DateTime to)
		{
			Args.ThrowIfNull(Logger);
			Type loggerType = Logger.GetType();
			Args.ThrowIf<InvalidOperationException>(loggerType != typeof(ObjectRepositoryLogger), "Invalid Logger specified ({0}) must be a ObjectRepositoryLogger", loggerType.FullName);

			ObjectRepositoryLogger logger = (ObjectRepositoryLogger)Logger;

			return logger
				.ObjectRepository
				.Query<LogEvent>(le => le.Time >= from && le.Time <= to)
				.CopyAs<LogEntry>()
				.ToList();
		}

		#endregion
	}
}
