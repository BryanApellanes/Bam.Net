/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Net
{
	public delegate void PingErrorEventHandler(object sender, PingErrorEventArgs e);

	public class PingErrorEventArgs
	{
		#region Properties

		private PingResponseType errorType;
		private DateTime errorDateTime;
		private string message;

		public PingResponseType ErrorType
		{
			get { return errorType; }
		}

		public DateTime ErrorDateTime
		{
			get { return errorDateTime; }
		}

		public string Message
		{
			get { return message; }
		}

		#endregion

		public PingErrorEventArgs(PingResponseType errorType, string message, DateTime errorDateTime)
		{
			this.errorType = errorType;
			this.message = message;
			this.errorDateTime = errorDateTime;
		}
	}
}
