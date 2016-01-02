/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using Naizari.Helpers;
using System.Text;
using System.Diagnostics;

namespace Naizari.Logging
{
    public class WindowsLogger: LoggerBase
    {
        public static event ExceptionThrownDelegate CreateLogException;
        public static event ExceptionThrownDelegate ModifyOverFlowPolicyException;

        public WindowsLogger() : base() { }

        EventLog eventLog;
        bool initialized = false;
        private void Initialize()
        {
            if (!initialized)
            {
                this.eventLog = CreateLog(this.LogName);
                this.initialized = true;
            }
        }

        public static EventLog CreateLog(string logName)
        {
            return CreateLog(logName, logName);
        }

        public static void OnCreateLogException(Exception ex)
        {
            if (CreateLogException != null)
            {
                CreateLogException(ex);
            }
        }

        public static void OnModifyOverFlowException(Exception ex)
        {
            if (ModifyOverFlowPolicyException != null)
            {
                ModifyOverFlowPolicyException(ex);
            }
        }

        public static EventLog CreateLog(string logSource, string logName)
        {
            try
            {
                if (!EventLog.Exists(logName))
                {
                    EventLog.CreateEventSource(logSource, logName);
                }

                EventLog eventLog = new EventLog(logName);
                eventLog.Source = logSource;
                try
                {
                    eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
                }
                catch (Exception ex)
                {
                    OnModifyOverFlowException(ex);
                }

                return eventLog;
            }
            catch (Exception ex)
            {
                OnCreateLogException(ex);
                return null;
            }
        }

        public static void ModifyOverflowPolicy(string logSource, string logName, OverflowAction action, int retentionDays)
        {
            try
            {
                EventLog eventLog = new EventLog(logName);
                eventLog.Source = logSource;
                eventLog.ModifyOverflowPolicy(action, retentionDays);
            }
            catch (Exception ex)
            {
                OnModifyOverFlowException(ex);
            }
        }

        public void Delete()
        {
            EventLog.Delete(this.LogName);
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            Initialize();

            EventLogEntryType elet = EventLogEntryType.Information;
            switch (logEvent.Severity)
            {
                case LogEventType.None:
                    elet = EventLogEntryType.Information;
                    break;
                case LogEventType.Information:
                    elet = EventLogEntryType.Information;
                    break;
                case LogEventType.Warning:
                    elet = EventLogEntryType.Warning;
                    break;
                case LogEventType.Error:
                    elet = EventLogEntryType.Error;
                    break;
                default:
                    break;
            }
            if (logEvent.EventID == -1)
                logEvent.EventID = 0;
            logEvent.Message = string.Format("{0}: {1}", UserUtil.GetCurrentUser(true), logEvent.Message);
            eventLog.WriteEntry(logEvent.Message, elet, logEvent.EventID);
        }
    }
}
