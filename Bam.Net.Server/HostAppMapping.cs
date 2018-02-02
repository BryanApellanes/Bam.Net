using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class HostAppMapping
    {
        /// <summary>
        /// Gets or sets the host.  This equates to the Host
        /// property of a Uri.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the AppName that the Host should be mapped to.
        /// </summary>
        public string AppName { get; set; }

        public override int GetHashCode()
        {
            return this.GetHashCode("Host", "AppName");
        }

        public override bool Equals(object obj)
        {
            if(obj is HostAppMapping hostMapping)
            {
                return hostMapping.Host.Equals(Host) && hostMapping.AppName.Equals(AppName);
            }
            return false;
        }
    }
}
