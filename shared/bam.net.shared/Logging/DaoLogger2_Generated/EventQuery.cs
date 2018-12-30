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
    public class EventQuery: Query<EventColumns, Event>
    { 
		public EventQuery(){}
		public EventQuery(WhereDelegate<EventColumns> where, OrderBy<EventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public EventQuery(Func<EventColumns, QueryFilter<EventColumns>> where, OrderBy<EventColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public EventQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static EventQuery Where(WhereDelegate<EventColumns> where)
        {
            return Where(where, null, null);
        }

        public static EventQuery Where(WhereDelegate<EventColumns> where, OrderBy<EventColumns> orderBy = null, Database db = null)
        {
            return new EventQuery(where, orderBy, db);
        }

		public EventCollection Execute()
		{
			return new EventCollection(this, true);
		}
    }
}