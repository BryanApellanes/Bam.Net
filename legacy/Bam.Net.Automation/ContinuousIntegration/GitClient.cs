/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Automation.SourceControl;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public class GitClient : IBuildSourceControlClient
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string ServerSourcePath
        {
            get;
            set;
        }

        public void GetLatest(string localDirectory)
        {
            Git git = Git.Repository(ServerSourcePath)
                        .UserName(UserName)
                        .UserEmail(UserEmail)
                        .CloneTo(localDirectory);

            ProcessOutput output = git.LastOutput();
            if(output.TimedOut)
            {
                throw new GetSourceException("Git process timed out");
            }
            if (output.ExitCode > 0)
            {

            }
        }

        public void GetTag(string tagName, string localDirectory)
        {
            throw new NotImplementedException();
        }

        public void SetTag(string tagName, string message)
        {
            throw new NotImplementedException();
        }

        public void Notify(string notificationRecieverIdentifier, string buildIdentifier, BuildJobResult buildResult, string message)
        {
            throw new NotImplementedException();
        }
    }
}
