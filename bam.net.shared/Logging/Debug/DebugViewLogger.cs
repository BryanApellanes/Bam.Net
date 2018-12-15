/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.Runtime.InteropServices;

namespace Bam.Net.Logging.Debug
{
	public class DebugViewLogger: DebugLogger
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern void OutputDebugString(string message);

		public DebugViewLogger() : base() { }

		public override void CommitLogEvent(LogEvent logEvent)
		{
			OutputDebugString(MessageFormat.NamedFormat(logEvent));
		}
	}
}
