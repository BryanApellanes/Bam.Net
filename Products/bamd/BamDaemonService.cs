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
using System.Text;
using System.Threading;

namespace Bam.Net.Application
{
    public class BamDaemonService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("BamDaemonService", "Bam Daemon Service", "Bam Windows agent daemon (service)");
            }
        }

        static BamDaemonServer _server;
        static object _serverLock = new object();
        public static BamDaemonServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () =>
                {
                    ILogger logger = GetLogger();
                    Log.Default = logger;
                    try
                    {
                        BamDaemonServer server = new BamDaemonServer(BamConf.Load(ServiceConfig.ContentRoot), logger)
                        {
                            HostPrefixes = new HashSet<HostPrefix>(GetConfiguredHostPrefixes()),
                            MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
                        };
                        logger.AddEntry("Created Server of Type {0}: {1}", typeof(BamDaemonServer).FullName, server.PropertiesToString());
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
                
        static Dictionary<string, BamDaemonProcessMonitor> _monitors;
        protected override void OnStart(string[] args)
        {
            try
            {
                Log.AddLogger(GetLogger());
                Log.AddEntry("{0} starting", ServiceInfo.ServiceName);
                _monitors = new Dictionary<string, BamDaemonProcessMonitor>();

                string contentRoot = ServiceConfig.ContentRoot;
                string configRoot = Path.Combine(contentRoot, "conf");
                FileInfo file = new FileInfo(Path.Combine(configRoot, $"{nameof(BamDaemonProcess).Pluralize()}.json"));
                if (!file.Exists)
                {
                    Log.AddEntry("BambotProcesses.json not found: {0}", file.FullName);
                }
                else
                {
                    BamDaemonProcess[] processes = file.FullName.FromJsonFile<BamDaemonProcess[]>();
                    Expect.IsNotNull(processes, "processes was null");
                    Log.AddEntry("{0} processes in BambotProcesses.json", processes.Length.ToString());
                    foreach (BamDaemonProcess process in processes)
                    {
                        StartProcess(process);
                    }
                }

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
            foreach (string key in _monitors.Keys)
            {
                try
                {
                    Log.AddEntry("Stopping {0}", key);
                    _monitors[key].Process.Kill();
                    Log.AddEntry("Stopped {0}", key);
                }
                catch (Exception ex)
                {
                    Log.AddEntry("Exception stopping process {0}: {1}", ex, key, ex.Message);
                }
            }
            Thread.Sleep(1000);
        }

        private static HostPrefix[] GetConfiguredHostPrefixes()
        {
            return ServiceConfig.GetConfiguredHostPrefixes();
        }

        private static ILogger GetLogger()
        {   
            return ServiceConfig.GetMultiTargetLogger(CreateLog(ServiceInfo.ServiceName));
        }

        private void StartProcess(BamDaemonProcess process)
        {
            try
            {
                string key = process.ToString();
                Log.AddEntry("Starting {0}", key);
                process.Subscribe(Log.Default);
                _monitors.Add(key, BamDaemonProcessMonitor.Start(process));
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error starting process {0}: {1}", ex, process?.ToString(), ex.Message);
            }
        }
    }
}
