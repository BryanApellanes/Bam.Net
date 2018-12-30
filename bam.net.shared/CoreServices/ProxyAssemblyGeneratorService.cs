using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    [Proxy("proxyGen")]
    [Serializable]
    [Encrypt]
    [ServiceSubdomain("proxyGen")]
    public class ProxyAssemblyGeneratorService : AsyncProxyableService
    {
        protected ProxyAssemblyGeneratorService() { }

        Dictionary<string, Assembly> _assemblies;
        public ProxyAssemblyGeneratorService(IDataDirectoryProvider dataDirectoryProvider, ILogger logger)
        {
            DataDirectoryProvider = dataDirectoryProvider;
            Logger = logger;
            _assemblies = new Dictionary<string, Assembly>();
            ProxyFactory = new ProxyFactory(SystemPaths.Current.Generated, logger, CoreServiceRegistryContainer.GetServiceRegistry());
            ProxyAssemblyGenerator = new ProxyAssemblyGenerator(new ProxySettings { DownloadClient = false, Host = "heart.bamapps.net", Port = 80 }, ProxyFactory.WorkspaceDirectory, Logger);
            LoadAssemblies();
        }

        static object _defaultLock = new object();
        static ProxyAssemblyGeneratorService _default;
        public static ProxyAssemblyGeneratorService Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new ProxyAssemblyGeneratorServiceProxy(ConfigurationResolverServiceUrlProvider.Instance.GetServiceUrl<ProxyAssemblyGeneratorService>()));
            }
        }

        public IDataDirectoryProvider DataDirectoryProvider { get; set; }
        public ProxyFactory ProxyFactory { get; set; }
        public ProxyAssemblyGenerator ProxyAssemblyGenerator { get; set; }

        public virtual Services.ServiceResponse GetBase64ProxyAssembly(string nameSpace, string typeName)
        {
            Type type = GetType(nameSpace, typeName);
            if(type == null)
            {
                return new Services.ServiceResponse { Success = false, Message = $"Specified type {nameSpace}.{typeName} not registered for generation." };
            }
            Assembly proxyAssembly = ProxyFactory.GetAssembly(type);
            FileInfo assemblyFileInfo = proxyAssembly.GetFileInfo();
            byte[] bytes = File.ReadAllBytes(assemblyFileInfo.FullName);
            return new Services.ServiceResponse { Success = true, Data = bytes.ToBase64() };
        }

        public virtual Services.ServiceResponse GetProxyCode(string nameSpace, string typeName)
        {
            Type type = GetType(nameSpace, typeName);
            if (type == null)
            {
                return new Services.ServiceResponse { Success = false, Message = $"Specified type {nameSpace}.{typeName} not found." };
            }
            return new Services.ServiceResponse { Success = true, Data = ProxyAssemblyGenerator.GetSource() };
        }
        
        protected Assembly GetRealAssembly(string nameSpace, string typeName)
        {
            string key = $"{nameSpace}.{typeName}";
            if (_assemblies.ContainsKey(key))
            {
                return _assemblies[key];
            }
            return null;
        }

        public override object Clone()
        {
            ProxyAssemblyGeneratorService clone = new ProxyAssemblyGeneratorService(DataDirectoryProvider, Logger);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        private void LoadAssemblies()
        {
            foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in ass.GetTypes())
                    {
                        _assemblies.AddMissing($"{type.Namespace}.{type.Name}", ass);
                    }
                }
                catch(Exception ex)
                {
                    Logger.AddEntry("Exception loading assemblies from {0}: {1}", ex, ass.GetFileInfo()?.FullName, ex.Message);
                }
            }
        }

        private Type GetType(string nameSpace, string typeName)
        {
            try
            {
                Assembly assembly = GetRealAssembly(nameSpace, typeName);
                if (assembly == null)
                {
                    return null;
                }
                return assembly
                    .GetTypes()
                    .FirstOrDefault(t => !string.IsNullOrEmpty(t.Namespace) && t.Namespace.Equals(nameSpace, StringComparison.InvariantCultureIgnoreCase) && t.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error getting type {0}.{1}: {2}", ex, nameSpace, typeName, ex.Message);
                return null;
            }
        }
    }
}
