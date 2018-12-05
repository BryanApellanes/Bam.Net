using Bam.Net.Data;
using Bam.Net.Logging;
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
    [Serializable]
    public class ProxyAssemblyGeneratorService : AsyncProxyableService
    {
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

        public IDataDirectoryProvider DataDirectoryProvider { get; set; }
        public ProxyFactory ProxyFactory { get; set; }
        public ProxyAssemblyGenerator ProxyAssemblyGenerator { get; set; }

        public ServiceResponse GetBase64ProxyAssembly(string nameSpace, string typeName)
        {
            Type type = GetType(nameSpace, typeName);
            if(type == null)
            {
                return new ServiceResponse { Success = false, Message = $"Specified type {nameSpace}.{typeName} not found." };
            }
            Assembly proxyAssembly = ProxyFactory.GetAssembly(type);
            FileInfo assemblyFileInfo = proxyAssembly.GetFileInfo();
            byte[] bytes = File.ReadAllBytes(assemblyFileInfo.FullName);
            return new ServiceResponse { Success = true, Data = bytes.ToBase64() };
        }

        public ServiceResponse GetProxyCode(string nameSpace, string typeName)
        {
            Type type = GetType(nameSpace, typeName);
            if (type == null)
            {
                return new ServiceResponse { Success = false, Message = $"Specified type {nameSpace}.{typeName} not found." };
            }
            return new ServiceResponse { Success = true, Data = ProxyAssemblyGenerator.GetSource() };
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
                foreach(Type type in ass.GetTypes())
                {
                    _assemblies.AddMissing($"{type.Namespace}.{type.Name}", ass);
                }
            }
        }

        private Type GetType(string nameSpace, string typeName)
        {
            Assembly assembly = GetRealAssembly(nameSpace, typeName);
            if (assembly == null)
            {
                return null;
            }
            return assembly
                .GetTypes()
                .FirstOrDefault(t => t.Namespace.Equals(nameSpace, StringComparison.InvariantCultureIgnoreCase) && t.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase));            
        }
    }
}
