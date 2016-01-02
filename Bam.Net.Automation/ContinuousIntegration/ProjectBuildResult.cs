/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.BuildEngine;
using Microsoft.Build;
using Microsoft.Build.Framework;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public class ProjectBuildResult
    {
        public ProjectBuildResult(string projectPath, BuildResult buildResult)
        {
            this.ProjectPath = projectPath;
            this.BuildResult = buildResult;
        }

        public string ProjectPath { get; set; }
        public BuildResult BuildResult { get; set; }
    }
}
