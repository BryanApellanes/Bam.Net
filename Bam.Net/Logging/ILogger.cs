/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Logging
{
    public interface ILogger
    {
        bool IsNull { get; }

        string ApplicationName { get; set; }
        VerbosityLevel Verbosity { get; set; }

        event LogEntryAddedListener EntryAdded;
        event LogEntryAddedListener FatalEventOccurred;
        event LogEntryAddedListener ErrorEventOccurred;
        event LogEntryAddedListener InfoEventOccurred;
        event LogEntryAddedListener WarnEventOccurred;

        void AddEntry(string messageSignature);
        void AddEntry(string messageSignature, int verbosity);
        void AddEntry(string messageSignature, LogEventType type);
        void AddEntry(string messageSignature, Exception ex);
        void AddEntry(string messageSignature, int verbosity, Exception ex);
        void AddEntry(string messageSignature, LogEventType type, Exception ex);
        void AddEntry(string messageSignature, params string[] variableMessageValues);
        void AddEntry(string messageSignature, int verbosity, params string[] variableMessageValues);
        void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues);
        void AddEntry(string messagesignature, int verbosity, Exception ex, params string[] variableMessageValues);
        void AddEntry(string messagesignature, LogEventType type, Exception ex, params string[] variableMessageValues);
        void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues);

        void BlockUntilEventQueueIsEmpty(int sleep = 0);
        ILogger StartLoggingThread();
        ILogger StopLoggingThread();
        ILogger RestartLoggingThread();

        void CommitLogEvent(LogEvent logEvent);

        EventIdProvider EventIdProvider { get; set; }
    }
}
