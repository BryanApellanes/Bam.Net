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
	public abstract class LogReader: ILogReader
	{
		#region ILogReader Members

		public ILogger Logger
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public List<LogEntry> GetLogEntries(DateTime from, DateTime to)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
