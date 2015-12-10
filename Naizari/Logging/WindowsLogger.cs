/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Naizari;
using Naizari.Configuration;

namespace Naizari.Logging
{
    /// <summary>
    /// An LoggerBase implementation that commits logs to the 
    /// Windows event viewer.
    /// </summary>
    public class WindowsLogger: Logger
    {
        public WindowsLogger()
            : base()
        {
            this.EventIdProvider = new WindowsEventIdProvider();
        }

        public static void CreateLog(string logSource, string logName)
        {
            GetLog(logSource, logName);
        }

        private static EventLog GetLog(string logSource, string logName)
        {
            if (!EventLog.Exists(logName))
            {
                EventLog.CreateEventSource(logSource, logName);
            }

            EventLog eventLog = new EventLog(logName);
            eventLog.Source = logSource;
            eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);

            return eventLog;
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            EventLogEntryType entryType = EventLogEntryType.Information;
            switch (logEvent.Severity)
            {
                case LogEventType.None:
                    entryType = EventLogEntryType.Information;
                    break;
                case LogEventType.Information:
                    entryType = EventLogEntryType.Information;
                    break;
                case LogEventType.Warning:
                    entryType = EventLogEntryType.Warning;
                    break;
                case LogEventType.Error:
                    entryType = EventLogEntryType.Error;
                    break;
                default:
                    break;
            }
                        
            EventLog eventLog = GetLog(ApplicationName, ApplicationName);
            eventLog.WriteEntry(logEvent.Message, entryType, 0);
        }        
    }
}
