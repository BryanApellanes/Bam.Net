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
using Bam.Net.Configuration;
using Bam.Net.Automation.ContinuousIntegration.Loggers;
using B = Bam.Net.Logging;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public abstract class BuildWorker: Worker
    {
        public BuildWorker()
            : base()
        {
            Init();
        }

        public BuildWorker(string name)
            : base(name)
        {
            Init();
        }

        private void Init()
        {
            this.Logger = new CsvBuildLogger();
            this._results = new List<ProjectBuildResult>();
            this.BuildTarget = "Build";
        }

        public string SourceDirectory
        {
            get;
            set;
        }

        public string OutputDirectory
        {
            get;
            set;
        }

        public string BuildTarget
        {
            get;
            set;
        }

        string _loggerTypeName;
        public string LoggerTypeName
        {
            get
            {
                if (string.IsNullOrEmpty(_loggerTypeName) && Logger != null)
                {
                    _loggerTypeName = Logger.GetType().AssemblyQualifiedName;
                }

                return _loggerTypeName;
            }
            set
            {
                _loggerTypeName = value;
                Type type = Type.GetType(value);
                if (type == null)
                {
                    throw new InvalidOperationException("The specified LoggerTypeName ({0}) was not found"._Format(value));
                }

                Logger = (BuildLogger)type.Construct();
            }
        }

        ILogger _logger;
        public ILogger Logger
        {
            get
            {
                if (_logger == null && !string.IsNullOrEmpty(LoggerTypeName))
                {
                    _logger = Type.GetType(LoggerTypeName).Construct<ILogger>();
                }
                return _logger;
            }
            set
            {
                _logger = value;
                _loggerTypeName = value.GetType().AssemblyQualifiedName;
            }
        }

        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "Name", "SourceDirectory", "OutputDirectory", "LoggerTypeName" };
            }
        }

        public event EventHandler ResultAdded;

        protected void OnResultAdded(string path, BuildResult result)
        {
            if (ResultAdded != null)
            {
                ResultAdded(this, new BuildResultEventArgs(new ProjectBuildResult(path, result)));
            }
        }

        protected abstract FileInfo[] GetBuildFiles();

        List<ProjectBuildResult> _results;
        object _resultsLock = new object();
        public ProjectBuildResult[] BuildResults
        {
            get
            {
                return _resultsLock.DoubleCheckLock(ref _results, () => new List<ProjectBuildResult>()).ToArray();
            }
        }

        protected override WorkState Do()
        {
            B.ILogger threaded = Logger as B.ILogger;
            if(threaded != null)
            {
                threaded.RestartLoggingThread();
            }

            this.CheckRequiredProperties();

            FileInfo[] projectFiles = GetBuildFiles();

            StringBuilder message = new StringBuilder();

            projectFiles.Each(projectFile =>
            {
                message.AppendLine(Build(projectFile, _results));
            });

            WorkState<ProjectBuildResult[]> workState = new WorkState<ProjectBuildResult[]>(this, BuildResults);
            ProjectBuildResult[] failures = BuildResults.Where(br => br.BuildResult.OverallResult == BuildResultCode.Failure).ToArray();
            workState.Status = failures.Length == 0 ? Status.Succeeded: Status.Failed;
            workState.Message = message.ToString();
            return workState;
        }

        protected virtual string Build(FileInfo projectOrSolution, List<ProjectBuildResult> results)
        {
            StringBuilder message = new StringBuilder();
            DirectoryInfo outputDir = new DirectoryInfo(OutputDirectory);
            DirectoryInfo sourceDir = new DirectoryInfo(SourceDirectory);            
            string outputPath = Path.Combine(outputDir.FullName, Path.GetFileNameWithoutExtension(projectOrSolution.Name));
            
            BuildResult buildResult = projectOrSolution.Compile(outputPath, Logger, BuildTarget);
            lock (_resultsLock)
            {
                results.Add(new ProjectBuildResult(projectOrSolution.FullName, buildResult));
                OnResultAdded(projectOrSolution.FullName, buildResult);
            }

            if (buildResult.OverallResult != BuildResultCode.Success)
            {
                if (buildResult.Exception != null)
                {
                    message.AppendLine("{0} Build failed: {1}"._Format(projectOrSolution.Name, buildResult.Exception.Message));
                }
                else
                {
                    message.AppendLine("{0} Build failed"._Format(projectOrSolution.Name));
                }

                if (buildResult.ResultsByTarget != null)
                {
                    IDictionary<string, TargetResult> resultsByTarget = buildResult.ResultsByTarget;
                    resultsByTarget.Keys.Each(key =>
                    {
                        TargetResult result = resultsByTarget[key];
                        message.AppendFormat("Target:{0}::ResultCode:{1}\r\n", key, result.ResultCode.ToString());
                        message.AppendLine("- Items");
                        result.Items.Each(item =>
                        {
                            message.AppendFormat("\t{0}\r\n", item.ItemSpec);
                        });
                        if (result.Exception != null)
                        {
                            message.AppendFormat("Exception Message: {0}\r\n", result.Exception.Message);
                        }
                    });
                }
            }

            return message.ToString();
        }
    }
}
