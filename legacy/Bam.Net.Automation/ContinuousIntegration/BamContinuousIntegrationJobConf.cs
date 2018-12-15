/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Automation.ContinuousIntegration.Loggers;
using Bam.Net.Configuration;
using Microsoft.Build.Framework;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public class BamContinuousIntegrationJobConf: ContinuousIntegrationJobConf, IHasRequiredProperties
    {
        public BamContinuousIntegrationJobConf(string name = "GitContinuousIntegration")
            : base(name)
        {
            JobDirectory = new DirectoryInfo(name).FullName;
            InitializeWorkers();
        }

        protected override void InitializeWorkers()
        {
            this.GetSource = GetWorker<GitGetSourceWorker>("{0}_GetSource"._Format(Name));
            this.Build = GetWorker<AllProjectsBuildWorker>("{0}_Build"._Format(Name));
            this.RunTests = GetWorker<BamTestRunProcessWorker>("{0}_RunTests"._Format(Name));
            this.DeployToTest = GetWorker<FtpDeploymentWorker>("{0}_DeployToTest"._Format(Name));
            this.SendNotification = GetWorker<EmailWorker>("{0}_SendNotification"._Format(Name));
            this.SuspendJob = GetWorker<SuspendJobWorker>("{0}_SuspendJob"._Format(Name));
            this.ProductionTransform = GetWorker<XmlTransformWorker>("{0}_ProductionTransform"._Format(Name));
            this.DeployToProduction = GetWorker<FtpDeploymentWorker>("{0}_DeployToProduction"._Format(Name));
            this.Archive = GetWorker<ZipFolderWorker>("{0}_Archive"._Format(Name));
        }

        public string[] RequiredProperties
        {
            get
            {
                return new string[] { 
                    "GetSourceLocalDirectory", 
                    "GetSourceUserName", 
                    "GetSourceUserEmail",
                    "GetSourceServerSourcePath",
                    "BuildSourceDirectory",
                    "BuildOutputDirectory",
                };
            }
        }
        //GetSource.Name
        //GetSource.LocalDirectory
        public string GetSourceLocalDirectory
        {
            get
            {
                return GetSource.LocalDirectory;
            }
            set
            {
                GetSource.LocalDirectory = value;
                SaveWorker(GetSource);
            }
        }
        //GetSource.UserName
        public string GetSourceUserName
        {
            get
            {
                return GetSource.UserName;
            }
            set
            {
                GetSource.UserName = value;
                SaveWorker(GetSource);
            }
        }
        //GetSource.UserEmail
        public string GetSourceUserEmail
        {
            get
            {
                return GetSource.UserEmail;
            }
            set
            {
                GetSource.UserEmail = value;
                SaveWorker(GetSource);
            }
        }
        //GetSource.ServerSourcePath
        public string GetSourceServerSourcePath
        {
            get
            {
                return GetSource.ServerSourcePath;
            }
            set
            {
                GetSource.ServerSourcePath = value;
                SaveWorker(GetSource);
            }
        }
        //Build.Name
        //Build.SourceDirectory
        public string BuildSourceDirectory
        {
            get
            {
                return Build.SourceDirectory;
            }
            set
            {
                Build.SourceDirectory = value;
                SaveWorker(Build);
            }
        }
        //Build.OutputDirectory
        public string BuildOutputDirectory
        {
            get
            {
                return Build.OutputDirectory;
            }
            set
            {
                Build.OutputDirectory = value;                
                SaveWorker(Build);
            }
        }

        public ILogger BuildLogger
        {
            get
            {
                return Build.Logger;
            }
            set
            {
                Build.Logger = value;
                SaveWorker(Build);
            }
        }

      

    }
}
