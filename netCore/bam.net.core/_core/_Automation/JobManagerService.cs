/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Profiguration;
using Bam.Net.ServiceProxy;
using Bam.Net.Data;
using Bam.Net.Analytics;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using System.Threading;
using Bam.Net.Services;
using Bam.Net.Automation;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Dynamic;

namespace Bam.Net.Automation
{
    /// <summary>
    /// The manager for all jobs.
    /// </summary>
    public partial class JobManagerService: AsyncProxyableService
    {
        DirectoryInfo _jobsDirectory;
        public string JobsDirectory
        {
            get
            {
                if (_jobsDirectory == null)
                {
                    _jobsDirectory = new DirectoryInfo("{0}\\Jobs"._Format(RuntimeSettings.AppDataFolder));
                }

                return _jobsDirectory.FullName;
            }
            set
            {
                _jobsDirectory = new DirectoryInfo(value);
            }
        }
    }
}
