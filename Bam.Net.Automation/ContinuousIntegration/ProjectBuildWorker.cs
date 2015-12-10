/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Automation.ContinuousIntegration
{
    /// <summary>
    /// A Worker that will attempt to build only the specified
    /// ProjectFileName.  A recursive search will be done
    /// to find it, it may be either a .csproj file, .vbproj file or
    /// .sln file.
    /// </summary>
    public class ProjectBuildWorker: BuildWorker
    {
        public ProjectBuildWorker() : base() { }
        public ProjectBuildWorker(string name) : base(name) { }

        public string ProjectFileName { get; set; }

        protected override FileInfo[] GetBuildFiles()
        {
            DirectoryInfo sourceDir = new DirectoryInfo(SourceDirectory);
			InvalidOperationException invalidOperationException = new InvalidOperationException("The specified ProjectFileName ({0}) was not found (additional searches for {0}.csproj and {0}.vbproj were also done with no results)"._Format(ProjectFileName));

			if (!sourceDir.Exists)
			{
				throw invalidOperationException;
			}

            FileInfo[] results = sourceDir.GetFiles(ProjectFileName, SearchOption.AllDirectories);            

            if (results.Length == 0)
            {
                results = sourceDir.GetFiles("{0}.csproj"._Format(ProjectFileName), SearchOption.AllDirectories);
            }

            if (results.Length == 0)
            {
                results = sourceDir.GetFiles("{0}.vbproj"._Format(ProjectFileName), SearchOption.AllDirectories);
            }

            if (results.Length == 0)
            {
				throw invalidOperationException;
            }

            return results;
        }

        public override string[] RequiredProperties
        {
            get
            {
                List<string> requiredProperties = new List<string>();
                requiredProperties.AddRange(base.RequiredProperties);
                requiredProperties.Add("ProjectFileName");
                return requiredProperties.ToArray();
            }
        }
    }
}
