/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Microsoft.Build.Framework;
using B = Bam.Net.Logging;
using System.Reflection;

namespace Bam.Net.Automation.ContinuousIntegration.Loggers
{
    public class BuildLogger<T>: BuildLogger where T: B.ILogger, new()
    {
        T _actualLogger;
        public BuildLogger()
        {
            this._actualLogger = new T();            
        }

        protected internal T ActualLogger
        {
            get
            {
                return _actualLogger;
            }
        }

        /// <summary>
        /// Set properties on the underlying Bam.Net.Logging.ILogger where names match
        /// the keys in the specified properties dictionary
        /// </summary>
        /// <param name="properties"></param>
        public void SetLoggerProperties(Dictionary<string, object> properties)
        {
            Type loggerType = typeof(T);

            properties.Keys.Each(key =>
            {
                PropertyInfo propInfo = loggerType.GetProperty(key);
                if (propInfo != null)
                {
                    propInfo.SetValue(_actualLogger, properties[key]);
                }
            });
        }

        public override void CommitLogEvent(Logging.LogEvent logEvent)
        {
            this._actualLogger.CommitLogEvent(logEvent);
        }
    }

    public abstract class BuildLogger: B.Logger, M.ILogger
    {
        public event EventHandler ErrorRaised;
        protected void OnErrorRaised(object sender, EventArgs args)
        {
            if (ErrorRaised != null)
            {
                ErrorRaised(sender, args);
            }
        }

        #region ILogger Members
        
        public void Initialize(M.IEventSource eventSource)
        {
            this.StartLoggingThread();
            eventSource.AnyEventRaised += (o, ba) =>
            {
                if(Verbosity >= M.LoggerVerbosity.Diagnostic)
                {
                    AddEntry("AnyEventRaised", ba, B.LogEventType.Information);
                }
            };

            eventSource.BuildFinished += (o, ba) =>
            {
                if(Verbosity >= M.LoggerVerbosity.Minimal)
                {
                    if (ba.Succeeded)
                    {
                        AddEntry("BuildFinished", ba, B.LogEventType.Information);
                    }
                    else
                    {
                        AddEntry("BuildFinished", ba, B.LogEventType.Error);
                    }
                }

                BlockUntilEventQueueIsEmpty();
            };
            eventSource.BuildStarted += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("BuildStarted", ba, B.LogEventType.Information);
                }
            };

            eventSource.CustomEventRaised += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Detailed)
                {
                    AddEntry("CustomEventRaised", ba, B.LogEventType.Custom);
                }
            };

            eventSource.ErrorRaised += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Minimal)
                {
                    AddEntry("ErrorRaised", ba, B.LogEventType.Error);
                }
                OnErrorRaised(o, ba);
            };

            eventSource.MessageRaised += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Detailed)
                {
                    AddEntry("MessageRaised", ba, B.LogEventType.Information);
                }
            };

            eventSource.ProjectFinished += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("ProjectFinished", ba, B.LogEventType.Information);
                }
            };

            eventSource.ProjectStarted += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("ProjectStarted", ba, B.LogEventType.Information);
                }
            };
            
            eventSource.TargetFinished += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("TargetFinished", ba, B.LogEventType.Information);
                }
            };

            eventSource.TargetStarted += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("TargetStarted", ba, B.LogEventType.Information);
                }
            };

            eventSource.TaskFinished += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("TaskFinished", ba, B.LogEventType.Information);
                }
            };

            eventSource.TaskStarted += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("TaskStarted", ba, B.LogEventType.Information);
                }
            };

            eventSource.WarningRaised += (o, ba) =>
            {
                if (Verbosity >= M.LoggerVerbosity.Normal)
                {
                    AddEntry("WarningRaised", ba, B.LogEventType.Warning);
                }
            };
        }

        protected override StringBuilder HandleDetails(B.LogEvent ev)
        {
            return new StringBuilder(ev.Message);
        }

        protected override void HandleStackTrace(Exception ex, StringBuilder message, StringBuilder stack)
        {
            // turn off the stack trace
        }

        protected virtual void AddEntry(string eventName, M.BuildEventArgs ba, B.LogEventType entryType)
        {
            BuildEventInfo info = new BuildEventInfo(ba);
            AddEntry("{0}\t,{1}"._Format(eventName, info.ToCsvLine()), entryType);
        }

        public string Parameters
        {
            get;
            set;
        }

        public void Shutdown()
        {
            BlockUntilEventQueueIsEmpty();
            StopLoggingThread();
        }


        #endregion


        public new M.LoggerVerbosity Verbosity
        {
            get
            {
                return (M.LoggerVerbosity)((int)base.Verbosity);
            }
            set
            {
                base.Verbosity = (B.VerbosityLevel)((int)value);
            }
        }
    }
}
