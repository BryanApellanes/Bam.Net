using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.IO;
using System.Reflection;
using Bam.Net.Configuration;

namespace Bam.Net.Server.Tvg
{
    public class GlooServer: SimpleServer<GlooResponder>
    {
        public GlooServer(BamConf conf, ILogger logger)
            : base(new GlooResponder(conf, logger), logger)
        {
            Responder.Initialize();
            this.CreatedOrChangedHandler = (o, fsea) =>
            {
                ReloadServices(fsea);
            };
            this.RenamedHandler = (o, rea) =>
            {                
                ReloadServices(GetDirectory(rea.FullPath));
            };
            this.ServiceTypes = new HashSet<Type>();
        }

        public override void Start()
        {
            this.ServiceTypes.Clear();
            base.Start();
        }
        protected HashSet<Type> ServiceTypes { get; private set; }
        private void ReloadServices(FileSystemEventArgs fsea)
        {
            string path = fsea.FullPath;
            DirectoryInfo directory = GetDirectory(path);
            if (directory != null)
            {
                ReloadServices(directory);
            }
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
            .GetAppSetting("SearchPattern")
            .Or("*Services.dll,*Proxyables.dll,*Gloo.dll")
            .DelimitSplit(",", "|").Each(searchPattern =>
            {
                FileInfo[] files = directory.GetFiles(searchPattern, SearchOption.AllDirectories);
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
            ServiceTypes.Each(serviceType =>
            {
                responder.RemoveCommonService(serviceType);
                responder.AddCommonService(serviceType, serviceType.Construct());
                Logger.AddEntry("Added service: {0}", serviceType.FullName);
            });
            return responder;
        }
    }
}
