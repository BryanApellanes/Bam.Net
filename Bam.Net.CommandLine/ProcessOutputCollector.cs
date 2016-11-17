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
    public class ProcessOutputCollector
    {
        public ProcessOutputCollector(Action<string> dataHandler = null, Action<string> errorHandler = null)
        {
            StandardOutput = new StringBuilder();
            StandardError = new StringBuilder();
            DataHandler = dataHandler ?? ((s) => { StandardOutput.Append(s); });
            ErrorHandler = errorHandler ?? ((e) => { StandardError.Append(e); });
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
