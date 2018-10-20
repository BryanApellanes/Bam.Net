using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BakeSettings
    {
        public BakeSettings()
        {
            MsBuild = "msbuild.exe";
            Config = "Release";
            Platform = "x64";
            TargetFrameworkVersion = "v4.7.2";
            AutoGenerateBindingRedirects = "true";
            GenerateDocumentation = "true";
            MaxCpuCount = 1;
        }
        public string MsBuild { get; set; }
        public string Nuget { get; set; }

        /// <summary>
        /// Gets or sets the packages directory.  The location of installed
        /// nuget packages.
        /// </summary>
        /// <value>
        /// The packages directory.
        /// </value>
        public string PackagesDirectory { get; set; }

        /// <summary>
        /// Gets or sets the global packages directory.
        /// </summary>
        /// <value>
        /// The global packages directory.
        /// </value>
        public string GlobalPackagesDirectory { get; set; }
        public string ProjectFile { get; set; }
        public string Config { get; set; } // Release or Debug

        public string Platform { get; set; } // x86 or x64

        public string TargetFrameworkVersion { get; set; } // v4.6.2     
        public string OutputPath { get; set; }

        string _autoGenerateBindingRedirects;
        public string AutoGenerateBindingRedirects
        {
            get { return _autoGenerateBindingRedirects.IsAffirmative() ? "true" : "false"; }
            set { _autoGenerateBindingRedirects = value; }
        }

        string _generateDocumentation;
        public string GenerateDocumentation
        {
            get { return _generateDocumentation.IsAffirmative() ? "true" : "false"; }
            set { _generateDocumentation = value; }
        }

        public int MaxCpuCount { get; set; }
    }
}
