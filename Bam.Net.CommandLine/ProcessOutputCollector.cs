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
		public ProcessOutputCollector(Action<string> dataHandler, Action<string> errorHandler)
		{
			this.StandardOutput = new StringBuilder();
			this.StandardError = new StringBuilder();
			this.DataHandler = dataHandler;
			this.ErrorHandler = errorHandler;
			this.ExitCode = -100;
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
