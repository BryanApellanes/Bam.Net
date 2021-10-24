/*
	Copyright © Bryan Apellanes 2015  
*/

using System.IO;
using Bam.Net.Services;

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
                    _jobsDirectory = new DirectoryInfo(Path.Combine(Workspace.ForApplication().Root.FullName, "Jobs"));
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
