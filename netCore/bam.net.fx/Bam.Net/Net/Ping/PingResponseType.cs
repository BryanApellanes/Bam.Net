/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Net
{
	public enum PingResponseType
	{
		Ok = 0,
		CouldNotResolveHost,
		RequestTimedOut,
		ConnectionError,
		InternalError,
		Canceled
	}
}
