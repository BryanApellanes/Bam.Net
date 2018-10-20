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
    public class LogEventQuery: Query<LogEventColumns, LogEvent>
    { 
		public LogEventQuery(){}
		public LogEventQuery(WhereDelegate<LogEventColumns> where, OrderBy<LogEventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LogEventQuery(Func<LogEventColumns, QueryFilter<LogEventColumns>> where, OrderBy<LogEventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LogEventQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LogEventQuery Where(WhereDelegate<LogEventColumns> where)
        {
            return Where(where, null, null);
        }

        public static LogEventQuery Where(WhereDelegate<LogEventColumns> where, OrderBy<LogEventColumns> orderBy = null, Database db = null)
        {
            return new LogEventQuery(where, orderBy, db);
        }

		public LogEventCollection Execute()
		{
			return new LogEventCollection(this, true);
		}
    }
}