using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration;

namespace Bam.Net.Services.Clients
{
    public class ApplicationCore
    {
        public ApplicationCore(): this(DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName())
        {

        }
        public ApplicationCore(string applicationName):this(Organization.Public.Name, applicationName)
        {

        }
        public ApplicationCore(string organizationName, string applicationName)
        {
            string hostName = "bamapps.net";
            int port = 80;

            CoreClient = new CoreClient(organizationName, applicationName, hostName, port, new CoreLoggerClient(hostName, port));
        }

        public CoreClient CoreClient { get; set; }
    }
}
