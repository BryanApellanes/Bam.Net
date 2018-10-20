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
    public static class Log
    {
		static ILogger _defaultLogger;
		static object _defaultLoggerLock = new object();
        /// <summary>
        /// Gets or sets the default logger.  Default is determined by the configuration 
        /// file.
        /// </summary>
        public static ILogger Default
        {
            get
            {
				return _defaultLoggerLock.DoubleCheckLock(ref _defaultLogger, () => GetDefaultLogger());
            }
			set
			{
				_defaultLogger = value;				
			}
        }

        public static void WarnIf(bool condition, string messageSignature, params object[] args)
        {
            if (condition)
            {
                Warn(messageSignature, args);
            }
        }

        public static void Info(string messageSignature, params object[] args)
        {
            Default.AddEntry(messageSignature, LogEventType.Information, args?.Select(a => a.ToString())?.ToArray());
        }

        public static void Warn(string messageSignature, params object[] args)
        {
            Default.AddEntry(messageSignature, LogEventType.Warning, args?.Select(a => a.ToString())?.ToArray());
        }

        public static void Error(string messageSignature, params object[] args)
        {
            Error(messageSignature, new Exception(string.Format(messageSignature, args)), args);
        }

        public static void Error(string messageSignature, Exception ex, params object[] args)
        {
            Default.AddEntry(messageSignature, ex, args?.Select(a => a.ToString())?.ToArray());
        }

        public static void Debug(string messageSignature, params object[] args)
        {
            DebugInfo(messageSignature, args);
        }

        public static void DebugInfo(string messageSignature, params object[] args)
        {
            WriteDebug(messageSignature, args);
            Info(messageSignature, args);
        }
        
        public static void DebugWarn(string messageSignature, params object[] args)
        {
            WriteDebug(messageSignature, args);
            Warn(messageSignature, args);
        }

        public static void DebugError(string messageSignature, Exception ex, params object[] args)
        {
            WriteDebug(messageSignature, args);
            Error(messageSignature, ex, args);
        }
        
        private static void WriteDebug(string messageSignature, object[] args)
        {
            if(ProcessMode.Current.Mode == ProcessModes.Dev)
            {
                string message = string.Format(messageSignature, args);
                Console.WriteLine($"DEBUG: {message}");
                System.Diagnostics.Debug.WriteLine(message);
            }
        }

        public static void Trace(string messageSignature, params object[] args)
        {
            TraceInfo(messageSignature, args);
        }

        public static void Trace(string messageSignature, Exception ex, params object[] args)
        {
            TraceError(messageSignature, ex, args);
        }

        public static void TraceInfo(string messageSignature, params object[] args)
        {
            WriteTrace(messageSignature, args);
            Info(messageSignature, args);
        }

        public static void TraceWarn(string messageSignature, params object[] args)
        {
            WriteTrace(messageSignature, args);
            Warn(messageSignature, args);
        }

        private static void WriteTrace(string messageSignature, object[] args)
        {
            if (ProcessMode.Current.Mode == ProcessModes.Dev || ProcessMode.Current.Mode == ProcessModes.Test)
            {
                string message = string.Format(messageSignature, args);
                Console.WriteLine($"TRACE: {message}");
                System.Diagnostics.Trace.WriteLine(message);
            }
        }
        
        private static void TraceError(string messageSignature, Exception ex, params object[] args)
        {
            WriteTrace(messageSignature, args);
            Error(messageSignature, ex, args);
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
                _currentLogger.RestartLoggingThread();
            }

            return _currentLogger;
        }

        const string _loggingNamespace = "Bam.Net.Logging";
        /// <summary>
        /// Creates a logger of the specified type.  If the containing assembly is already loaded 
        /// the type should be the namespace qualified name of the ILogger implementation to 
        /// instantiate.  If the containing assembly is not already loaded the type should be
        /// the AssemblyQualified name.  If the type is not found an InvalidOperationException is thrown.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static ILogger CreateLogger(string logType)
        {
            lock (_currentLoggerLock)
            {
                string loggerTypeName = string.Format("{0}", logType);
                Type loggerType = null;
                try
                {
                    loggerType = Type.GetType(loggerTypeName);
                }
                catch
                {
                    loggerTypeName = string.Format("{0}Logger", logType);
                    loggerType = Type.GetType(loggerTypeName);
                }
                
                if (loggerType == null)
                {
                    loggerType = Type.GetType(string.Format("{0}.{1}Logger", _loggingNamespace, logType));
                    if (loggerType == null)
                    {
                        loggerType = Type.GetType(string.Format("{0}.{1}", _loggingNamespace, logType));
                    }

                    if (loggerType == null)
                    {
                        throw new InvalidOperationException(string.Format("The specified logType ({0}) was not found.", logType));
                    }
                }

                return CreateLogger(loggerType);
            }
        }

        /// <summary>
        /// Creates a logger of the specified type.
        /// </summary>
        /// <param name="loggerType">Type of logger to instantiate.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static ILogger CreateLogger(Type loggerType)
        {
            ConstructorInfo ctor = loggerType.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new InvalidOperationException(string.Format("The specified logType ({0}) doesn't have a parameterless constructor.", loggerType.FullName));
            }

            return ((ILogger)ctor.Invoke(null)).StartLoggingThread();
        }

        public static MultiTargetLogger AddLogger(Type loggerType)
        {
            return AddLogger(CreateLogger(loggerType));
        }

        public static MultiTargetLogger AddLogger(ILogger loggerInstance)
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
                        if (_currentLogger is Logger current)
                        {
                            current.StopLoggingThread();
                        }
                        _currentLogger = main;
                    }
                }
            }

            main.AddLogger(loggerInstance);
            return main;
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
        public static void BlockUntilEventQueueIsEmpty(int sleep = 0) { Default.BlockUntilEventQueueIsEmpty(sleep); }
        #endregion

        /// <summary>
        /// Restarts the background logging commit thread.
        /// </summary>
        public static void Restart()
        {
            Default.RestartLoggingThread();
        }

        /// <summary>
        /// Starts the background logging commit thread.
        /// </summary>
        public static void Start()
        {
            Default.StartLoggingThread();
        }

        /// <summary>
        /// Stops the background logging commit thread.
        /// </summary>
        public static void Stop()
        {
            Default.StopLoggingThread();
        }        
    }
}
