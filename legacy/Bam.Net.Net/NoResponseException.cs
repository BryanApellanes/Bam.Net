/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Runtime.Serialization;

namespace Bam.Net.Net.Dns
{
	/// <summary>
	/// Thrown when the server does not respond
	/// </summary>
	[Serializable]
	public class NoResponseException : SystemException
	{
		public NoResponseException()
		{
			// no implementation
		}

		public NoResponseException(Exception innerException) :  base(null, innerException) 
		{
			// no implementation
		}

		public NoResponseException(string message, Exception innerException) : base (message, innerException)
		{
			// no implementation
		}
        
		protected NoResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// no implementation
		}
	}
}