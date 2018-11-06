using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Server;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using System.Reflection;
using Bam.Net.Services;
using Bam.Net.CoreServices;
using System.Threading;

namespace Bam.Net.Application
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {        
        static ServiceProxyServer server;

        [ConsoleAction("killHeartServer", "Kill the Heart server")]
        public static void StopServer()
        {
            if (server != null)
            {
                server.Stop();
                OutLine("Heart stopped", ConsoleColor.Yellow);
            }
            else
            {
                OutLine("Heart server not running");
            }
        }

        [ConsoleAction("startHeartServer", "Start the Heart server")]
        public static void StartServerAndPause()
        {
            ServiceRegistry serviceRegistry = StartServer();
            Pause($"Heart server is serving service registry {serviceRegistry.Name}");
        }

        internal static ServiceRegistry StartServer()
        {
            HostPrefix[] prefixes = GetConfiguredHostPrefixes();
            ILogger logger = GetLogger();
            Log.Default = logger;
            ServiceRegistry serviceRegistry = CoreServiceRegistryContainer.Create();
            server = serviceRegistry.Serve(prefixes, logger);
            return serviceRegistry;
        }

        public static HostPrefix[] GetConfiguredHostPrefixes()
        {
            int port = int.Parse(DefaultConfiguration.GetAppSetting("Port", "80"));
            bool ssl = DefaultConfiguration.GetAppSetting("Ssl").IsAffirmative();
            List<HostPrefix> results = new List<HostPrefix>();
            foreach(string hostName in DefaultConfiguration.GetAppSetting("HostNames").Or("localhost").DelimitSplit(",", true))
            {
                HostPrefix hostPrefix = new HostPrefix()
                {
                    HostName = hostName,
                    Port = port,
                    Ssl = ssl
                };
                results.Add(hostPrefix);
            }
            return results.ToArray();
        }

        private static ILogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false,
                UseColors = true
            };
            logger.StartLoggingThread();
            Log.AddLogger(logger);
            return Log.Default;
        }
    }
}