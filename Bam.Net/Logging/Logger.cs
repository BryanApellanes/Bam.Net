/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Bam.Net.Configuration;
using System.Collections.Concurrent;
//using Bam.Net.Helpers;

namespace Bam.Net.Logging
{
    public abstract class Logger : ILogger, IHasRequiredProperties
    {
        ConcurrentQueue<LogEvent> _logEventQueue;
        Thread _loggingThread;
        AutoResetEvent _waitForEnqueueLogEvent;
        AutoResetEvent _waitForQueueToBeEmpty;

        List<string> requiredProperties;
        public Logger()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(OnDomainUnload);
            _logEventQueue = new ConcurrentQueue<LogEvent>();
            _waitForEnqueueLogEvent = new AutoResetEvent(false);

            requiredProperties = new List<string>();
            requiredProperties.Add("LogType");
            requiredProperties.Add("ApplicationName");
            Verbosity = VerbosityLevel.Information;
            EventIdProvider = new HashingEventIdProvider();
        }

        protected virtual void OnDomainUnload(object sender, EventArgs e)
        {
            BlockUntilEventQueueIsEmpty();
            StopLoggingThread();
        }

        public string[] RequiredProperties
        {
            get { return requiredProperties.ToArray(); }
        }

        public virtual ILogger RestartLoggingThread()
        {
            StopLoggingThread();
            StartLoggingThread();
            return this;
        }

        object _threadLock = new object();
        bool _keepLogging = true;
        public virtual ILogger StopLoggingThread()
        {
            if (_loggingThread != null)
            {
                lock (_threadLock)
                {
                    _keepLogging = false;
                    _waitForEnqueueLogEvent.Set();
                    int wait = 3000;
                    int waited = 0;
                    if (Exec.TakesTooLong(() =>
                    {
                        while (_loggingThread.ThreadState != System.Threading.ThreadState.AbortRequested &&
                           _loggingThread.ThreadState != System.Threading.ThreadState.Aborted &&
                           _loggingThread.ThreadState != System.Threading.ThreadState.Stopped)
                        {
                            Thread.Sleep(1);
                            waited++;
                            if (waited == wait)
                            {
                                break;
                            }
                        }
                    }, 3500))
                    {
                        try
                        {
                            _loggingThread.Abort();
                        }
                        catch { } // not all ThreadStates are valid for a call to Abort
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// The number of milliseconds to wait after a LogEvent
        /// is queued to be committed before beginning the
        /// commit loop
        /// </summary>
        public int CommitCycleDelay { get; set; }
        /// <summary>
        /// Start the background logger commit thread.
        /// </summary>
        public virtual ILogger StartLoggingThread()
        {
            lock (_threadLock)
            {
                _loggingThread = new Thread(LoggingThread) {IsBackground = true};
                _keepLogging = true;
                _loggingThread.Start();
            }
            return this;
        }

        protected virtual void QueueLogEvent(LogEvent logEvent)
        {
            _logEventQueue.Enqueue(logEvent);
            _waitForEnqueueLogEvent.Set();
        }

        private void LoggingThread()
        {
            if (IsNull)
            {
                return;
            }

            while (_keepLogging)
            {
                _waitForEnqueueLogEvent.WaitOne();
                Thread.Sleep(CommitCycleDelay);
                while (_logEventQueue.Count > 0)
                {
                    LogEvent logEvent;
                    if (_logEventQueue.TryDequeue(out logEvent))
                    {
                        if (logEvent != null && (int)logEvent.Severity <= (int)Verbosity)
                        {
                            CommitLogEvent(logEvent);
                        }
                    }                    
                }
            }
        }

        /// <summary>
        /// Blocks the current thread until the event queue is empty.
        /// </summary>
        public virtual void BlockUntilEventQueueIsEmpty(int sleep = 0)
        {
            if(_loggingThread != null && _loggingThread.ThreadState == System.Threading.ThreadState.Running)
            {
                _keepLogging = false;
                while (_logEventQueue.Count > 0)
                {
                    _waitForEnqueueLogEvent.Set();
                    Thread.Sleep(3);                    
                }                
            }
            Thread.Sleep(sleep);
        }

        internal ConcurrentQueue<LogEvent> LogEventQueue
        {
            get
            {
                return _logEventQueue;
            }
        }

        #region ILogger Members

        public virtual bool IsNull { get { return false; } }

        string appName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(appName) || appName.Equals(DefaultConfiguration.DefaultApplicationName))
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
        public VerbosityLevel Verbosity
        {
            get;
            set;
        }

        /// <summary>
        /// When overridden in a derived class will commit the specified logEvent
        /// to the underlying storage for the current Logger implementation.
        /// </summary>
        /// <param name="logEvent"></param>
        public abstract void CommitLogEvent(LogEvent logEvent);

        public event LogEntryAddedListener EntryAdded;
        public event LogEntryAddedListener FatalEventOccurred;
        public event LogEntryAddedListener InfoEventOccurred;
        public event LogEntryAddedListener WarnEventOccurred;
        public event LogEntryAddedListener ErrorEventOccurred;
        public event LogEntryAddedListener CustomEventOccurred;

        public virtual IEventIdProvider EventIdProvider { get; set; }

        public void AddEntry(string messageSignature)
        {
            AddEntry(messageSignature, new string[] { });
        }

        public virtual void AddEntry(string messageSignature, int verbosity)
        {
            AddEntry(messageSignature, verbosity, new string[] { });
        }

        public virtual void AddEntry(string messageSignature, Exception ex)
        {
            AddEntry(messageSignature, ex, new string[] { });
        }

        public virtual void AddEntry(string messageSignature, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), variableMessageValues);
        }

        public virtual void AddEntry(string messageSignature, int verbosity, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, UserUtil.GetCurrentUser(true), verbosity, variableMessageValues);
        }

        public virtual void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues)
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
            LogEvent ev = CreateLogEvent(messageSignature, user, category, (LogEventType)verbosity, ex, variableMessageValues);

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
            LogEvent ev = CreateLogEvent(messageSignature, user, category, LogEventType.Error, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);

        }

        public void AddEntry(string messageSignature, int verbosity, Exception ex)
        {
            AddEntry(messageSignature, verbosity, ex, new string[] { });
        }

        public void AddEntry(string messageSignature, int verbosity, Exception ex, params string[] variableMessageValues)
        {
            LogEvent ev = CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", (LogEventType)verbosity, ex, variableMessageValues);

            QueueLogEvent(ev);

            OnEntryAdded(ev);
        }

        protected LogEvent CreateInfoEvent(string message)
        {
            return CreateInfoEvent(message, new string[] { });
        }
        protected LogEvent CreateInfoEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Information, null, messageVariableValues);
        }

        protected LogEvent CreateWarningEvent(string message)
        {
            return CreateWarningEvent(message, new string[] { });
        }
        protected LogEvent CreateWarningEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Warning, null, messageVariableValues);
        }

        protected LogEvent CreateErrorEvent(string message)
        {
            return CreateErrorEvent(message, new string[] { });
        }
        protected LogEvent CreateErrorEvent(string messageSignature, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Error, null, messageVariableValues);
        }

        protected LogEvent CreateErrorEvent(string messageSignature, Exception ex, params string[] messageVariableValues)
        {
            return CreateLogEvent(messageSignature, UserUtil.GetCurrentUser(true), "Application", LogEventType.Error, ex, messageVariableValues);
        }

        protected internal LogEvent CreateLogEvent(string messageSignature, string user, string category, LogEventType type, Exception ex, params string[] messageVariableValues)
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

            StringBuilder message = HandleDetails(ev);

            StringBuilder stack = new StringBuilder();
            HandleStackTrace(ex, message, stack);

            ev.Message = message.ToString();
            ev.StackTrace = stack.ToString();

            ev.Source = this.ApplicationName;
            ev.User = user;

            ev.Severity = type;

            return ev;
        }

        protected virtual StringBuilder HandleDetails(LogEvent ev)
        {
            ApplicationDiagnosticInfo details = new ApplicationDiagnosticInfo(ev);
            return new StringBuilder(details.ToString());
        }

        protected virtual void HandleStackTrace(Exception ex, StringBuilder message, StringBuilder stack)
        {
			Args.SetMessageAndStackTrace(ex, message, stack);
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

        public virtual void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, (int)type, variableMessageValues);
        }

        public virtual void AddEntry(string messageSignature, LogEventType type, Exception ex, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, (int)type, ex, variableMessageValues);
        }

        public virtual void AddEntry(string messageSignature, VerbosityLevel verbosity)
        {
            AddEntry(messageSignature, (int)verbosity);
        }

        public virtual void AddEntry(string messageSignature, VerbosityLevel verbosity, Exception ex)
        {
            AddEntry(messageSignature, (int)verbosity, ex);
        }

        public virtual void AddEntry(string messageSignature, VerbosityLevel type, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, (int)type, variableMessageValues);
        }

        public virtual void AddEntry(string messageSignature, VerbosityLevel type, Exception ex, params string[] variableMessageValues)
        {
            AddEntry(messageSignature, (int)type, ex, variableMessageValues);
        }

        public void Info(string messageSignature, params object[] args)
        {
            Args.ThrowIfNull(args);            
            AddEntry(messageSignature, LogEventType.Information, args.Each(a => a.ToString()).ToArray());
        }

        public void Warning(string messageSignature, params object[] args)
        {
            Args.ThrowIfNull(args);
            AddEntry(messageSignature, LogEventType.Warning, args.Each(a => a.ToString()).ToArray());
        }

        public void Error(string messageSignature, params object[] args)
        {
            Args.ThrowIfNull(args);
            AddEntry(messageSignature, LogEventType.Error, args.Each(a => a.ToString()).ToArray());
        }

        #endregion
    }
}
