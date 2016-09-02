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
    public class EventParamQuery: Query<EventParamColumns, EventParam>
    { 
		public EventParamQuery(){}
		public EventParamQuery(WhereDelegate<EventParamColumns> where, OrderBy<EventParamColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public EventParamQuery(Func<EventParamColumns, QueryFilter<EventParamColumns>> where, OrderBy<EventParamColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public EventParamQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static EventParamQuery Where(WhereDelegate<EventParamColumns> where)
        {
            return Where(where, null, null);
        }

        public static EventParamQuery Where(WhereDelegate<EventParamColumns> where, OrderBy<EventParamColumns> orderBy = null, Database db = null)
        {
            return new EventParamQuery(where, orderBy, db);
        }

		public EventParamCollection Execute()
		{
			return new EventParamCollection(this, true);
		}
    }
}