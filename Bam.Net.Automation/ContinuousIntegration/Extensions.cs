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
using System.IO;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public static class Extensions
    {
        public static BuildResult Compile(this FileInfo solutionOrProject, string outputPath, ILogger logger = null)
        {
            Dictionary<string, string> properties = GetBuildDefaultProperties(outputPath);
            return Compile(solutionOrProject, properties, logger);
        }

        public static BuildResult Compile(this FileInfo solutionOrProject, string outputPath, ILogger logger = null, params string[] targets)
        {
            Dictionary<string, string> properties = GetBuildDefaultProperties(outputPath);
            return Compile(solutionOrProject, properties, logger, targets);
        }

        public static BuildResult Compile(this FileInfo solutionOrProject, Dictionary<string, string> buildRequestProperties = null, ILogger logger = null)
        {
            return Compile(solutionOrProject, buildRequestProperties, logger, "Build");
        }

        public static BuildResult Compile(this FileInfo solutionOrProject, Dictionary<string, string> buildRequestProperties = null, ILogger logger = null, params string[] targets)
        {
            if (!solutionOrProject.Exists)
            {
                throw new FileNotFoundException("The specified project or solution file ({0}) was not found"._Format(solutionOrProject.FullName));
            }

            if (buildRequestProperties == null)
            {
                buildRequestProperties = GetDefaultProperties();
            }

            ProjectCollection collection = new ProjectCollection();
            BuildParameters buildParameters = new BuildParameters(collection);
            buildParameters.Loggers = new[] { logger };
            BuildRequestData requestData = new BuildRequestData(solutionOrProject.FullName, buildRequestProperties, null, targets, null);

            return BuildManager.DefaultBuildManager.Build(buildParameters, requestData);
        }

        private static Dictionary<string, string> GetBuildDefaultProperties(string outputPath)
        {
            Dictionary<string, string> properties = GetDefaultProperties();
            properties["OutputPath"] = outputPath;
            properties["GenerateDocumentation"] = "true";
            return properties;
        }

        private static Dictionary<string, string> GetDefaultProperties()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Configuration", "Debug");
            result.Add("Platform", "Any CPU");
            return result;
        }
    }
}
