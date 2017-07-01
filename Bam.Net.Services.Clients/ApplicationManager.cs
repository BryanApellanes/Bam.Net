using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Logging;

namespace Bam.Net.Services.Clients
{
    public class ApplicationManager
    {
        public ApplicationManager(string organizationName, string applicationName, string coreHostName, int corePort = 80, string workingDirectory = null, ILogger logger = null)
        {
            _coreClientInitializer = Task.Run(() => new CoreClient(organizationName, applicationName, coreHostName, corePort, workingDirectory, logger));
        }

        public ApplicationManager(string coreHostName, int corePort = 80, string workingDirectory = null, ILogger logger = null): this(DefaultConfigurationOrganizationNameProvider.Instance.GetOrganizationName(), DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName(), coreHostName, corePort, workingDirectory, logger)
        {        
        }

        Task<CoreClient> _coreClientInitializer;
        public CoreClient CoreClient
        {
            get
            {
                return _coreClientInitializer.Result;
            }
        }
    }
}
