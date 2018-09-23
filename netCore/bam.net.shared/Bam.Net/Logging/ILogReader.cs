/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
	public interface ILogReader
	{
		ILogger Logger { get; set; }
		List<LogEntry> GetLogEntries(DateTime from, DateTime to);
	}
}
