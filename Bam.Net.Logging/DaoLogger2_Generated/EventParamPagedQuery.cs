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
    public class EventParamPagedQuery: PagedQuery<EventParamColumns, EventParam>
    { 
		public EventParamPagedQuery(EventParamColumns orderByColumn, EventParamQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}