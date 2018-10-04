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
    public class FileSystemFileCopyWorker: FileCopyWorker
    {
        public FileSystemFileCopyWorker() : base() { }
        public FileSystemFileCopyWorker(string name) : base(name) { }

        protected override WorkState Do(WorkState currentWorkState)
        {        
            string jobName = this.Job != null ? this.Job.Name : "null";

            FileInfo src = new FileInfo(Source);
            ThrowIfFileNotFound(jobName, src);

            FileInfo dst = new FileInfo(Destination);

            File.Copy(src.FullName, dst.FullName);

            return new WorkState(this, "File {0} copied successfully to {1}"._Format(src.FullName, dst.FullName)) { PreviousWorkState = currentWorkState };
        }

        private void ThrowIfFileNotFound(string jobName, FileInfo src)
        {
            if (!src.Exists)
            {
                throw new FileNotFoundException("Job:{0},FileCopyWork:{1}::File {2} not found."._Format(jobName, this.Name, src.FullName));
            }
        }


    }
}
