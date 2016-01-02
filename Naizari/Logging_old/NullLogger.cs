/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Logging
{
    public class NullLogger: ILogger
    {
        #region ILogger Members

        public void Initialize() { }

        public bool IsNull { get { return true; } }

        public string ApplicationName
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public string LogLocation
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public string LogName
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public string NotifyFatal
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public string NotifyError
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public string NotifyInfo
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public string NotifyWarn
        {
            get
            {
                return string.Empty;
            }
            set
            {
                
            }
        }

        public event LogEntryAddedListener EntryAdded;

        public event LogEntryAddedListener FatalEventOccurred;

        public event LogEntryAddedListener ErrorEventOccurred;

        public event LogEntryAddedListener InfoEventOccurred;

        public event LogEntryAddedListener WarnEventOccurred;

        public void AddEntry(string messageSignature)
        {
            
        }

        public void AddEntry(string messageSignature, LogEventType type)
        {
            
        }

        public void AddEntry(string messageSignature, Exception ex)
        {
            
        }

        public void AddEntry(string messageSignature, LogEventType type, Exception ex)
        {
            
        }

        public void AddEntry(string messageSignature, params string[] variableMessageValues)
        {
            
        }

        public void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues)
        {
            
        }

        public void AddEntry(string messagesignature, LogEventType type, Exception ex, params string[] variableMessageValues)
        {
            
        }

        public void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues)
        {
        
        }

        public void CommitLogEvent(LogEvent logEvent)
        {
        
        }



        public LogEvent CreateInfoEvent(string message)
        {
            return new LogEvent();
        }

        public LogEvent CreateInfoEvent(string messageSignature, params string[] messageVariableValues)
        {
            return new LogEvent();
        }

        public LogEvent CreateErrorEvent(string messageSignature, Exception ex, params string[] messageVariableValues)
        {
            return new LogEvent();
        }

        public LogEvent CreateErrorEvent(string message)
        {
            return new LogEvent();
        }

        public LogEvent CreateWarningEvent(string message)
        {
            return new LogEvent();
        }

        public LogEvent CreateWarningEvent(string messageSignature, params string[] messageVariableValues)
        {
            return new LogEvent();
        }

        #endregion
    }
}
