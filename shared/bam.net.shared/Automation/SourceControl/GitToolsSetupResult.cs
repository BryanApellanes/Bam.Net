/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.SourceControl
{
    internal class GitToolsSetupResult
    {
        public bool GitDirSet { get; set; }

        public bool EnvironmentPathSet { get; set; }
    }
}
