using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.IO;
using System.Reflection;
using Bam.Net.Configuration;
using System.Timers;

namespace Bam.Net.Server.Tvg
{
    public class GlooServer: SimpleServer<GlooResponder>
    {
        public GlooServer(BamConf conf, ILogger logger)
            : base(new GlooResponder(conf, logger), logger)
        {
            Responder.Initialize();
            CreatedOrChangedHandler = (o, fsea) =>
            {
                ReloadServices(fsea);
            };
            RenamedHandler = (o, rea) =>
            {                
                ReloadServices(GetDirectory(rea.FullPath));
            };
            ServiceTypes = new HashSet<Type>();
        }

        public override void Start()
        {
            ServiceTypes.Clear();
            MonitorDirectories.Each(new { Server = this }, (ctx, dir) =>
            {
                ctx.Server.ReloadServices(new DirectoryInfo(dir));
            });
            base.Start();
        }
        protected HashSet<Type> ServiceTypes { get; private set; }
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
                    ReloadServices(directory);
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

        private void ReloadServices(DirectoryInfo directory)
        {
            DefaultConfiguration
            .GetAppSetting("AssemblySearchPattern")
            .Or("*Services.dll,*Proxyables.dll,*Gloo.dll")
            .DelimitSplit(",", "|").Each(new { Directory = directory }, (ctx, searchPattern) =>
            {
                FileInfo[] files = ctx.Directory.GetFiles(searchPattern, SearchOption.AllDirectories);
                files.Each(file =>
                {
                    try
                    {
                        Assembly.LoadFrom(file.FullName)
                        .GetTypes()
                        .Where(type => type.HasCustomAttributeOfType<ProxyAttribute>())
                        .Each(serviceType =>
                        {
                            ServiceTypes.Add(serviceType);                                
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.AddEntry("An exception occurred loading services from file {0}: {1}", LogEventType.Warning, ex, file.FullName, ex.Message);
                    }
                });
            });
            RegisterProxiedClasses();
        }

        private ServiceProxyResponder RegisterProxiedClasses()
        {
            GlooResponder gloo = (GlooResponder)Responder;
            ServiceProxyResponder responder = gloo.ServiceProxyResponder;
            ServiceTypes.Each(new { Logger = Logger, Responder = responder }, (ctx, serviceType) =>
            {                
                ctx.Responder.RemoveCommonService(serviceType);
                ctx.Responder.AddCommonService(serviceType, GetServiceRetriever(serviceType));
                ctx.Logger.AddEntry("Added service: {0}", serviceType.FullName);
            });
            return responder;
        }

        private Func<object> GetServiceRetriever(Type type)
        {
            Type glooRegistryContainer = type.Assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<GlooContainerAttribute>()).FirstOrDefault();
            if(glooRegistryContainer != null)
            {
                MethodInfo provider = glooRegistryContainer.GetMethods().Where(mi => mi.HasCustomAttributeOfType<GlooRegistryProviderAttribute>() || mi.Name.Equals("Get")).FirstOrDefault();
                GlooRegistry reg = (GlooRegistry)provider.Invoke(null, null);
                return () => reg.Get(type);
            }
            return () => type.Construct();
        }                
    }
}
