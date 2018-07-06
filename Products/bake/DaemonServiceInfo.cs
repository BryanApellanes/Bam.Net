using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class DaemonServiceInfo
    {
        public DaemonServiceInfo()
        {
            Port = 8477;
            HostNames = "bamd-".RandomLetters(6);
            Ssl = false;
            ApplicationName = "bamd";
            LogType = "TextFile";
        }

        public string Host { get; set; }
        /// <summary>
        /// Gets or sets a comma separted list of hostnames.
        /// </summary>
        /// <value>
        /// The host names.
        /// </value>
        public string HostNames { get; set; }
        public bool Ssl { get; set; }
        public int Port { get; set; }
        public string ApplicationName { get; set; }
        public string LogType { get; set; }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "Host", Host },
                { "Port", Port.ToString() },
                { "HostNames", HostNames },
                { "Ssl", Ssl.ToString() },
                { "ApplicationName", ApplicationName },
                { "LogType", LogType }
            };
        }
    }
}
