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

namespace Bam.Net.Services.Events
{
    public abstract class EventSourceService: ProxyableService
    {
        Dictionary<string, HashSet<EventSubscription>> _listeners;
        public EventSourceService(DaoRepository daoRepository, AppConf appConf, ILogger logger): base(daoRepository, appConf)
        {
            SupportedEvents = new HashSet<string>();
            Logger = logger ?? appConf?.Logger ?? Log.Default;
            DaoRepository?.AddType<EventMessage>();
            _listeners = new Dictionary<string, HashSet<EventSubscription>>();
        }

        [Local]
        public HashSet<EventSubscription> GetListeners(string eventName)
        {
            if (_listeners.ContainsKey(eventName))
            {
                return _listeners[eventName];
            }
            return new HashSet<EventSubscription>();
        }

        [Local]
        public virtual void Subscribe(string eventName, EventHandler handler)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                _listeners.Add(eventName, new HashSet<EventSubscription>());
            }

            _listeners[eventName].Add(EventSubscription.FromEventHandler(handler));
        }

        public object EventData { get; set; }

        public override object Clone()
        {
            EventSourceService instance = GetType().Construct<EventSourceService>(DaoRepository, AppConf, Logger);
            instance.CopyProperties(this);
            instance.CopyEventHandlers(this);
            instance._listeners = _listeners;
            return instance;
        }

        public virtual Task Trigger(string eventName)
        {
            InvokeEventSubscriptions(eventName);
            return Trigger(eventName, EventData?.ToJson());
        }

        public virtual Task Trigger(string eventName, EventArgs args)
        {
            InvokeEventSubscriptions(eventName, args);
            IJsonable jsonable = args as IJsonable;
            return Trigger(eventName, jsonable == null ? string.Empty: jsonable.ToJson());
        }

        public virtual Task Trigger(string eventName, string json)
        {
            EventMessage msg = new EventMessage { Name = eventName, UserName = CurrentUser.UserName, CreatedBy = CurrentUser.UserName, Created = DateTime.UtcNow, Json = json };
            return Trigger(eventName, msg);
        }

        public virtual Task Trigger(string eventName, EventMessage msg)
        {  
            return Trigger(eventName, this, new EventSourceServiceArgs { EventMessage = msg });
        }

        public virtual Task Trigger(string eventName, object sender, EventSourceServiceArgs args)
        {
            if(args.EventMessage != null)
            {
                DaoRepository.Save(args.EventMessage);
            }
            return FireListenersAsync(eventName, sender, args);
        }

        protected Task FireListenersAsync(string eventName, object sender, EventArgs args)
        {
            InProcessEvents.FireListenersAsync(GetType(), eventName, sender, args);
            return Task.Run(() =>
            {
                if (_listeners.ContainsKey(eventName))
                {
                    Parallel.ForEach(_listeners[eventName], (subscription) =>
                    {
                        try
                        {
                            subscription.Invoke(sender, args);
                        }
                        catch (Exception ex)
                        {
                            Logger.AddEntry("{0}::Exception occurred in EventSource Listenter for event name ({1}): {2}", LogEventType.Warning, ex, GetType().Name, eventName, ex.Message);
                        }
                    });
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
