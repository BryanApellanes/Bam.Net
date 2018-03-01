/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Configuration;

namespace Bam.Net.Automation
{
    /// <summary>
    /// Work done as a command line process
    /// </summary>
    public class ProcessWorker: Worker
    {
        public ProcessWorker() : base() { }
        public ProcessWorker(string name) : base(name) { }
        public ProcessWorker(string name, string commandLine)
            : base(name)
        {
            this.CommandLine = commandLine;
        }

        public string CommandLine { get; set; }

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name", "CommandLine" }; }
        }

        protected override WorkState Do(WorkState currentWorkState)
        {
            Args.ThrowIfNullOrEmpty(CommandLine, "CommandLine");

            ProcessOutput output = CommandLine.Run();
            WorkState<ProcessOutput> result = new WorkState<ProcessOutput>(this, output)
            {
                Message = string.Format("{0} exited with code {1}", CommandLine, output.ExitCode),
                PreviousWorkState = currentWorkState
            };
            return result;
        }
    }
}
