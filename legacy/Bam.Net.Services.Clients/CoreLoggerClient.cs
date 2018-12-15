using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using System.Diagnostics;

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
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            _loggerService = _proxyFactory.GetProxy<SystemLoggerService>(hostName, port, logger);
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            try
            {
                Task.Run(() => _loggerService.CommitLogEvent(logEvent));
            }
            catch (Exception ex)
            {
                Args.PopMessageAndStackTrace(ex, out StringBuilder message, out StringBuilder stackTrace);
                Trace.Write($"{nameof(CoreLoggerClient)}:: Error committing log event:{message.ToString()}\r\n{stackTrace.ToString()}");
            }
        }
    }
}
