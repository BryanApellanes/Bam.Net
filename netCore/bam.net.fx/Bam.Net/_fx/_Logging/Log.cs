/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using Bam.Net.Configuration;

namespace Bam.Net.Logging
{
    public static partial class Log
    {
        public static void AddEntry(string messageSignature, EventLogEntryType type)
        {
            AddEntry(messageSignature, type, new string[] { });
        }
        
        /// <summary>
        /// Convenience method for passing in System.Diagnostics.EventLogEntryType instead of LogEventType.
        /// The specified EventLogEntryType will be converted to an equivalent LogEventType if necessary.
        /// </summary>
        /// <param name="messageSignature"></param>
        /// <param name="type"></param> 
        /// <param name="variableValues"></param>
        public static void AddEntry(string messageSignature, EventLogEntryType type, params string[] variableValues)
        {
            LogEventType logEventType = LogEventType.Information;
            switch (type)
            {
                case EventLogEntryType.Error:
                    logEventType = LogEventType.Error;
                    break;
                case EventLogEntryType.FailureAudit:
                    logEventType = (LogEventType)((int)type);
                    break;
                case EventLogEntryType.Information:
                    logEventType = LogEventType.Information;
                    break;
                case EventLogEntryType.SuccessAudit:
                    logEventType = (LogEventType)((int)type);
                    break;
                case EventLogEntryType.Warning:
                    logEventType = LogEventType.Warning;
                    break;
                default:
                    break;
            }

            AddEntry(messageSignature, logEventType, variableValues);
        }
    }
}
