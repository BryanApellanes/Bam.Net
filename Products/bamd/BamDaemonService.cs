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
        const int MaxLineCount = 10000;

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
                    try
                    {                        
                        BamDaemonServer server = new BamDaemonServer(logger)
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

        static Dictionary<string, BamDaemonProcess> _processes;
        static Dictionary<string, FileInfo> _standardOuts;
        static Dictionary<string, FileInfo> _standardErrors;
        protected override void OnStart(string[] args)
        {
            try
            {
                Log.AddLogger(GetLogger());
                Log.AddEntry("{0} starting", ServiceInfo.ServiceName);

                _processes = new Dictionary<string, BamDaemonProcess>();
                _standardOuts = new Dictionary<string, FileInfo>();
                _standardErrors = new Dictionary<string, FileInfo>();

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

        private void StartProcess(BamDaemonProcess process)
        {
            try
            {
                string key = process.ToString();
                Log.AddEntry("Starting {0}", key);
                if (!_processes.ContainsKey(key))
                {
                    _processes.Add(key, process);
                }
                if (!_standardOuts.ContainsKey(key))
                {
                    _standardOuts.Add(key, new FileInfo(Path.Combine(ServiceConfig.LogRoot, $"{Path.GetFileNameWithoutExtension(process.Name)}_out.txt")));
                }
                if (!_standardErrors.ContainsKey(key))
                {
                    _standardErrors.Add(key, new FileInfo(Path.Combine(ServiceConfig.LogRoot, $"{Path.GetFileNameWithoutExtension(process.Name)}_err.txt")));
                }
                process.Subscribe(Log.Default);
                void restarter(object s, EventArgs a) // exit handler that restarts the process if it exits
                {
                    Log.AddEntry("Process died, restarting: {0}", key);
                    Thread.Sleep(2500);
                    try
                    {
                        BamDaemonProcess pr = _processes[key];
                        pr.ProcessOutput.ActiveStandardOut = new StringBuilder();
                        pr.ProcessOutput.ActiveStandardError = new StringBuilder();
                        if (pr.RetryStart(restarter))
                        {
                            Log.AddEntry("{0} restarted", key);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.AddEntry("Error restarting process {0}: {1}", ex, key, ex.Message);
                    }
                }
                process.StandardOut += (s, a) =>
                {
                    BamDaemonProcessEventArgs args = (BamDaemonProcessEventArgs)a;
                    string msg = $"{args.Message}\r\n";
                    if(args.BambotProcess.StandardOutLineCount > MaxLineCount)
                    {
                        msg.SafeWriteToFile(_standardOuts[key].FullName);
                    }
                    else
                    {
                        args.BambotProcess.StandardOutLineCount++;
                        msg.SafeAppendToFile(_standardOuts[key].FullName);
                    }
                };
                process.ErrorOut += (s, a) =>
                {
                    BamDaemonProcessEventArgs args = (BamDaemonProcessEventArgs)a;
                    string msg = $"{args.Message}\r\n";
                    if (args.BambotProcess.StandardErrorLineCount > MaxLineCount)
                    {
                        msg.SafeWriteToFile(_standardErrors[key].FullName);
                    }
                    else
                    {
                        args.BambotProcess.StandardErrorLineCount++;
                        msg.SafeAppendToFile(_standardErrors[key].FullName);
                    }
                };
                process.Start(restarter);
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error starting process {0}: {1}", ex, process?.ToString(), ex.Message);
            }
        }

        protected override void OnStop()
        {
            Server.Stop();
            foreach (string key in _processes.Keys)
            {
                try
                {
                    _processes[key].ProcessOutput.Process.Kill();
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
            ILogger logger = CreateLog(ServiceInfo.ServiceName);
            
            return ServiceConfig.GetMultiTargetLogger(logger);
        }
    }
}
