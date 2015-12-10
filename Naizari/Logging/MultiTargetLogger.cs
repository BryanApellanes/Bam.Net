/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Logging
{
    /// <summary>
    /// The type of logger that Log.Default will be set to if
    /// AddLogger is called.  Commit() implementation only 
    /// calls Commit on each logger that has been added through
    /// the AddLogger method.  This class is not intended to 
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
        /// If the specified logger is a NullLoger it will not be added.
        /// </summary>
        /// <param name="logger"></param>
        public void AddLogger(ILogger logger)
        {
            if (logger.IsNull)
            {
                return;
            }

            if (!_loggers.Contains(logger) && logger != this && _loggers.Where(l => l.GetType() == logger.GetType()).FirstOrDefault() == null)
            {
                _loggers.Add(logger);
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
                return _loggers.ToArray();
            }
        }
        
        /// <summary>
        /// Passes the specified logEvent to the Commit method
        /// of each of the ILoggers in Loggers.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void CommitLogEvent(LogEvent logEvent)
        {
            foreach (ILogger logger in _loggers)
            {
                logger.CommitLogEvent(logEvent);
            }
        }
    }
}
