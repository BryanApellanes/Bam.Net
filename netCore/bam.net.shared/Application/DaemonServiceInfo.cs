using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    /// <summary>
    /// Class that represents settings used by a running daemon.
    /// </summary>
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
        /// Gets or sets a comma separted list of hostnames.  These
        /// hostnames represent the hostnames the daemon responds to.
        /// </summary>
        /// <value>
        /// The host names.
        /// </value>
        public string HostNames { get; set; }
        public bool Ssl { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the application the daemon will 
        /// know itself as.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the type of the log.
        /// </summary>
        /// <value>
        /// The type of the log.
        /// </value>
        public string LogType { get; set; }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "Port", Port.ToString() },
                { "HostNames", HostNames },
                { "Ssl", Ssl.ToString() },
                { "ApplicationName", ApplicationName },
                { "LogType", LogType }
            };
        }
    }
}
