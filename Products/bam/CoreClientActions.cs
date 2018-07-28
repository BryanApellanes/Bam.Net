using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Bam.Net.Services.Clients;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class CoreClientActions: CommandLineTestInterface
    {
        static Services.Clients.CoreClient _client;
        static CoreClientActions()
        {
            _client = new Services.Clients.CoreClient();
        }

        [ConsoleAction]
        public void GetLogEntries()
        {
            string from = Prompt("Get logs from date:");
            string to = Prompt("Get logs to date:");

            DateTime fromDate = DateTime.Parse(from);
            DateTime toDate = DateTime.Parse(to);

            List<LogEntry> logEntries = _client.GetLogEntries(fromDate, toDate);
            foreach(LogEntry entry in logEntries)
            {
                OutLine($"{entry.Source}, {entry.Message}, {entry.Severity}, {entry.Computer}, {entry.User}, {entry.Time.ToShortDateString()} {entry.Time.ToShortTimeString()}");
            }
        }
    }
}
