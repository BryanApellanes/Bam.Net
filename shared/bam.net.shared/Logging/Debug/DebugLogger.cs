/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Bam.Net.Logging.Debug
{
	public class DebugLogger: Logger
	{
		public DebugLogger()
		{
			this.MessageFormat = "{EventId}::{Category}::{User}::{Time}::{Computer}::{Severity}::\r\n{Message}";
		}		

		public string MessageFormat
		{
			get;
			set;
		}

		public override void CommitLogEvent(LogEvent logEvent)
		{
			System.Diagnostics.Debug.WriteLine(MessageFormat.NamedFormat(logEvent));
		}
	}
}
