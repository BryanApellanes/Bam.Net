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
            Logger = logger;
            DaoRepository.AddType<EventMessage>();
            _listeners = new Dictionary<string, HashSet<Action<EventMessage, IHttpContext>>>();
        }

        public virtual void Trigger(string eventName, string json)
        {
            EventMessage msg = new EventMessage { Name = eventName, UserName = CurrentUser.UserName, CreatedBy = CurrentUser.UserName, Created = DateTime.UtcNow, Json = json };
            DaoRepository.Save(msg);
            FireListenersAsync(eventName, msg, HttpContext);
        }

        [Exclude]
        public virtual void AddListener(string eventName, Action<EventMessage, IHttpContext> listener)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                _listeners.Add(eventName, new HashSet<Action<EventMessage, IHttpContext>>());
            }

            _listeners[eventName].Add(listener);
        }

        protected Task FireListenersAsync(string eventName, EventMessage message, IHttpContext context)
        {
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

        protected ILogger Logger { get; private set; }        

    }
}
