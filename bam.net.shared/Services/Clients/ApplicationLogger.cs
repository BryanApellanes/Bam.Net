using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Services.Clients;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// A Logger that commits event entries to the target of 
    /// a given CoreClient
    /// </summary>
    public class ApplicationLogger : Logger, ILog
    {
        DaoLogger2 _daoLogger;
        public ApplicationLogger(CoreClient client, ApplicationLogDatabase logDb)
        {
            Args.ThrowIfNull(client, "client");
            CoreClient = client;
            _daoLogger = new DaoLogger2(logDb) { CommitCycleDelay = 5000 };            
        }
        
        public override void CommitLogEvent(LogEvent logEvent)
        {
            Task.Run(() => _daoLogger.CommitLogEvent(logEvent));
            if ((int)logEvent.Severity <= (int)VerbosityLevel.Error)
            {
                Task.Run(() => CoreClient.LoggerService.Error(logEvent.MessageSignature, logEvent.MessageVariableValues));
            }
        }

        public override void BlockUntilEventQueueIsEmpty(int sleep = 0)
        {
            _daoLogger.BlockUntilEventQueueIsEmpty(sleep);
            base.BlockUntilEventQueueIsEmpty(sleep);
        }

        public CoreClient CoreClient { get; }
    }
}
