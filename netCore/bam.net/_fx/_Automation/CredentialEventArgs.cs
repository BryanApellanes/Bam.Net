using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;

namespace Bam.Net.Automation
{
    public class CredentialEventArgs : EventArgs
    {
        public string HostName { get; set; }
        public string ServiceName { get; set; }
        public bool Found { get; set; }
    }
}
