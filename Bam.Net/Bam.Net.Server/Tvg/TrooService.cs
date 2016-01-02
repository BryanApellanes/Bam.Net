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
using Bam.Net.Data.Repositories;
using Bam.Net.Data;

namespace Bam.Net.Server
{
    public class TrooService : ServiceExe
    {
        public static ServiceInfo ServiceInfo
        {
            get
            {
                return new ServiceInfo("TrooService", "Troo Service", "Data persisistence, the source of what's troo");
            }
        }

        static TrooServer _server;
        static object _serverLock = new object();
        public static TrooServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () =>
                {
                    try
                    {
                        ILogger logger = GetLogger();
                        TrooServer server = new TrooServer(BamConf.Load(ServiceConfig.ContentRoot), logger, GetRepository(logger));
                        server.HostPrefixes = new HostPrefix[] { GetConfiguredHostPrefix() };
                        server.MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";");
                        logger.AddEntry("Created Server of Type {0}: {1}", typeof(TrooServer).FullName, server.PropertiesToString());
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

        private static IRepository GetRepository(ILogger logger)
        {
            return new DaoRepository(GetDatabase(), logger);
        }

        private static Database GetDatabase()
        {
            return ServiceConfig.GetConfiguredDatabase();
        }

        private static HostPrefix GetConfiguredHostPrefix()
        {
            return ServiceConfig.GetConfiguredHostPrefix();
        }

        private static ILogger GetLogger()
        {
            return ServiceConfig.GetMultiTargetLogger(TrooService.CreateLog(TrooService.ServiceInfo.ServiceName));
        }
    }
}
