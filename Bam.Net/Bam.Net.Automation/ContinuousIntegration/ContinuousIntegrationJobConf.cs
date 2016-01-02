/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public abstract class ContinuousIntegrationJobConf: JobConf
    {
        private const string DefaultWorkerDirectory = ".\\ContinuousIntegration";

        public ContinuousIntegrationJobConf()
            : base()
        {
            this.JobDirectory = DefaultWorkerDirectory;
        }

        public ContinuousIntegrationJobConf(string name)
            : base(name)
        {
            this.JobDirectory = DefaultWorkerDirectory;
        }

        protected abstract void InitializeWorkers();
        
        GetSourceWorker _getSourceWorker;        
        // Get latest
        protected internal GetSourceWorker GetSource
        {
            get
            {
                return _getSourceWorker;
            }
            set
            {
                _getSourceWorker = value;
            }
        }

        BuildWorker _buildWorker;
        // Build
        protected internal BuildWorker Build
        {
            get
            {
                return _buildWorker;
            }
            set
            {
                _buildWorker = value;
            }
        }

        protected internal ITestWorker RunTests
        {
            get;
            set;
        }

        protected internal DeploymentWorker DeployToTest
        {
            get;
            set;
        }

        protected internal NotificationWorker SendNotification
        {
            get;
            set;
        }

        protected internal SuspendJobWorker SuspendJob
        {
            get;
            set;
        }

        protected internal XmlTransformWorker ProductionTransform
        {
            get;
            set;
        }

        protected internal DeploymentWorker DeployToProduction
        {
            get;
            set;
        }

        protected internal ZipFolderWorker Archive
        {
            get;
            set;
        }
    }
}
