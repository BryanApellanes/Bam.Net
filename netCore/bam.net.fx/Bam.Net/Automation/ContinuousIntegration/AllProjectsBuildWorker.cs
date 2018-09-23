/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build;
using Microsoft.Build.Framework;
using System.IO;
using Bam.Net.Configuration;
using Bam.Net.Automation.ContinuousIntegration.Loggers;

namespace Bam.Net.Automation.ContinuousIntegration
{
    /// <summary>
    /// A Worker that will attempt to build all the projects 
    /// that it finds in the specified SourceDirectory.
    /// A recursive search will be done for all .csproj 
    /// and .vbproj files.
    /// </summary>
    public class AllProjectsBuildWorker: BuildWorker
    {
        public AllProjectsBuildWorker() : base() { }
        public AllProjectsBuildWorker(string name) : base(name) { }

        protected override WorkState Do(WorkState currentWorkState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the solution files to build
        /// </summary>
        /// <returns></returns>
        protected override FileInfo[] GetBuildFiles()
        {
            List<FileInfo> results = new List<FileInfo>();
            DirectoryInfo sourceDir = new DirectoryInfo(SourceDirectory);
			if (sourceDir.Exists)
			{
				results.AddRange(sourceDir.GetFiles("*.csproj", SearchOption.AllDirectories));
				results.AddRange(sourceDir.GetFiles("*.vbproj", SearchOption.AllDirectories));
			}

            return results.ToArray();
        }

    }
}
