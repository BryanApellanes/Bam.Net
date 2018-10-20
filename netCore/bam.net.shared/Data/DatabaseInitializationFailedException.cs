/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bam.Net.Data
{
	[Serializable]
	public class DatabaseInitializationFailedException: Exception
	{
		public DatabaseInitializationFailedException() : base() { }
		public DatabaseInitializationFailedException(string connectionName)
			: base("Failed to initialize database: {0}"._Format(connectionName))
		{ }
		public DatabaseInitializationFailedException(SerializationInfo info, StreamingContext context) { }

		public DatabaseInitializationFailedException(List<string> exceptionMessages)
			: base(string.Format("Failed to initialize database:\r\n{0}", exceptionMessages.ToArray().ToDelimited(s => s, "\r\n")))
		{ }
		
	}
}
