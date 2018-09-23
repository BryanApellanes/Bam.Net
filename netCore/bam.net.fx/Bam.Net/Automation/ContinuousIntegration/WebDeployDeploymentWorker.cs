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
    public class WebDeployDeploymentWorker: DeploymentWorker
    {
        public WebDeployDeploymentWorker() : base() { }
        public WebDeployDeploymentWorker(string name) : base(name) { }



        protected override WorkState Do()
        {
            throw new NotImplementedException();
        }
    }
}
