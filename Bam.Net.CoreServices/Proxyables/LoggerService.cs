/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Linq;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices.Proxyables
{
    [Encrypt]
    [Proxy("log")]
    [ApiKeyRequired]
    public class LoggerService
    {
        public LoggerService() { }
        public LoggerService(ILogger logger)
        {
            this.Logger = logger;
        }

        object _loggerLock = new object();
        ILogger _logger;
        public ILogger Logger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () => Log.Default);
            }
            set
            {
                this._logger = value;
            }
        }

        public void Info(string messageSignature, params object[] formatArguments)
        {
            Logger.AddEntry(messageSignature, formatArguments.Select(a => a.ToString()).ToArray());
        }

        public void Warning(string messageSignature, params object[] formatArguments)
        {
            Logger.AddEntry(messageSignature, LogEventType.Warning, formatArguments.Select(a => a.ToString()).ToArray());
        }

        public void Error(string messageSignature, params object[] formatArguments)
        {
            Logger.AddEntry(messageSignature, LogEventType.Error, formatArguments.Select(a => a.ToString()).ToArray());
        }
    }
}
