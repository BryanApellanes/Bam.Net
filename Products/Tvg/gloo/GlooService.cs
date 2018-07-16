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
                return new ServiceInfo("GlooService", "Gloo Service", "Exposes CLR types as web services, providing the gloo that binds.");
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
                    ILogger logger = GetGlooServiceLogger();
                    try
                    {
                        GlooServer server = new GlooServer(BamConf.Load(ServiceConfig.ContentRoot), logger)
                        {
                            HostPrefixes = new HashSet<HostPrefix>(GetConfiguredHostPrefixes()),
                            MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
                        };
                        logger.AddEntry("Created Server of Type {0}: {1}", typeof(GlooServer).FullName, server.PropertiesToString());
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

        protected override void OnStart(string[] args)
        {
            try
            {
                Log.AddLogger(GetGlooServiceLogger());
                Log.AddEntry("{0} starting", ServiceInfo.ServiceName);
                Server.Start();                
            }
            catch (Exception ex)
            {
                Log.AddEntry("Error starting gloo service: {0}", ex, ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                Log.AddEntry("{0} stopping", ServiceInfo.ServiceName);
                Server.Stop();
                Thread.Sleep(1000);
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

        static ILogger _glooServiceLogger;
        static object _glooServiceLoggerLock = new object();
        private static ILogger GetGlooServiceLogger()
        {
            return _glooServiceLoggerLock.DoubleCheckLock(ref _glooServiceLogger, () => ServiceConfig.GetMultiTargetLogger(CreateLog(ServiceInfo.ServiceName)));
        }
    }
}
