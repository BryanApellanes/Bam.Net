/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bam.Net.Application
{
    public class DaemonService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("BamDaemonService", "Bam Daemon Service", "Bam Windows agent daemon (service)");
            }
        }

        static DaemonServer _server;
        static object _serverLock = new object();
        public static DaemonServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () =>
                {
                    ILogger logger = GetDaemonServiceLogger();
                    try
                    {
                        ProcessMonitorService = new DaemonProcessMonitorService(logger);
                        DaemonServer server = new DaemonServer(BamConf.Load(ServiceConfig.ContentRoot), ProcessMonitorService, logger)
                        {
                            HostPrefixes = new HashSet<HostPrefix>(GetConfiguredHostPrefixes()),
                            MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
                        };
                        logger.AddEntry("Created Server of Type {0}: {1}", typeof(DaemonServer).FullName, server.PropertiesToString());
                        return server;
                    }
                    catch (Exception ex)
                    {
                        logger.AddEntry("Exception occurred: {0}", ex, ex.Message);
                    }
                    return null;
                });
            }
        }

        protected static DaemonProcessMonitorService ProcessMonitorService { get; set; }
        protected override void OnStart(string[] args)
        {
            try
            {
                Log.AddLogger(GetDaemonServiceLogger());
                Log.AddEntry("{0} starting", ServiceInfo.ServiceName);
                Server.Start();
                ProcessMonitorService.Start();
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error starting bam daemon service: {0}", ex, ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                Log.AddEntry("{0} stopping", ServiceInfo.ServiceName);
                Server.Stop();
                ProcessMonitorService.Stop();
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error stopping {0}: {1}", LogEventType.Warning, ServiceInfo.ServiceName, ex.Message);
            }
        }

        private static HostPrefix[] GetConfiguredHostPrefixes()
        {
            return ServiceConfig.GetConfiguredHostPrefixes();
        }

        static ILogger _daemonServiceLogger;
        static object _daemonServiceLoggerLock = new object();
        private static ILogger GetDaemonServiceLogger()
        {
            return _daemonServiceLoggerLock.DoubleCheckLock(ref _daemonServiceLogger, () => ServiceConfig.GetMultiTargetLogger(CreateLog(ServiceInfo.ServiceName)));
        }
    }
}
