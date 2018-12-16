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
    public partial class ProxyAssemblyGenerator // core
    {
        public GeneratedAssemblyInfo GetAssembly()
        {
            return GenerateAssembly();
        }

        static Dictionary<Type, GeneratedAssemblyInfo> _generatedAssemblyInfos = new Dictionary<Type, GeneratedAssemblyInfo>();

        public GeneratedAssemblyInfo GenerateAssembly()
        {
            if (_generatedAssemblyInfos.ContainsKey(ServiceType))
            {
                return _generatedAssemblyInfos[ServiceType];
            }
            ProxyAssemblyGeneratorService genSvc = ProxyAssemblyGeneratorService.Default;
            ServiceResponse response = genSvc.GetBase64ProxyAssembly(ServiceType.Namespace, ServiceType.Name);
            if (!response.Success)
            {
                throw new ApplicationException(response.Message);
            }
            // write bytes to temp
            byte[] assembly = response.Data.ToString().FromBase64();
            string path = Path.Combine(SystemPaths.Current.Generated, $"{ServiceType.Name}_{ServiceSettings.Protocol}_{ServiceSettings.Host}_{ServiceSettings.Port}_proxy.dll");
            File.WriteAllBytes(path, assembly);
            // load the assembly from the file
            GeneratedAssemblyInfo info = new GeneratedAssemblyInfo(Assembly.LoadFile(path));
            _generatedAssemblyInfos[ServiceType] = info;
            return info;
        }
    }
}
