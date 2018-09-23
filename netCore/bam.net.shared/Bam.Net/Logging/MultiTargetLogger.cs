/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net.Logging
{
    /// <summary>
    /// The type of logger that Log.Default will be set to if
    /// AddLogger is called.  Commit() implementation only 
    /// calls Commit on each logger that has been added through
    /// the AddLogger method.  This class is not intended to be 
    /// used directly.
    /// </summary>
    public class MultiTargetLogger: Logger
    {
        List<ILogger> _loggers;
        public MultiTargetLogger()
            : base()
        {
            this._loggers = new List<ILogger>();
        }

        /// <summary>
        /// Adds the specified logger if it hasn't already been added.  If
        /// the specified logger is the current MultiTargetLogger it will not be added.
        /// If the specified logger is a NullLoger or another MultiTargetLogger it will not be added.
        /// </summary>
        /// <param name="logger"></param>
        public void AddLogger(ILogger logger)
        {
            if (logger.IsNull || logger == null)
            {
                return;
            }

            if (!_loggers.Contains(logger) && logger != this && logger.GetType() != typeof(MultiTargetLogger))
            {
                _loggers.Add(logger);
                SetApplicationNames();
            }
        }

        /// <summary>
        /// An array of all the loggers currently added to this 
        /// MultiTargetLogger
        /// </summary>
        public ILogger[] Loggers
        {
            get
            {
                return _loggers == null ? new ILogger[] { }: _loggers.ToArray();
            }
        }

        public override ILogger StartLoggingThread()
        {
            foreach(ILogger logger in Loggers)
            {
                logger.StopLoggingThread();
            }
            return base.StartLoggingThread();
        }

        /// <summary>
        /// Passes the specified logEvent to the Commit method
        /// of each of the ILoggers in Loggers.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void CommitLogEvent(LogEvent logEvent)
        {
            Parallel.ForEach(Loggers, (logger) => logger.CommitLogEvent(logEvent));
        }

        bool warned;
        private void SetApplicationNames()
        {
            SortedSet<string> appNames = new SortedSet<string>();

            foreach(ILogger logger in Loggers)
            {
                if (!logger.ApplicationName.Equals(DefaultConfiguration.DefaultApplicationName))
                {
                    appNames.Add(logger.ApplicationName);
                }
            }            
            if(appNames.Count > 1 && !warned)
            {
                warned = true;
                Warning("Multiple ApplicationNames found, using ({0}): {1}", ApplicationName, string.Join(", ", appNames.ToArray()));
            }
            string appName= appNames.FirstOrDefault() ?? DefaultConfiguration.DefaultApplicationName;
            ApplicationName = appName;
            foreach (ILogger logger in Loggers)
            {
                logger.ApplicationName = appName;
            }
        }
    }
}
