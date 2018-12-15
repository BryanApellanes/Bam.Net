using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BuildConfig
    {
        public string RepoRoot { get; set; }
        public string RepoName { get; set; }
        public string Branch { get; set; }
        public bool Clean { get; set; }
        public string BuildName { get; set; }
        public string ProjectFile { get; set; }
        public string MsBuildPath { get; set; }
        public string Config { get; set; }
        public string FrameworkVersion { get; set; }
        public string Platform { get; set; }
        public string BamBotRoot { get; set; }
        public string LegacyBuildScriptsPath { get; set; }
        public string PackageRestorePath { get; set; }
        public string RestoreReference { get; set; }
        public string Description { get; set; }
        public string OutputPath { get; set; }
        public int Code { get; set; }

        public string GetOutputPath(string root, string hash)
        {
            OutputPath = Path.Combine(root, Platform, FrameworkVersion, "Debug", $"_{hash}");
            return OutputPath;
        }
    }
}
