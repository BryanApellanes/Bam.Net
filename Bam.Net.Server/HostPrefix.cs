/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class HostPrefix
    {
        public HostPrefix()
        {
            this.HostName = "localhost";
            this.Port = 8080;
        }

        public HostPrefix(string hostName, int port)
        {
            this.HostName = hostName;
            this.Port = port;
        }

        public string HostName { get; set; }
        public int Port { get; set; }

        public bool Ssl { get; set; }

        public override string ToString()
        {
            string protocol = Ssl ? "https://": "http://";
            return string.Format("{0}{1}:{2}/", protocol, HostName, Port);
        }
    }
}
