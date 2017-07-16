/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using Bam.Net.Data;
using Bam.Net.Logging;
using Bam.Net.Logging.Data;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    [Proxy("loggerSvc")]
    [ApiKeyRequired]
    [ServiceSubdomain("logger")]
    public class CoreLoggerService: CoreProxyableService, ILog, ILogEventCommitter
    {
        DaoLogger2 _logger;
        protected CoreLoggerService() { }

        public CoreLoggerService(AppConf conf)
        {
            AppConf = conf;
        }
        
        [Local]
        public void SetLogger()
        {
            Args.ThrowIfNull(Database);
            SetLogger(Database);
        }

        [Exclude]
        public void SetLogger(Database db)
        {
            db.TryEnsureSchema<Event>();
            _logger = new DaoLogger2(db);
            if(Logger != null)
            {
                Logger = Log.AddLogger(Logger);
            }
            Logger = Log.AddLogger(_logger);
        }

        public virtual void CommitLogEvent(Logging.LogEvent logEvent)
        {
            Logger.CommitLogEvent(logEvent);
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
