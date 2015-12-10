/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Logging
{
    public interface ILogger
    {
        bool IsNull { get; }

        string ApplicationName { get; set; }
        string LogLocation { get; set; }
        string LogName { get; set; }
        string NotifyFatal { get; set; }
        string NotifyError { get; set; }
        string NotifyInfo { get; set; }
        string NotifyWarn { get; set; }

        event LogEntryAddedListener EntryAdded;
        event LogEntryAddedListener FatalEventOccurred;
        event LogEntryAddedListener ErrorEventOccurred;
        event LogEntryAddedListener InfoEventOccurred;
        event LogEntryAddedListener WarnEventOccurred;

        void Initialize();

        void AddEntry(string messageSignature);
        void AddEntry(string messageSignature, LogEventType type);
        void AddEntry(string messageSignature, Exception ex);
        void AddEntry(string messageSignature, LogEventType type, Exception ex);
        void AddEntry(string messageSignature, params string[] variableMessageValues);
        void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues);
        void AddEntry(string messagesignature, LogEventType type, Exception ex, params string[] variableMessageValues);
        void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues);

        LogEvent CreateInfoEvent(string message);
        LogEvent CreateInfoEvent(string messageSignature, params string[] messageVariableValues);
        
        LogEvent CreateErrorEvent(string messageSignature, Exception ex, params string[] messageVariableValues);
        LogEvent CreateErrorEvent(string message);

        LogEvent CreateWarningEvent(string message);
        LogEvent CreateWarningEvent(string messageSignature, params string[] messageVariableValues);
        

        void CommitLogEvent(LogEvent logEvent);
    }
}
