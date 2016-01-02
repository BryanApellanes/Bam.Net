/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Execution;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public class BuildResultEventArgs: EventArgs
    {
        public BuildResultEventArgs(ProjectBuildResult result)
        {
            this.ProjectBuildResult = result;
        }

        public ProjectBuildResult ProjectBuildResult { get; set; }
    }
}
