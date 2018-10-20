/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class LogEventPagedQuery: PagedQuery<LogEventColumns, LogEvent>
    { 
		public LogEventPagedQuery(LogEventColumns orderByColumn, LogEventQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}