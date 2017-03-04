using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CoreServices;
using Bam.Net.Logging;

namespace gloo.services
{
    public class ApplicationLogger : Logger
    {
        public ApplicationLogger(CoreClient client)
        {
            Args.ThrowIfNull(client, "client");
            CoreClient = client;
        }
        public CoreClient CoreClient { get; }
        public override void CommitLogEvent(LogEvent logEvent)
        {
            if ((int)logEvent.Severity <= (int)VerbosityLevel.Error)
            {
                Task.Run(() => CoreClient.LoggerService.Error(logEvent.MessageSignature, logEvent.MessageVariableValues));
            }
        }
    }
}
