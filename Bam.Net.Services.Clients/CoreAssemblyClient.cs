using Bam.Net.CoreServices;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    public class CoreAssemblyClient
    {
        CoreAssemblyService _fileService;
        ProxyFactory _proxyFactory;
        public CoreAssemblyClient(ILogger logger = null) : this("bamapps.net", 80, logger)
        { }

        public CoreAssemblyClient(string hostName, int port, ILogger logger = null)
        {
            _proxyFactory = new ProxyFactory(logger);
            _fileService = _proxyFactory.GetProxy<CoreAssemblyService>(hostName, port);
        }

        static CoreAssemblyClient()
        {
            Resolver.AssemblyResolver = (rea) =>
            {
                // get the assembly descriptor for the requesting assembly
                // 
                return null;
            };
        }
        // Resolver.AssemblyResolver
        //
        // resolve assembly passes the assembly.fullname
        // use that to ask the AssemblyService.AssemblyManagementRepository for the 
        // AssemblyDescriptor by fullname
        // then AssemblyService.ResolveAssembly by assemblyDescriptor.Name
        // read the assembly bytes and send them in response Resolver.AssemblyResolver

    }
}
