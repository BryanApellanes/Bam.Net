/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Naizari.Configuration;
using Naizari.Helpers;

namespace Naizari.Logging
{
    public abstract class Logger : ILogger, IHasRequiredProperties
    {
        static Queue<LogEvent> logEventQueue;
        static Thread loggingThread;
        static AutoResetEvent waitForEnqueueLogEvent;
        static AutoResetEvent waitForQueueToBeEmpty;

        List<string> requiredProperties;
        static Logger()
        {
            logEventQueue = new Queue<LogEvent>();
            waitForEnqueueLogEvent = new AutoResetEvent(false);
            waitForQueueToBeEmpty = new AutoResetEvent(false);
        }

        public Logger()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(OnDomainUnload);
            this.requiredProperties = new List<string>();
            this.requiredProperties.Add("LogType");
            this.requiredProperties.Add("ApplicationName");
            this.Verbosity = 4;
            this.EventIdProvider = new EventIdProvider();
        }

        protected virtual void OnDomainUnload(object sender, EventArgs e)
        {
            this.BlockUntilEventQueueIsEmpty();
            this.StopLoggingThread();
        }
        
        public string[] RequiredProperties
        {
            get { return requiredProperties.ToArray(); }
        }


        public virtual void RestartLoggingThread()
        {
            this.StopLoggingThread();
            this.StartLoggingThread();
        }

        public virtual void StopLoggingThread()
        {
            if (loggingThread != null)
            {
                try
                {
                    loggingThread.Abort();
                    loggingThread.Join(3000);
                }
                catch { } // not all ThreadStates are valid for a call to Abort
            }
        }

        public virtual void StartLoggingThread()
        {
            loggingThread = new Thread(new ThreadStart(LoggingThread));
            loggingThread.IsBackground = true;
            loggingThread.Start();
        }

        protected virtual void QueueLogEvent(LogEvent logEvent)
        {
            logEventQueue.Enqueue(logEvent);
            waitForEnqueueLogEvent.Set();
        }

        private void LoggingThread()
        {
            if (this.IsNull)
            {
                return;
            }

            while (true)
            {
                waitForEnqueueLogEvent.WaitOne();
                lock (logEventQueue)
                {
                    while (logEventQueue.Count > 0)
                    {
                        LogEvent logEvent = logEventQueue.Dequeue();
                        if (logEvent != null && (int)logEvent.Severity <= Verbosity)
                        {
                            CommitLogEvent(logEvent);
                        }
                    }
                }
                waitForQueueToBeEmpty.Set();
            }
        }

        /// <summary>
        /// Blocks the current thread until the event queue is empty.  Keep
        /// in mind that other calls to AddEntry by other threads will 
        /// increment the number of events in the queue.
        /// </summary>
        public void BlockUntilEventQueueIsEmpty()
        {
            RestartLoggingThread();
            if (logEventQueue.Count > 0)
            {
                waitForQueueToBeEmpty.WaitOne();
            }
        }

        internal Queue<LogEvent> LogEventQueue
        {
            get
            {
                return logEventQueue;
            }
        }

        #region ILogger Members

        public virtual bool IsNull { get { return false; } }

        string appName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(appName) || appName.Equals("UNKOWN"))
                {
                    appName = DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
                }
                return appName;
            }
            set
            {
                appName = value;
            }
        }

        /// <summary>
        /// A number indicating what level of verbosity to log.  The default is 4.
        /// </summary>
        public int Verbosity
        {
            get;
            set;
        }

        /// <summary>
        /// When overridden in a derived class will commit the specified logEvent
        /// to the underlying storage for the current LoggerBase implementation.
        /// </summary>
        /// <param name="logEvent"></param>
        public abstract void CommitLogEvent(LogEvent logEvent);

        public event LogEntryAddedListener EntryAdded;
        public event LogEntryAddedListener FatalEventOccurred;
        public event LogEntryAddedListener InfoEventOccurred;
        public event LogEntryAddedListener WarnEventOccurred;
        public event LogEntryAddedListener ErrorEventOccurred;
        public event LogEntryAddedListener CustomEventOccurred;

        public virtual EventIdProvider EventIdProvider { get; set; }

        public void AddEntry(string messageSignature)
        {
            AddEntry(messageSignature, new string[] { });
        }

        public void AddEntry(string messageSignature, int verbosity)
        {
            AddEntry(messageSignature, verbosity, new string[] { });
        }

        public void AddEntry(string messageSignature, Exception ex)
        {
            AddEntry(messageSignature, ex, new string[] { });
        }

        public void AddEntry(string messageSignature, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), variableMessageValues);
        }

        public void AddEntry(string messageSignature, int verbosity, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), verbosity, variableMessageValues);
        }

        public void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), ex, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, user, (int)LogEventType.Information, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, int verbosity, params string[] variableMessageValues)
        {
            if ((LogEventType)verbosity == LogEventType.Error)
            {
                AddEntry(messageSignature, user, new Exception("A custom error event has been logged"), variableMessageValues);
            }
            else
            {
                AddEntry(messageSignature, user, "Application", verbosity, variableMessageValues);
            }
        }

        private void AddEntry(string messageSignature, string user, Exception ex, params string[] variableMessageValues)
        {
            if (ex == null)
            {
                AddEntry(messageSignature, user, (int)LogEventType.Error, variableMessageValues);
            }
            else
            {
                AddEntry(messageSignature, user, "Application", ex, variableMessageValues);
            }
        }

        private void AddEntry(string messageSignature, string user, string category, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, user, category, (int)LogEventType.Information, variableMessageValues);
        }

        private void AddEntry(string messageSignature, string user, string category, int verbosity, params string[] variableMessageValues)
        {
            Exception ex = null;
            if ((LogEventType)verbosity == LogEventType.Error)
            {
                ex = new Exception("A custom error event has been logged");
            }
            LogEvent ev = CreateLogEvent(messageSignature, user, category, verbosity, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);

        }


        private void OnEntryAdded(LogEvent logEvent)
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
                    if (InfoEventOccurred != null)
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
            LogEvent ev = CreateLogEvent(messageSignature, user, category, (int)LogEventType.Error, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);

        }

        public void AddEntry(string messageSignature, int verbosity, Exception ex)
        {
            AddEntry(messageSignature, verbosity, ex, new string[] { });
        }

        public void AddEntry(string messageSignature, int verbosity, Exception ex, params string[] variableMessageValues)
        {
            LogEvent ev = CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", verbosity, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);
        }

        protected LogEvent CreateInfoEvent(string message)
        {
            return CreateInfoEvent(message, new string[] { });
        }
        protected LogEvent CreateInfoEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", (int)LogEventType.Information, null, messageVariableValues);
        }

        protected LogEvent CreateWarningEvent(string message)
        {
            return CreateWarningEvent(message, new string[] { });
        }
        protected LogEvent CreateWarningEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", (int)LogEventType.Warning, null, messageVariableValues);
        }

        protected LogEvent CreateErrorEvent(string message)
        {
            return CreateErrorEvent(message, new string[] { });
        }
        protected LogEvent CreateErrorEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", (int)LogEventType.Error, null, messageVariableValues);
        }

        protected LogEvent CreateErrorEvent(string messageSignature, Exception ex, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", (int)LogEventType.Error, ex, messageVariableValues);
        }
        
        private LogEvent CreateLogEvent(string messageSignature, string user, string category, int type, Exception ex, params string[] messageVariableValues)
        {
            LogEvent ev = new LogEvent();
            ev.MessageSignature = messageSignature;
            ev.MessageVariableValues = messageVariableValues;
            ev.EventID = GetEventId(this.ApplicationName, messageSignature);
            ev.Time = DateTime.UtcNow;
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
            StringBuilder message = new StringBuilder();
            message.AppendFormat("Thread=#{0}({1}):App={2}:PID={3}:Utc={4}:{5}",
                Thread.CurrentThread.GetHashCode(),
                Thread.CurrentThread.ManagedThreadId,
                ApplicationName,
                System.Diagnostics.Process.GetCurrentProcess().Id,
                utcNow.ToShortDateString() + utcNow.ToShortTimeString(),
                ev.Message);

            if (ex != null)
            {
                message.AppendFormat("\r\n{0}\r\n", ex.Message);
                if (ex.StackTrace != null)
                {
                    message.AppendFormat("\r\n{0}\r\n", ex.StackTrace);
                }
                else
                {
                    message.AppendFormat("\r\n{0}\r\n", new System.Diagnostics.StackTrace(true).ToString());
                }

                if (ex.InnerException != null)
                {
                    message.AppendFormat("\r\n{0}\r\n", ex.InnerException.Message);
                    if (ex.InnerException.StackTrace != null)
                    {
                        message.AppendFormat("\r\n{0}\r\n", ex.InnerException.StackTrace);
                    }
                    else
                    {
                        message.AppendFormat("\r\n{0}\r\n", new System.Diagnostics.StackTrace(true).ToString());
                    }
                }
            }
            else
            {
                message.AppendFormat("\r\n{0}\r\n", new System.Diagnostics.StackTrace(true).ToString());
            }

            ev.Message = message.ToString();

            ev.Source = this.ApplicationName;
            ev.User = user;

            ev.Severity = (LogEventType)type;

            return ev;
        }

        /// <summary>
        /// Returns an id for the specified applicationName and messageSignature.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="messageSignature"></param>
        /// <returns></returns>
        protected virtual int GetEventId(string applicationName, string messageSignature)
        {
            return EventIdProvider.GetEventId(applicationName, messageSignature);
        }
        

        public void AddEntry(string messageSignature, LogEventType verbosity)
        {
            AddEntry(messageSignature, (int)verbosity);
        }

        public void AddEntry(string messageSignature, LogEventType verbosity, Exception ex)
        {
            AddEntry(messageSignature, (int)verbosity, ex);
        }

        public void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, (int)type, variableMessageValues);
        }

        public void AddEntry(string messageSignature, LogEventType type, Exception ex, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, (int)type, ex, variableMessageValues);
        }

        #endregion
    }
}
