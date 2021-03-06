/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Bam.Net.Logging
{
    /// <summary>
    /// An abstract base class providing methods for 
    /// subscribing to and firing events defined on derived
    /// classes.
    /// </summary>
	[Serializable]
    public abstract class Loggable: ILoggable
    {
        public Loggable()
        {
            this._subscribers = new HashSet<ILogger>();
            this.LogVerbosity = LogEventType.Custom;
        }

        /// <summary>
        /// A value from 0 - 5, represented by the LogEventType enum.
        /// The higher the value the more log entries will 
        /// be logged.
        /// </summary>
        public LogEventType LogVerbosity { get; set; }

        HashSet<ILogger> _subscribers;
        /// <summary>
        /// An array of all the ILoggers that have
        /// been subscribed to this Loggable
        /// </summary>
        [JsonIgnore]
        public virtual ILogger[] Subscribers
        {
            get { return _subscribers.ToArray(); }
        }

        object _subscriberLock = new object();

		/// <summary>
		/// Subscribe the current Loggables subscribers
		/// to the specified Loggable and vice versa
		/// </summary>
		/// <param name="loggable"></param>
        [Exclude]
		public virtual void Subscribe(Loggable loggable)
		{
			Subscribers.Each(logger =>
			{
				loggable.Subscribe(logger);
			});
            loggable.Subscribers.Each(logger =>
            {
                Subscribe(logger);
            });
		}

        /// <summary>
        /// Subscribe the specified logger to
        /// all the events of the current type
        /// where the event delegate is defined
        /// as an EventHandler.  This method 
        /// will also take into account the 
        /// current value of LogVerbosity if
        /// the events found are addorned with the 
        /// Verbosity attribute
        /// </summary>
        /// <param name="logger"></param>
        [Exclude]
        [DebuggerStepThrough]
        public virtual void Subscribe(ILogger logger)
        {
            lock (_subscriberLock)
            {
                if (logger != null && !IsSubscribed(logger))
                {
                    _subscribers.Add(logger);
                    Type currentType = this.GetType();
                    EventInfo[] eventInfos = currentType.GetEvents();
                    eventInfos.Each(eventInfo =>
                    {
                        bool shouldSubscribe = true;
                        VerbosityLevel logEventType = VerbosityLevel.Information;
                        if (eventInfo.HasCustomAttributeOfType(out VerbosityAttribute verbosity))
                        {
                            shouldSubscribe = (int)verbosity.Value <= (int)LogVerbosity;
                            logEventType = verbosity.Value;
                        }

                        if (shouldSubscribe)
                        {
                            if (eventInfo.EventHandlerType.Equals(typeof(EventHandler)))
                            {
                                eventInfo.AddEventHandler(this, (EventHandler)((s, a) =>
                                {
                                    string message = "";
                                    if (verbosity != null)
                                    {
                                        if (!verbosity.TryGetMessage(s, out message))
                                        {
                                            verbosity.TryGetMessage(a, out message);
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        logger.AddEntry(message, (int)logEventType);
                                    }
                                    else
                                    {
                                        logger.AddEntry("Event {0} raised on type {1}::{2}", (int)logEventType, logEventType.ToString(), currentType.Name, eventInfo.Name);
                                    }
                                }));
                            }
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Returns true if the specified logger is 
        /// subscribed to the current Loggable
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        [Exclude]
        public virtual bool IsSubscribed(ILogger logger)
        {
            return _subscribers.Contains(logger);
        }

        /// <summary>
        /// Fire the specified event if there are
        /// subscribers
        /// </summary>
        /// <param name="eventHandler"></param>
        protected void FireEvent(EventHandler eventHandler)
        {
            FireEvent(eventHandler, EventArgs.Empty);
        }

        /// <summary>
        /// Fire the specified event if there are subscribers
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="eventArgs"></param>
		protected void FireEvent(EventHandler eventHandler, EventArgs eventArgs)
        {
            try
            {
                eventHandler?.Invoke(this, eventArgs);
            }
            catch (Exception ex)
            {
                Log.Trace("Exception in FireEvent: {0}", ex.Message);
            }
        }

        protected void FireEvent(EventHandler eventHandler, object sender, EventArgs eventArgs)
        {
            try
            {
                eventHandler?.Invoke(sender, eventArgs);
            }
            catch (Exception ex)
            {
                Log.Trace("Exception in FireEvent2: {0}", ex.Message);
            }
        }
    }
}
