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
using Bam.Net.Configuration;

namespace Bam.Net.Automation.SourceControl
{
    internal class GitConfigStack
    {
        public GitConfigStack()
        {
            this.CredentialHelper = "winstore";
            this.GitPath = DefaultConfiguration.GetAppSetting("GitPath",  "C:\\Program Files\\Git\\bin");
        }

        public string RemoteRepository { get; set; }
        public string LocalRepository { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CredentialHelper { get; set; }

        public string GitPath { get; set; }

        public ProcessOutput LastOutput { get; set; }

        static GitConfigStack _default;
        static object _defaultLock = new object();
        public static GitConfigStack Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new GitConfigStack());
            }
        }
    }
}
