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
using Bam.Net.ServiceProxy;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    [Proxy("sysLoggerSvc")]
    [ApiKeyRequired]
    [ServiceSubdomain("syslogger")]
    public class SystemLoggerService: ApplicationProxyableService, ILog, ILogEventCommitter
    {
        DaoLogger2 _logger;
        protected SystemLoggerService() { }

        public SystemLoggerService(AppConf conf)
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
            _logger = new DaoLogger2(db)
            {
                LogEventCreatedHandler = SetLogEntryProperties
            };
            if (Logger != null)
            {
                Logger = Log.AddLogger(Logger);
            }
            Logger = Log.AddLogger(_logger);
        }

        [Exclude]
        public DaoLogger2 GetLogger()
        {
            return _logger;
        }
        
        public virtual void CommitLogEvent(Bam.Net.Logging.LogEvent logEvent)
        {
            Task.Run(() => Logger.CommitLogEvent(logEvent));
        }

        public virtual void Info(string messageSignature, params object[] formatArguments)
        {
            Bam.Net.Logging.LogEvent entry = _logger.CreateInfoEvent(messageSignature, formatArguments.Select(a => a.ToString()).ToArray());
            SetLogEntryProperties(entry);
            CommitLogEvent(entry);
        }
        
        public virtual void Warning(string messageSignature, params object[] formatArguments)
        {
            Bam.Net.Logging.LogEvent entry = _logger.CreateWarningEvent(messageSignature, formatArguments.Select(a => a.ToString()).ToArray());
            SetLogEntryProperties(entry);
            CommitLogEvent(entry);
        }

        public virtual void Error(string messageSignature, params object[] formatArguments)
        {
            Bam.Net.Logging.LogEvent entry = _logger.CreateErrorEvent(messageSignature, formatArguments.Select(a => a.ToString()).ToArray());
            SetLogEntryProperties(entry);
            CommitLogEvent(entry);
        }        

        public override object Clone()
        {
            SystemLoggerService clone = new SystemLoggerService(AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        private void SetLogEntryProperties(Bam.Net.Logging.LogEvent entry)
        {
            entry.User = CurrentUser.UserName;
            entry.Computer = ClientIpAddress;
            entry.Source = ClientApplicationName;
            entry.Category = ProcessMode.ToString();
        }
    }
}
