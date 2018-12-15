/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Net;

namespace Bam.Net.Net
{
	public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);

	public class PingCompletedEventArgs
	{
		#region Properties

		private PingResponse pingResponse;
		private DateTime endDateTime;

		public PingResponse PingResponse
		{
			get { return pingResponse; }
		}

		public DateTime EndDateTime
		{
			get { return endDateTime; }
		}
		
		#endregion

		public PingCompletedEventArgs(PingResponse pingResponse, DateTime endDateTime)
		{
			this.pingResponse = pingResponse;
			this.endDateTime = endDateTime;
		}
	}
}
