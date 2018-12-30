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
    public class CookieDataQuery: Query<CookieDataColumns, CookieData>
    { 
		public CookieDataQuery(){}
		public CookieDataQuery(WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CookieDataQuery(Func<CookieDataColumns, QueryFilter<CookieDataColumns>> where, OrderBy<CookieDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CookieDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CookieDataQuery Where(WhereDelegate<CookieDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static CookieDataQuery Where(WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy = null, Database db = null)
        {
            return new CookieDataQuery(where, orderBy, db);
        }

		public CookieDataCollection Execute()
		{
			return new CookieDataCollection(this, true);
		}
    }
}