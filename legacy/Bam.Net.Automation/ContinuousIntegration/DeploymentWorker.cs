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
    public abstract class DeploymentWorker: Worker
    {
        public DeploymentWorker() : base() { }
        public DeploymentWorker(string name) : base(name) { }

        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "ProjectNames", "WorkingDirectory", "ServerName" };
            }
        }
        /// <summary>
        /// A comma and/or semi-color separated list of project names
        /// </summary>
        public string ProjectNames
        {
            get;
            set;
        }

        public string WorkingDirectory
        {
            get;
            set;
        }

        //Deploy.UserName
        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ServerName
        {
            get;
            set;
        }
    }
}
