using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;

namespace Bam.Net.Logging.NLog
{
    public class NLogTarget: Target
    {
        public static ILogger Logger { get; set; }
        protected override void Write(LogEventInfo logEvent)
        {
            GetLogActions()[logEvent.Level.Name]($"{logEvent.LoggerName}:{logEvent.Message}", logEvent.Parameters);
        }   
        
        private Dictionary<string, Action<string, object[]>> GetLogActions()
        {
            if(Logger == null)
            {
                Logger = Log.Default;
            }
            return new Dictionary<string, Action<string, object[]>>
            {
                {"Trace", Logger.Info },
                {"Debug", Logger.Info },
                {"Info", Logger.Info },
                {"Warn", Logger.Warning },
                {"Error", Logger.Error },
                {"Fatal", Logger.Error },
                {"Off", (s,o)=>{ } },
                {null, (s,o)=>{ } },
                {"", (s,o)=>{ } }
            };
        }
    }
}
