using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using Bam.Net.Services.Clients;

namespace Bam.Net.Application
{
    public class GlooServer: SimpleServer<GlooResponder>
    {
        public GlooServer(BamConf conf, ILogger logger, bool verbose = false)
            : base(new GlooResponder(conf, logger, verbose), logger)
        {
            Responder.Initialize();
            CreatedOrChangedHandler = (o, fsea) =>
            {
                ReloadServices(fsea);
            };
            RenamedHandler = (o, rea) =>
            {
                DirectoryInfo dir = GetDirectory(rea.FullPath);
                if (dir != null)
                {
                    TryReloadServices(dir);
                }
            };
            ServiceTypes = new HashSet<Type>();
        }

        public override void Start()
        {
            if(MonitorDirectories.Length > 0)
            {
                ServiceTypes.Clear();
                MonitorDirectories.Each(new { Server = this }, (ctx, dir) =>
                {
                    ctx.Server.TryReloadServices(new DirectoryInfo(dir));
                });
            }
            else
            {
                RegisterServiceTypes();
            }
            base.Start();
        }
        object _serviceTypeLock = new object();
        public HashSet<Type> ServiceTypes { get; private set; }
        protected ServiceProxyResponder RegisterServiceTypes(IEnumerable<Type> serviceTypes)
        {
            lock (_serviceTypeLock)
            {
                ServiceTypes.Clear();
                serviceTypes.Each(st => ServiceTypes.Add(st));
            }
            return RegisterServiceTypes();
        }
        protected ServiceProxyResponder RegisterServiceTypes()
        {
            GlooResponder gloo = Responder;
            ServiceProxyResponder responder = gloo.ServiceProxyResponder;
            AddCommonServices(responder);
            gloo.RpcResponder.Executors = responder.CommonServiceProvider;
            return responder;
        }

        private void AddCommonServices(ServiceProxyResponder responder)
        {
            ServiceTypes.Each(new {  Logger, Responder = responder }, (ctx, serviceType) =>
            {
                ctx.Responder.RemoveCommonService(serviceType);
                ctx.Responder.AddCommonService(serviceType, ServiceRegistry.GetServiceLoader(serviceType));
                ctx.Logger.AddEntry("Added service: {0}", serviceType.FullName);
            });
            IApiKeyResolver apiKeyResolver = (IApiKeyResolver)ServiceRegistry.GetServiceLoader(typeof(IApiKeyResolver), new CoreClient())();
            responder.CommonSecureChannel.ApiKeyResolver = apiKeyResolver;
            responder.AppSecureChannels.Values.Each(sc => sc.ApiKeyResolver = apiKeyResolver);
        }

        Timer reloadDelay;
        private void ReloadServices(FileSystemEventArgs fsea)
        {
            if(reloadDelay != null)
            {
                reloadDelay.Stop();
                reloadDelay.Dispose();
            }

            reloadDelay = new Timer(3000);
            reloadDelay.Elapsed += (o, args) =>
            {
                string path = fsea.FullPath;

                DirectoryInfo directory = GetDirectory(path);
                if (directory != null)
                {
                    TryReloadServices(directory);
                }
            };
            reloadDelay.AutoReset = false;
            reloadDelay.Enabled = true;            
        }

        private static DirectoryInfo GetDirectory(string path)
        {
            DirectoryInfo directory = null;
            if (File.Exists(path))
            {
                directory = new FileInfo(path).Directory;
            }
            else if (Directory.Exists(path))
            {
                directory = new DirectoryInfo(path);
            }
            return directory;
        }

        private void TryReloadServices(DirectoryInfo directory)
        {
            try
            {
                List<string> excludeNamespaces = new List<string>();
                excludeNamespaces.AddRange(DefaultConfiguration.GetAppSetting("ExcludeNamespaces").DelimitSplit(",", "|"));
                List<string> excludeClasses = new List<string>();
                excludeClasses.AddRange(DefaultConfiguration.GetAppSetting("ExcludeClasses").DelimitSplit(",", "|"));

                lock (_serviceTypeLock)
                {
                    DefaultConfiguration
                    .GetAppSetting("AssemblySearchPattern")
                    .Or("*Services.dll,*Proxyables.dll,*Gloo.dll")
                    .DelimitSplit(",", "|")
                    .Each(new { Directory = directory, ExcludeNamespaces = excludeNamespaces, ExcludeClasses = excludeClasses },
                    (ctx, searchPattern) =>
                    {
                        FileInfo[] files = ctx.Directory.GetFiles(searchPattern, SearchOption.AllDirectories);
                        foreach (FileInfo file in files)
                        {
                            try
                            {
                                Assembly toLoad = Assembly.LoadFrom(file.FullName);
                                Type[] types = toLoad.GetTypes().Where(type => !ctx.ExcludeNamespaces.Contains(type.Namespace) &&
                                        !ctx.ExcludeClasses.Contains(type.Name) &&
                                        type.HasCustomAttributeOfType<ProxyAttribute>()).ToArray();
                                foreach (Type t in types)
                                {
                                    ServiceTypes.Add(t);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.AddEntry("An exception occurred loading services from file {0}: {1}", LogEventType.Warning, ex, file.FullName, ex.Message);
                            }
                        }
                    });
                }
                RegisterServiceTypes();
            }
            catch (Exception ex)
            {
                Logger.AddEntry("An exception occurred loading services: {0}", ex, ex.Message);
            }
        }                        
    }
}
