/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Logging.Data
{
	// schema = DaoLogger 
    public static class DaoLoggerContext
    {
		public static string ConnectionName
		{
			get
			{
				return "DaoLogger";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class LogEventQueryContext
	{
			public LogEventCollection Where(WhereDelegate<LogEventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.Where(where, db);
			}
		   
			public LogEventCollection Where(WhereDelegate<LogEventColumns> where, OrderBy<LogEventColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.Where(where, orderBy, db);
			}

			public LogEvent OneWhere(WhereDelegate<LogEventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.OneWhere(where, db);
			}

			public static LogEvent GetOneWhere(WhereDelegate<LogEventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.GetOneWhere(where, db);
			}
		
			public LogEvent FirstOneWhere(WhereDelegate<LogEventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.FirstOneWhere(where, db);
			}

			public LogEventCollection Top(int count, WhereDelegate<LogEventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.Top(count, where, db);
			}

			public LogEventCollection Top(int count, WhereDelegate<LogEventColumns> where, OrderBy<LogEventColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LogEventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.LogEvent.Count(where, db);
			}
	}

	static LogEventQueryContext _logEvents;
	static object _logEventsLock = new object();
	public static LogEventQueryContext LogEvents
	{
		get
		{
			return _logEventsLock.DoubleCheckLock<LogEventQueryContext>(ref _logEvents, () => new LogEventQueryContext());
		}
	}    }
}																								
