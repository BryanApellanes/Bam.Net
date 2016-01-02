/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;

namespace Bam.Net.Documentation.Tests
{
	[Serializable]
	public class ConsoleActions : CommandLineTestInterface
	{
		// See the below for examples of ConsoleActions and UnitTests

		#region ConsoleAction examples
		[ConsoleAction("Non Static Test")]
		public void NonStatic()
		{
			Pass("Test passed");
		}

		#endregion
	}
}