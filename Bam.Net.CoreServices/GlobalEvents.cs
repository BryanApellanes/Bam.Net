using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices
{
    public static class GlobalEvents
    {
        static Dictionary<string, List<Action<EventMessage, IHttpContext>>> _listeners;
        static GlobalEvents()
        {
            _listeners = new Dictionary<string, List<Action<EventMessage, IHttpContext>>>();
            Logger = Log.Default;
        }

        public static ILogger Logger { get; set; }

        public static void Subscribe<T>(string eventName, Action<EventMessage, IHttpContext> listener)
        {
            Subscribe(typeof(T), eventName, listener);
        }

        public static void Subscribe(Type type, string eventName, Action<EventMessage, IHttpContext> listener)
        {
            string eventKey = GetEventKey(type, eventName);
            _listeners.AddMissing(eventKey, new List<Action<EventMessage, IHttpContext>>());
            _listeners[eventKey].Add(listener);
        }

        public static Task FireListenersAsync(Type type, string eventName, EventMessage message, IHttpContext context)
        {
            return Task.Run(() =>
            {
                string eventKey = GetEventKey(type, eventName);
                if (_listeners.ContainsKey(eventKey))
                {
                    Parallel.ForEach(_listeners[eventKey], (action) =>
                    {
                        try
                        {
                            action(message, context);
                        }
                        catch (Exception ex)
                        {
                            Logger.AddEntry("GlobalEvents::Exception occurred in EventSource Listenter for event name ({0}): {1}", LogEventType.Warning, eventName, ex.Message);
                        }
                    });
                }
                else
                {
                    Logger.AddEntry("GlobalEvents::Specified eventName not found ({0})", LogEventType.Warning, eventName);
                }
            });
        }

        private static string GetEventKey(Type type, string eventName)
        {
            return $"{type.Name}.{eventName}";
        }

    }
}
