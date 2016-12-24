/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    [Proxy("loggerSvc")]
    [ApiKeyRequired]
    public class CoreLoggerService: ProxyableService, ILog
    {
        protected CoreLoggerService() { }
        public CoreLoggerService(AppConf conf)
        {
            AppConf = conf;
        }
        
        public virtual void Info(string messageSignature, params object[] formatArguments)
        {
            Logger.AddEntry(messageSignature, formatArguments.Select(a => a.ToString()).ToArray());
        }

        public virtual void Warning(string messageSignature, params object[] formatArguments)
        {
            Logger.AddEntry(messageSignature, LogEventType.Warning, formatArguments.Select(a => a.ToString()).ToArray());
        }

        public virtual void Error(string messageSignature, params object[] formatArguments)
        {
            Logger.AddEntry(messageSignature, LogEventType.Error, formatArguments.Select(a => a.ToString()).ToArray());
        }

        public override object Clone()
        {
            CoreLoggerService clone = new CoreLoggerService(AppConf);
            clone.CopyProperties(this);
            return clone;
        }
    }
}
