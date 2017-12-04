using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Logging;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// A Logger that commits LogEvents to 
    /// a specified CoreLoggerService
    /// </summary>
    public class CoreLoggerClient : Logger
    {
        ProxyFactory _proxyFactory;
        SystemLoggerService _loggerService;
        public CoreLoggerClient(string hostName, int port)
        {
            _proxyFactory = new ProxyFactory();
            _loggerService = _proxyFactory.GetProxy<SystemLoggerService>(hostName, port);
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            _loggerService.CommitLogEvent(logEvent);
        }
    }
}
