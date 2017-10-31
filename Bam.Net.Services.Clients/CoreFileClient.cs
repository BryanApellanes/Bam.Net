using Bam.Net.CoreServices;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    public class CoreFileClient
    {
        CoreFileService _fileService;
        ProxyFactory _proxyFactory;
        public CoreFileClient(string hostName, int port, ILogger logger = null)
        {
            _proxyFactory = new ProxyFactory(logger);
            _fileService = _proxyFactory.GetProxy<CoreFileService>(hostName, port);
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
