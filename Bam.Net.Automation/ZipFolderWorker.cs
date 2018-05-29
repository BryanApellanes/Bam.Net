/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Configuration;
using System.IO;
using System.IO.Compression;

namespace Bam.Net.Automation
{
    public class ZipFolderWorker: Worker
    {
        public ZipFolderWorker() : base() { }
        public ZipFolderWorker(string name) : base(name) { }

        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "Name", "SourceDirectory", "TargetPath" };
            }
        }

        /// <summary>
        /// The directory to zip
        /// </summary>
        public string SourceDirectory { get; set; }

        /// <summary>
        /// The full path or job relative path to the
        /// final zip file including the desired extension
        /// </summary>
        public string TargetPath { get; set; }
        protected override WorkState Do(WorkState previousWorkState)
        {
            Validate.RequiredProperties(this);
            
            DirectoryInfo dir = new DirectoryInfo(SourceDirectory);
            ZipFile.CreateFromDirectory(dir.FullName, TargetPath);
            WorkState workstate = new WorkState(this, "Sucessfully zipped file to {0}"._Format(SourceDirectory));
            return workstate;
        }
    }
}
