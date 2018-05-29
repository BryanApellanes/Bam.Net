/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CommandLine
{
    /// <summary>
    /// Handles standard and error output from a process.
    /// </summary>
    public class ProcessOutputCollector
    {
        public ProcessOutputCollector(StringBuilder output, StringBuilder error)
        {
            StandardOutput = output;
            StandardError = error;
            DataHandler = (s) => StandardOutput.AppendLine(s);
            ErrorHandler = (e) => StandardError.AppendLine(e);
            ExitCode = -100;
        }
        public ProcessOutputCollector(Action<string> dataHandler = null, Action<string> errorHandler = null)
        {
            StandardOutput = new StringBuilder();
            StandardError = new StringBuilder();
            DataHandler = (s) =>
            {
                StandardOutput.AppendLine(s);
                dataHandler?.Invoke(s);
            };            
            ErrorHandler = (e) =>
            {
                StandardError.AppendLine(e);
                errorHandler?.Invoke(e);
            };
            ExitCode = -100;
        }

        public StringBuilder StandardOutput { get; private set; }
        public StringBuilder StandardError { get; private set; }
        public Action<string> DataHandler { get; private set; }
        public Action<string> ErrorHandler { get; private set; }

        public int ExitCode { get; set; }
        public override string ToString()
        {
            return StandardOutput.ToString();
        }
    }
}
