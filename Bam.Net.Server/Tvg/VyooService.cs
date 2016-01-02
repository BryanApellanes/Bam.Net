/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Configuration;
using System.IO;
using System.Reflection;
using Bam.Net.Server.Tvg;
using Bam.Net.Server;

namespace Bam.Net.Server
{
    public class VyooService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("VyooService", "Vyoo Service", "Template Vyoo Server");
            }
        }

        static VyooServer _server;
        static object _serverLock = new object();
        public static VyooServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () =>
                {
                    try
                    {
                        ILogger logger = GetLogger();
                        VyooServer server = new VyooServer(BamConf.Load(ServiceConfig.ContentRoot), logger);
                        server.HostPrefixes = new HostPrefix[] { GetConfiguredHostPrefix() };
                        server.MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";");
                        logger.AddEntry("Created Server of Type {0}: {1}", typeof(VyooServer).FullName, server.PropertiesToString());
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
            return ServiceConfig.GetMultiTargetLogger(VyooService.CreateLog(VyooService.ServiceInfo.ServiceName));
        }
    }
}
