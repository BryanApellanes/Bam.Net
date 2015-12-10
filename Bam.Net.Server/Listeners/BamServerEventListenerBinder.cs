/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.Reflection;
using Bam.Net;

namespace Bam.Net.Server.Listeners
{
    public class BamServerEventListenerBinder
    {
        public BamServerEventListenerBinder(BamConf conf)
        {
            this.BamConf = conf;
        }

        public BamServerEventListenerBinder(BamServer server)
        {
            this.BamConf = server.GetCurrentConf();
        }

        public BamConf BamConf { get; private set; }
        protected internal BamServer Server
        {
            get
            {
                return BamConf.Server;
            }
        }
        public ILogger Logger
        {
            get
            {
                return Server.MainLogger;
            }
        }
        /// <summary>
        /// Bind the BamServerEventListener implementations defined in BamConf 
        /// to the server events
        /// </summary>
        public void Bind()
        {
            ILogger logger = Logger;
            Type serverType = typeof(BamServer);
            BamServerEventListener[] listeners = BamConf.GetServerEventListeners(logger);
            listeners.Each(listener =>
            {
                Type listenerType = listener.GetType();
                listenerType.GetMethodsWithAttributeOfType<ServerEventListenerAttribute>().Each(method =>
                {
                    ServerEventListenerAttribute attr = method.GetCustomAttributeOfType<ServerEventListenerAttribute>();
                    string eventName = attr.EventName.Or(method.Name);
                    EventInfo eventInfo = serverType.GetEvent(eventName);
                    if (eventInfo != null)
                    {
                        eventInfo.AddEventHandler(Server, Delegate.CreateDelegate(listenerType, method));
                    }
                    else
                    {
                        logger.AddEntry("{0}::The specified event name ({1}) was not found on type ({2})", LogEventType.Warning, listener.GetType().Name, method.Name, serverType.Name);
                    }
                });
            });
        }
    }
}
