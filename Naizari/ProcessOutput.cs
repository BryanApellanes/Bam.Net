/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari
{
    public class ProcessOutput
    {
        public ProcessOutput()
        {
            this.ExitCode = -1;
            this.StandardError = string.Empty;
            this.StandardOutput = string.Empty;
        }

        public ProcessOutput(string output, string errorOutput, int exitCode)
        {
            this.StandardError = errorOutput;
            this.StandardOutput = output;
            this.ExitCode = exitCode;
        }

        public int ExitCode { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
    }
}
