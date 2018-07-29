using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.Services.Clients;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class CoreClientActions : CommandLineTestInterface
    {
        static CoreClient _client;
        static CoreClientActions()
        {
            _client = new CoreClient();
        }

        [ConsoleAction("setRemoteSmtpSettings")]
        public void SetRemoteSmtpCredentials()
        {
            CredentialManagementService svc = _client.GetProxy<CredentialManagementService>();
            svc.SetSmtpSettings(DataSettingsSmtpSettingsProvider.Default.SmtpSettings);
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
                OutLine($"{entry.Severity}:: {entry.Computer}:: Message = {entry.Message}, {entry.User}, {entry.Time.ToShortDateString()} {entry.Time.ToShortTimeString()}");
            }
        }
    }
}
