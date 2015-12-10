/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Threading;
using Naizari.Configuration;
using Naizari.Helpers;
using Naizari.Logging.EventRegistration;

namespace Naizari.Logging
{
    public abstract class LoggerBase: ILogger, IHasRequiredProperties
    {
        protected static Queue<LogEvent> logEventQueue;
        protected static Thread loggingThread;
        protected static AutoResetEvent waitForQueue;

        List<string> requiredProperties;
        static LoggerBase()
        {
            logEventQueue = new Queue<LogEvent>();
            waitForQueue = new AutoResetEvent(false);
        }

        public LoggerBase()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);            
            this.requiredProperties = new List<string>();
            this.requiredProperties.Add("LogType");
            this.requiredProperties.Add("ApplicationName");
            this.RestartLoggingThread();
        }

        void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            this.StopLoggingThread();
        }

        public virtual void Initialize() { }

        public string[] RequiredProperties
        {
            get { return requiredProperties.ToArray(); }
        }


        public void RestartLoggingThread()
        {
            this.StopLoggingThread();
            this.StartLoggingThread();
        }

        public void StopLoggingThread()
        {
            if (loggingThread != null && loggingThread.ThreadState == ThreadState.Running)
            {
                loggingThread.Abort();
                loggingThread.Join(3000);
            }
        }

        public void StartLoggingThread()
        {
            loggingThread = new Thread(new ThreadStart(LoggingThread));
            loggingThread.IsBackground = true;
            loggingThread.Start();
        }

        protected void QueueLogEvent(LogEvent logEvent)
        {
            logEventQueue.Enqueue(logEvent);
            waitForQueue.Set();
        }

        private void LoggingThread()
        {
            while (true)
            {
                waitForQueue.WaitOne();
                lock (logEventQueue)
                {
                    while (logEventQueue.Count > 0)
                    {
                        LogEvent logEvent = logEventQueue.Dequeue();
                        if (logEvent != null)
                        {
                            CommitLogEvent(logEvent);
                        }
                    }
                }
            }
        }

        LogType providerType;
        public virtual string LogType
        {
            get
            {
                if (providerType == Logging.LogType.Null)
                {
                    return "";
                }

                return providerType.ToString();
            }
            set { providerType = (LogType)Enum.Parse(typeof(LogType), value); }
        }

        #region ILogger Members

        public bool IsNull { get { return false; } }

        string appName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(appName))
                {
                    return "UNKNOWN";
                }
                else
                {
                    return appName;
                }
            }
            set
            {
                appName = value;
            }
        }

        string logLocation;
        public string LogLocation
        {
            get
            {
                if (string.IsNullOrEmpty(this.logLocation))
                    return FsUtil.GetCurrentUserAppDataFolder() + "Logs";

                return this.logLocation;
            }
            set
            {
                this.logLocation = value;
            }
        }
        string logName;
        public string LogName
        {
            get
            {
                if (string.IsNullOrEmpty(this.logName))
                    return Log.Current.ApplicationName;

                return this.logName;
            }
            set
            {
                this.logName = value;
            }
        }

        public abstract void CommitLogEvent(LogEvent logEvent);

        public event LogEntryAddedListener EntryAdded;
        public event LogEntryAddedListener FatalEventOccurred;
        public event LogEntryAddedListener InfoEventOccurred;
        public event LogEntryAddedListener WarnEventOccurred;
        public event LogEntryAddedListener ErrorEventOccurred;

        public string NotifyFatal { get; set; }
        public string NotifyError { get; set; }
        public string NotifyWarn { get; set; }
        public string NotifyInfo { get; set; }

        public void AddEntry(string messageSignature)
        {
            AddEntry(messageSignature, new string[] { });
        }

        public void AddEntry(string messageSignature, LogEventType type)
        {
            AddEntry(messageSignature, type, new string[] { });
        }

        public void AddEntry(string messageSignature, Exception ex)
        {
            AddEntry(messageSignature, ex, new string[] { });
        }

        public void AddEntry(string messageSignature, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), variableMessageValues);
        }

        public void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), type, variableMessageValues);
        }

        public void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), ex, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, user, LogEventType.Information, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, LogEventType type, params string[] variableMessageValues)
        {

            if (type == LogEventType.Error)
            {
                AddEntry(messageSignature, user, new Exception("A custom error event has been logged"), variableMessageValues);
            }
            else
            {
                AddEntry(messageSignature, user, "Application", type, variableMessageValues);
            }
        }

        private void AddEntry(string messageSignature, string user, Exception ex, params string[] variableMessageValues)
        {
            if (ex == null)
                AddEntry(messageSignature, user, LogEventType.Error, variableMessageValues);
            else
                AddEntry(messageSignature, user, "Application", ex, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, string category, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, user, category, LogEventType.Information, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, string category, LogEventType type, params string[] variableMessageValues)
        {
            Exception ex = null;
            if( type == LogEventType.Error )
                ex = new Exception("A custom error event has been logged");
            LogEvent ev = CreateLogEvent(messageSignature, user, category, type, ex, variableMessageValues);

            QueueLogEvent(ev);
            
            OnEntryAdded(ev);
        }


        protected void OnEntryAdded(LogEvent logEvent)
        {
            if (EntryAdded != null)
            {
                EntryAdded(this.ApplicationName, logEvent);
            }

            switch (logEvent.Severity)
            {
                case LogEventType.None:
                    break;
                case LogEventType.Information:
                    if (InfoEventOccurred!= null)
                        InfoEventOccurred(this.ApplicationName, logEvent);
                    break;
                case LogEventType.Warning:
                    if (WarnEventOccurred != null)
                        WarnEventOccurred(this.ApplicationName, logEvent);
                    break;
                case LogEventType.Error:
                    if (ErrorEventOccurred != null)
                        ErrorEventOccurred(this.ApplicationName, logEvent);
                    break;
                case LogEventType.Fatal:
                    if (FatalEventOccurred != null)
                        FatalEventOccurred(this.ApplicationName, logEvent);
                    break;
                default:
                    break;
            }
        }

        private void AddEntry(string messageSignature, string user, string category, Exception ex, params string[] variableMessageValues)
        {
            LogEvent ev = CreateLogEvent(messageSignature, user, category, LogEventType.Error, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);

        }

        public void AddEntry(string messageSignature, LogEventType type, Exception ex)
        {
            AddEntry(messageSignature, type, ex, new string[] { });
        }

        public void AddEntry(string messageSignature, LogEventType type, Exception ex, params string[] variableMessageValues)
        {
            LogEvent ev = CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", type, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);
        }

        public LogEvent CreateInfoEvent(string message)
        {
            return CreateInfoEvent(message, new string[] { });
        }
        public LogEvent CreateInfoEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Information, null, messageVariableValues);
        }

        public LogEvent CreateWarningEvent(string message)
        {
            return CreateWarningEvent(message, new string[] { });
        }
        public LogEvent CreateWarningEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Warning, null, messageVariableValues); 
        }

        public LogEvent CreateErrorEvent(string message)
        {
            return CreateErrorEvent(message, new string[] { });
        }
        public LogEvent CreateErrorEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Error, null, messageVariableValues);
        }

        public LogEvent CreateErrorEvent(string messageSignature, Exception ex, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Error, ex, messageVariableValues);
        }

        private LogEvent CreateLogEvent(string messageSignature, string user, string category, LogEventType type, Exception ex, params string[] messageVariableValues)
        {
            LogEvent ev = new LogEvent();
            ev.MessageSignature = messageSignature;
            ev.MessageVariableValues = messageVariableValues;
            ev.EventID = EventManager.Current.GetEventId(this.ApplicationName, messageSignature);
            ev.TimeOccurred = DateTime.UtcNow;
            ev.Category = category;
            ev.Computer = Environment.MachineName;
            if (messageVariableValues.Length > 0)
            {
                try
                {
                    ev.Message = string.Format(messageSignature, messageVariableValues);
                }
                catch //(Exception ex)
                {
                    ev.Message = messageSignature;
                }
            }
            else
            {
                ev.Message = messageSignature;
            }
            DateTime utcNow = DateTime.UtcNow;
            ev.Message = "Thread=#" + Thread.CurrentThread.GetHashCode() + "(" + Thread.CurrentThread.ManagedThreadId +
                "):App=" + this.ApplicationName +
                ":PID=" + System.Diagnostics.Process.GetCurrentProcess().Id + ":" +
                "Utc=" + utcNow.ToShortDateString() + " " + utcNow.ToShortTimeString() + ":" +
                ev.Message;
            if (ex != null)
            {
                ev.Message += "\r\n" + ex.Message + "\r\n";
                ev.Message += ex.StackTrace;

                if (ex.InnerException != null)
                {
                    ev.Message += "\r\n" + ex.InnerException.Message;
                    ev.Message += ex.InnerException.StackTrace;
                }
            }

            ev.Source = this.ApplicationName;
            ev.User = user;

            if (type == LogEventType.None)
                type = LogEventType.Information;            
            
            ev.Severity = type;

            return ev;
        }
        #endregion
    }
}
