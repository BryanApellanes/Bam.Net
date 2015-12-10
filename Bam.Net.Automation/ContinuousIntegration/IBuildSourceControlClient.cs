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
    public interface IBuildSourceControlClient
    {
        string ServerSourcePath { get; set; }
        void GetLatest(string localDirectory);
        void GetTag(string tagName, string localDirectory);
        void SetTag(string tagName, string message);
        void Notify(string notificationRecieverIdentifier, string buildIdentifier, BuildJobResult buildResult, string message);
    }
}
