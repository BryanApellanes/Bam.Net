using Bam.Net.Application;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Automation
{
    public class DaemonDeployer: Deployable
    {
        /// <summary>
        /// Gets or sets the local directory which is what is deployed.
        /// </summary>
        /// <value>
        /// The local directory.
        /// </value>
        public DirectoryInfo LocalDirectory { get; set; }
        public DaemonInfo DaemonInfo { get; set; }

        public override void Deploy()
        {
            Deploy(DaemonInfo);
        }

        public void Deploy(DaemonInfo daemonInfo)
        {
            Args.ThrowIf(string.IsNullOrEmpty(daemonInfo.Host), "Host not specified");
            Args.ThrowIf(string.IsNullOrEmpty(daemonInfo.Name), "Name not specified");

            string directoryPathOnRemote = Path.Combine(Paths.Sys, daemonInfo.Name);
            FileInfo daemonFile = new FileInfo(daemonInfo.FileName);



            throw new NotImplementedException();
        }
    }
}
