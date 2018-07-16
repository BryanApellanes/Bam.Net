using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    [Proxy("sysLogReader")]
    [ApiKeyRequired]
    [ServiceSubdomain("syslogreader")]
    public class SystemLogReader : ApplicationProxyableService, ILogReader
    {
        protected SystemLogReader() { }

        public SystemLogReader(SystemLoggerService loggerService)
        {
            SystemLoggerService = loggerService;
            LogReader = new DaoLogger2LogReader(SystemLoggerService.GetLogger());
        }

        protected SystemLoggerService SystemLoggerService { get; }

        protected DaoLogger2LogReader LogReader { get; }

        public override object Clone()
        {
            SystemLogReader clone = new SystemLogReader(SystemLoggerService);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public List<LogEntry> GetLogEntries(DateTime from, DateTime to)
        {
            return LogReader.GetLogEntries(from, to);
        }
    }
}
