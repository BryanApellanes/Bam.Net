using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Bam.Net.Application
{
    public class GlooService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("GlooService", "Gloo Service", "Exposes CLR types as web services, providing the gloo that binds");
            }
        }

        static GlooServer _server;
        static object _serverLock = new object();
        public static GlooServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () =>
                {
                    try
                    {
                        ILogger logger = GetLogger();
                        GlooServer server = new GlooServer(BamConf.Load(ServiceConfig.ContentRoot), logger)
                        {
                            HostPrefixes = new HashSet<HostPrefix> { GetConfiguredHostPrefix() },
                            MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
                        };
                        logger.AddEntry("Created Server of Type {0}: {1}", typeof(GlooServer).FullName, server.PropertiesToString());
                        return server;
                    }
                    catch (Exception ex)
                    {
                        GetLogger().AddEntry("Exception occurred: {0}", ex, ex.Message);
                    }
                    return null;
                });
            }
        }
        protected override void OnStart(string[] args)
        {
            Server.Start();
        }

        protected override void OnStop()
        {
            Server.Stop();
            Thread.Sleep(1000);
        }
        
        private static HostPrefix GetConfiguredHostPrefix()
        {
            return ServiceConfig.GetConfiguredHostPrefix();
        }

        private static ILogger GetLogger()
        {
            return ServiceConfig.GetMultiTargetLogger(GlooService.CreateLog(GlooService.ServiceInfo.ServiceName));
        }
    }
}
