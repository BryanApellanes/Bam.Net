/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Net;

namespace Bam.Net.Net
{
	public delegate void PingStartedEventHandler(object sender, PingStartedEventArgs e);

	public class PingStartedEventArgs
	{
		private IPEndPoint serverEndPoint;
		public IPEndPoint ServerEndPoint
		{
			get { return serverEndPoint; }
		}

		private DateTime startDateTime;
		public DateTime StartDateTime
		{
			get { return startDateTime; }
		}

		private int byteCount;
		public int ByteCount
		{
			get { return byteCount; }
		}

		public PingStartedEventArgs(IPEndPoint serverEndPoint, int byteCount, DateTime startDateTime)
		{
			this.serverEndPoint = serverEndPoint;
			this.startDateTime = startDateTime;
			this.byteCount = byteCount;
		}
	}
}
