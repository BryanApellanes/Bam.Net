/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.CommandLine
{
    public class ProcessOutput
    {
        public ProcessOutput()
        {
            this.ExitCode = -1;
            this.StandardError = string.Empty;
            this.StandardOutput = string.Empty;
        }

        public ProcessOutput(string output, string errorOutput, int exitCode, bool timedOut)
        {
            this.TimedOut = timedOut;
            this.StandardError = errorOutput;
            this.StandardOutput = output;
            this.ExitCode = exitCode;
        }

        public bool TimedOut { get; set; }

        public int ExitCode { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
    }
}
