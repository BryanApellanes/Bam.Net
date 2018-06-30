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
                    ILogger logger = GetLogger();
                    Log.Default = logger;
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
                Log.AddLogger(GetLogger());
                Log.AddEntry("{0} starting", ServiceInfo.ServiceName);
                ProcessMonitorService.Start();
                Server.Start();
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error starting service: {0}", ex, ex.Message);
            }
        }

        protected override void OnStop()
        {
            Log.AddEntry("{0} stopping", ServiceInfo.ServiceName);
            Server.Stop();
            ProcessMonitorService.Stop();            
        }

        private static HostPrefix[] GetConfiguredHostPrefixes()
        {
            return ServiceConfig.GetConfiguredHostPrefixes();
        }

        private static ILogger GetLogger()
        {   
            return ServiceConfig.GetMultiTargetLogger(CreateLog(ServiceInfo.ServiceName));
        }
    }
}
