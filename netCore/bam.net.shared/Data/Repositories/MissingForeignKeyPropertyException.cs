/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using System.Runtime.Serialization;

namespace Bam.Net.Data.Repositories
{
	[Serializable]
	public class MissingForeignKeyPropertyException: Exception 
	{
		public MissingForeignKeyPropertyException() { }
		protected MissingForeignKeyPropertyException(SerializationInfo info, StreamingContext context)
			: base()
		{ }
		public MissingForeignKeyPropertyException(IEnumerable<string> missingProperties)
			: base("Missing Foreign Keys: \r\n\t{0}"._Format(missingProperties.ToArray().ToDelimited(s => s, "\r\n\t")))
		{ }
	}
}
