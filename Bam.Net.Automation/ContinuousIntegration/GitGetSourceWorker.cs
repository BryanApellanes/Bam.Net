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
    public class GitGetSourceWorker: GetSourceWorker
    {
        public GitGetSourceWorker()
            : base()
        {
            Init();
        }

        public GitGetSourceWorker(string name)
            : base(name)
        {
            Init();
        }

        private void Init()
        {
            this.SourceControlType = "Git";            
        }

        protected internal override void ConfigureClient()
        {
            GitClient client = new GitClient();
            client.UserEmail = UserEmail;
            client.UserName = UserName;
            client.ServerSourcePath = ServerSourcePath;
            SourceControlClient = client;
        }
    }
}
