/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;

namespace Bam.Net.Automation.SourceControl
{
    internal class GitConfigStack
    {
        public GitConfigStack()
        {
            this.CredentialHelper = "winstore";
            this.GitBinPath = "C:\\Program Files (x86)\\Git\\bin";
        }

        public string Repository { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CredentialHelper { get; set; }

        public string GitBinPath { get; set; }

        public ProcessOutput LastOutput { get; set; }
    }
}
