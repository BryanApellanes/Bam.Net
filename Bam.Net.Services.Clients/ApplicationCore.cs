using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;

namespace Bam.Net.Services.Clients
{
    public class ApplicationCore
    {
        public ApplicationCore()
        {
            string hostName = "bamapps.net";
            int port = 80;

            CoreClient = new CoreClient(hostName, port, new CoreLoggerClient(hostName, port));
        }

        public CoreClient CoreClient { get; set; }
    }
}
