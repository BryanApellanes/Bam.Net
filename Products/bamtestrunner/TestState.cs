/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.Runtime.Serialization;
using Bam.Net.Logging;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing
{
	[Serializable]
    public class TestState
    {
		public TestState()
		{
			this.Logger = new ConsoleLogger { UseColors = true };
		}
		public TestState(SerializationInfo info, StreamingContext context) { }
		public TestState(ILogger logger)
		{
			this.Logger = logger;
		}

		protected ILogger Logger { get; set; }

		public bool ExceptionOccurred { get; set; }
		public bool ExitOnFailure { get; set; }

		public void Info(string message)
		{
			Logger.AddEntry(message);
		}

		public void Error(string message, Exception ex)
		{
			Logger.AddEntry(message, ex);
		}
    }
}
