/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class CookieDataPagedQuery: PagedQuery<CookieDataColumns, CookieData>
    { 
		public CookieDataPagedQuery(CookieDataColumns orderByColumn, CookieDataQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}