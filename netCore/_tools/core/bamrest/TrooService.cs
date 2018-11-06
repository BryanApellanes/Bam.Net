/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Bam.Net.Application
{
    public class TrooService //: ServiceExe
    {
        //public static ServiceInfo ServiceInfo
        //{
        //    get
        //    {
        //        return new ServiceInfo("TrooService", "Troo Service", "Data persisistence, the source of what's troo");
        //    }
        //}

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
                        TrooServer server = new TrooServer(BamConf.Load(ServiceConfig.ContentRoot), logger, GetRepository(logger))
                        {
                            HostPrefixes = new HashSet<HostPrefix>(GetConfiguredHostPrefixes()),
                            MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
                        };
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
        //protected override void OnStart(string[] args)
        //{
        //    Server.Start();
        //}

        //protected override void OnStop()
        //{
        //    Server.Stop();
        //    Thread.Sleep(1000);
        //}

        private static IRepository GetRepository(ILogger logger)
        {
            return new DaoRepository(GetDatabase(), logger);
        }

        private static Database GetDatabase()
        {
            return ServiceConfig.GetConfiguredDatabase();
        }

        private static HostPrefix[] GetConfiguredHostPrefixes()
        {
            return ServiceConfig.GetConfiguredHostPrefixes();
        }

        private static ILogger GetLogger()
        {
            return new ConsoleLogger();//ServiceConfig.GetMultiTargetLogger(TrooService.CreateLog(TrooService.ServiceInfo.ServiceName));
        }
    }
}
