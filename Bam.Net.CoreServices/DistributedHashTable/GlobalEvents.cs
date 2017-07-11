using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.CoreServices.DistributedHashTable;
using Bam.Net.CoreServices.DistributedHashTable;

namespace Bam.Net.CoreServices
{
    public static class GlobalEvents
    {
        static Dictionary<string, HashSet<EventSubscription>> _listeners;
        static GlobalEvents()
        {
            _listeners = new Dictionary<string, HashSet<EventSubscription>>();
            Logger = Log.Default;
        }

        public static ILogger Logger { get; set; }

        public static void Subscribe(object src, string eventName, EventHandler listener)
        {
            src.SubscribeOnce(eventName, (o, a) =>
            {
                EventMessage msg = new EventMessage { Name = eventName, Created = DateTime.UtcNow, Json = a?.ToJson(), Sender = o, EventArgs = a };
                FireListenersAsync(src.GetType(), eventName, o, new EventSourceServiceArgs { EventMessage = msg });
            });
            Subscribe(src.GetType(), eventName, listener);
        }

        public static void Subscribe<T>(string eventName, EventHandler listener)
        {
            Subscribe(typeof(T), eventName, listener);
        }

        public static void Subscribe(Type type, string eventName, EventHandler listener)
        {
            string eventKey = GetEventKey(type, eventName);
            _listeners.AddMissing(eventKey, new HashSet<EventSubscription>());
            _listeners[eventKey].Add(EventSubscription.FromEventHandler(listener));
        }

        public static void ClearSubscribers<T>(string eventName)
        {
            ClearSubscribers(typeof(T), eventName);
        }

        public static void ClearSubscribers(Type type, string eventName)
        {
            string eventKey = GetEventKey(type, eventName);
            if (_listeners.ContainsKey(eventKey))
            {
                _listeners[eventKey].Clear();
            }
        }

        public static Task FireListenersAsync(Type type, string eventName, object sender, EventArgs args)
        {
            return Task.Run(() =>
            {
                string eventKey = GetEventKey(type, eventName);
                if (_listeners.ContainsKey(eventKey))
                {
                    Parallel.ForEach(_listeners[eventKey], (subscription) =>
                    {
                        try
                        {
                            subscription.Invoke(sender, args);
                        }
                        catch (Exception ex)
                        {
                            Logger.AddEntry("GlobalEvents::Exception occurred in EventSource Listenter for event name ({0}): {1}", LogEventType.Warning, eventName, ex.Message);
                        }
                    });
                }
            });
        }

        private static string GetEventKey(Type type, string eventName)
        {
            return $"{type.Name}.{eventName}";
        }

    }
}
