using Bam.Net.CoreServices.Logging;
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
    public class SystemLogReaderService : ApplicationProxyableService, ILogReader
    {
        protected SystemLogReaderService() { }

        public SystemLogReaderService(SystemLoggerService loggerService)
        {
            SystemLoggerService = loggerService;
            LogReader = new DaoLogger2LogReader(SystemLoggerService.GetLogger());
        }

        protected SystemLoggerService SystemLoggerService { get; }

        protected DaoLogger2LogReader LogReader { get; }

        public override object Clone()
        {
            SystemLogReaderService clone = new SystemLogReaderService(SystemLoggerService);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public virtual List<LogEntry> GetLogEntries(DateTime from, DateTime to)
        {
            return LogReader.GetLogEntries(from, to);
        }

        public virtual List<LogEntry> GetLogEntriesFrom(DateTime since, string applicationName, string machineName)
        {
            return GetLogEntries(since, DateTime.UtcNow).Where(le => le.Computer.Equals(machineName, StringComparison.InvariantCultureIgnoreCase) && le.Source.Equals(applicationName)).ToList();
        }

        public virtual List<LogEntrySource> GetSources(DateTime since)
        {
            return GetLogEntries(since, DateTime.UtcNow).Select(le => new LogEntrySource { ComputerName = le.Computer, ApplicationName = le.Source }).ToList();
        }
    }
}
