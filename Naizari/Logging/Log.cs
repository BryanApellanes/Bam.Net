/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using Naizari.Configuration;

namespace Naizari.Logging
{
    public static class Log
    {
        public static ILogger Default
        {
            get
            {
                return GetDefaultLogger();
            }
        }

        /// <summary>
        /// Reset Log.Current to null.  Used primarily for testing.
        /// </summary>
        public static void Reset()
        {
            lock (_currentLoggerLock)
            {
                _currentLogger = null;
            }
        }

        static ILogger _currentLogger;
        static object _currentLoggerLock = new object();
        private static ILogger GetDefaultLogger()
        {
            if (_currentLogger == null)
            {
                // create a logger of the type specified by the config
                // if no value is in the config create a null logger
                _currentLogger = CreateLogger(DefaultConfiguration.GetAppSetting("LogType", "Null"));
            }

            return _currentLogger;
        }

        const string _loggingNamespace = "Naizari.Logging";
        internal static ILogger CreateLogger(string logType)
        {
            lock (_currentLoggerLock)
            {
                string loggerTypeName = string.Format("{0}Logger", logType);
                Type loggerType = Type.GetType(loggerTypeName);
                if (loggerType == null)
                {
                    loggerType = Type.GetType(string.Format("{0}.{1}Logger", _loggingNamespace, logType));
                    if (loggerType == null)
                    {
                        loggerType = Type.GetType(string.Format("{0}.{1}", _loggingNamespace, logType));
                    }

                    if (loggerType == null)
                    {
                        throw new InvalidOperationException(string.Format("The specified logType ({0}) could not be found.", logType));
                    }
                }

                return CreateLogger(loggerType);
            }
        }

        internal static ILogger CreateLogger(Type loggerType)
        {
            ConstructorInfo ctor = loggerType.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new InvalidOperationException(string.Format("The specified logType ({0}) doesn't have a parameterless constructor.", loggerType.FullName));
            }

            return (ILogger)ctor.Invoke(null);
        }

        public static void AddLogger(Type loggerType)
        {
            AddLogger(CreateLogger(loggerType));
        }

        public static void AddLogger(ILogger loggerInstance)
        {
            MultiTargetLogger main = null;

            if (_currentLogger == null)
            {
                _currentLogger = CreateLogger(typeof(MultiTargetLogger));
                main = (MultiTargetLogger)_currentLogger;
            }
            else
            {
                main = _currentLogger as MultiTargetLogger;
                if (main == null)
                {
                    main = (MultiTargetLogger)CreateLogger(typeof(MultiTargetLogger));
                    if (_currentLogger != null)
                    {
                        // add _currentLogger to the new MultiTargetLogger
                        // so it can continue to log
                        main.AddLogger(_currentLogger);
                    }

                    // whatever the _currentLogger was it wasn't a MultiTargetLogger
                    // set it here
                    lock (_currentLoggerLock)
                    {
                        Logger current = _currentLogger as Logger;
                        if (current != null)
                        {
                            current.StopLoggingThread();
                        }
                        _currentLogger = main;
                    }
                }
            }

            main.AddLogger(loggerInstance);
        }

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

        #region ILogger convenience methods.  Vanilla wrappers to the AddEntry and BlockUntilEventQueueIsEmpty methods of the Default ILogger
        public static void AddEntry(string messageSignature) { Default.AddEntry(messageSignature); }
        public static void AddEntry(string messageSignature, int verbosity) { Default.AddEntry(messageSignature, verbosity); }
        public static void AddEntry(string messageSignature, LogEventType type) { Default.AddEntry(messageSignature, type); }
        public static void AddEntry(string messageSignature, Exception ex) { Default.AddEntry(messageSignature, ex); }
        public static void AddEntry(string messageSignature, int verbosity, Exception ex) { Default.AddEntry(messageSignature, verbosity, ex); }
        public static void AddEntry(string messageSignature, LogEventType type, Exception ex) { Default.AddEntry(messageSignature, type, ex); }
        public static void AddEntry(string messageSignature, params string[] variableMessageValues) { Default.AddEntry(messageSignature, variableMessageValues); }
        public static void AddEntry(string messageSignature, int verbosity, params string[] variableMessageValues) { Default.AddEntry(messageSignature, verbosity, variableMessageValues); }
        public static void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues) { Default.AddEntry(messageSignature, type, variableMessageValues); }
        public static void AddEntry(string messagesignature, int verbosity, Exception ex, params string[] variableMessageValues) { Default.AddEntry(messagesignature, verbosity, ex, variableMessageValues); }
        public static void AddEntry(string messagesignature, LogEventType type, Exception ex, params string[] variableMessageValues) { Default.AddEntry(messagesignature, type, ex, variableMessageValues); }
        public static void AddEntry(string messageSignature, Exception ex, params string[] variableMessageValues) { Default.AddEntry(messageSignature, ex, variableMessageValues); }

        /// <summary>
        /// Blocks the current thread until the event queue is empty.  Keep
        /// in mind that other calls to AddEntry by other threads will 
        /// increment the number of events in the queue.  If the commit 
        /// thread is running it will be restarted.
        /// </summary>
        public static void BlockUntilEventQueueIsEmpty() { Default.BlockUntilEventQueueIsEmpty(); }
        #endregion

        /// <summary>
        /// Starts the background logging commit thread.
        /// </summary>
        public static void Start()
        {
            Default.RestartLoggingThread();
        }

        /// <summary>
        /// Stops the background logging commit thread.
        /// </summary>
        public static void Stop()
        {
            Default.StopLoggingThread();
        }

        /// <summary>
        /// Flushes the current LogEvent queue without committing them.
        /// May throw an InvalidCastException if the ILogger implementation
        /// referenced by Log.Default doesn't extend Logger.
        /// </summary>
        public static void AbandonQueue()
        {
            ((Logger)Default).LogEventQueue.Clear();
        }
    }
}
