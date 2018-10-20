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
using Bam.Net.Configuration;

namespace Bam.Net.Automation
{
    public class DirectoryCopyWorker: Worker
    {
        public DirectoryCopyWorker() : base() { }
        public DirectoryCopyWorker(string name) : base(name) { }

        public string Source { get; set; }
        public string Destination { get; set; }

        protected override WorkState Do(WorkState currentWorkState)
        {        
            string jobName = this.Job != null ? this.Job.Name : "null";

            DirectoryInfo src = new DirectoryInfo(Source);
            ThrowIfDirectoryNotFound(jobName, src);

            DirectoryInfo dst = new DirectoryInfo(Destination);
            ThrowIfDirectoryNotFound(jobName, dst);

            src.Copy(dst);

            return new WorkState(this, "Directory {0} copied successfully to {1}"._Format(src.FullName, dst.FullName));
        }


        private void ThrowIfDirectoryNotFound(string jobName, DirectoryInfo src)
        {
            if (!src.Exists)
            {
                throw new DirectoryNotFoundException("Job:{0},DirectoryCopyWork:{1}::Directory {2} not found."._Format(jobName, this.Name, src.FullName));
            }
        }



        #region IHasRequiredProperties Members

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name", "Source", "Destination"}; }
        }

        #endregion
    }
}
