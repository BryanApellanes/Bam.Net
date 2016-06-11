using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices
{
    public abstract class EventSource: ProxyableService
    {
        Dictionary<string, HashSet<Action<EventMessage, IHttpContext>>> _listeners;
        public EventSource(DaoRepository daoRepository, AppConf appConf, ILogger logger): base(daoRepository, appConf)
        {
            SupportedEvents = new HashSet<string>();
            Logger = logger ?? appConf?.Logger ?? Log.Default;
            DaoRepository?.AddType<EventMessage>();
            _listeners = new Dictionary<string, HashSet<Action<EventMessage, IHttpContext>>>();
        }

        public override object Clone()
        {
            object instance = GetType().Construct(DaoRepository, AppConf, Logger);
            instance.CopyProperties(this);
            return instance;
        }

        public virtual Task FireEvent(string eventName)
        {
            InvokeEventSubscriptions(eventName);
            return FireEvent(eventName, JsonData?.ToJson());
        }

        public virtual Task FireEvent(string eventName, EventArgs args)
        {
            InvokeEventSubscriptions(eventName, args);
            return FireEvent(eventName, args.ToJson());            
        }

        public virtual Task FireEvent(string eventName, string json)
        {
            EventMessage msg = new EventMessage { Name = eventName, UserName = CurrentUser.UserName, CreatedBy = CurrentUser.UserName, Created = DateTime.UtcNow, Json = json };
            DaoRepository.Save(msg);
            return FireListenersAsync(eventName, msg, HttpContext);
        }

        [Exclude]
        public virtual void Subscribe(string eventName, Action<EventMessage, IHttpContext> listener)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                _listeners.Add(eventName, new HashSet<Action<EventMessage, IHttpContext>>());
            }

            _listeners[eventName].Add(listener);
        }

        protected Task FireListenersAsync(string eventName, EventMessage message, IHttpContext context)
        {
            GlobalEvents.FireListenersAsync(GetType(), eventName, message, context);
            return Task.Run(() =>
            {
                if (_listeners.ContainsKey(eventName))
                {
                    Parallel.ForEach(_listeners[eventName], (action) =>
                    {
                        try
                        {
                            action(message, context);
                        }
                        catch (Exception ex)
                        {
                            Logger.AddEntry("{0}::Exception occurred in EventSource Listenter for event name ({1}): {2}", LogEventType.Warning, GetType().Name, eventName, ex.Message);
                        }
                    });
                }
                else
                {
                    Logger.AddEntry("{0}::Specified eventName not found ({1})", LogEventType.Warning, GetType().Name, eventName);
                }
            });
        }
        protected HashSet<string> SupportedEvents { get; set; }

        private void InvokeEventSubscriptions(string eventName, EventArgs args = null)
        {
            args = args ?? EventArgs.Empty;
            IEnumerable<EventSubscription> subs = this.GetEventSubscriptions(eventName);
            if (subs.Count() > 0)
            {
                subs.Each(es =>
                {
                    es.Invoke(this, args);
                });
            }
        }

    }
}
