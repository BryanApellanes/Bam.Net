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
    public class TfsClient: IBuildSourceControlClient
    {

        public string ServerSourcePath
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void GetLatest(string localDirectory)
        {
            throw new NotImplementedException();
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
