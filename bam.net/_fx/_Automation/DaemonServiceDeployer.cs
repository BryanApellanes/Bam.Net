using Bam.Net.Application;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Automation
{
    /// <summary>
    /// Class providing automation hooks to the bamd.exe process.  Bamd
    /// monitors other executable daemons ensuring they stay up.
    /// </summary>
    public class DaemonServiceDeployer : WindowsServiceDeployer
    {        
        public DaemonServiceDeployer(DaemonServiceInfo svcInfo)
        {
            ServiceInfo = new WindowsServiceInfo
            {
                FileName = "bamd.exe",
                AppSettings = svcInfo.ToDictionary()
            };
        }
    }
}
