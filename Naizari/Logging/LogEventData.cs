/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data.Common;
using Naizari.Data;

namespace Naizari.Logging
{
	public class LogEventData: LogEventDataBase
	{
		public LogEventData(): base(){ }

        public static LogEventData FromLogEvent(LogEvent logEvent)
        {
            return FromLogEvent(logEvent, DaoContext.Get(LogEventData.ContextName).DatabaseAgent);
        }

        public static LogEventData FromLogEvent(LogEvent logEvent, DatabaseAgent agent)
        {
            LogEventData retVal = LogEventData.New(agent);
            retVal.Category = logEvent.Category;
            retVal.Computer = logEvent.Computer;
            retVal.EventID = logEvent.EventID;
            retVal.Message = logEvent.Message;
            retVal.Severity = logEvent.Severity.ToString();
            retVal.TimeOccurred = logEvent.Time;
            retVal.Source = logEvent.Source;
            retVal.User = logEvent.User;
            return retVal;
        }
	}
}

