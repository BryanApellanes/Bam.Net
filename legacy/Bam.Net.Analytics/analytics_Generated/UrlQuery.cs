/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class UrlQuery: Query<UrlColumns, Url>
    { 
		public UrlQuery(){}
		public UrlQuery(WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UrlQuery(Func<UrlColumns, QueryFilter<UrlColumns>> where, OrderBy<UrlColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UrlQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UrlQuery Where(WhereDelegate<UrlColumns> where)
        {
            return Where(where, null, null);
        }

        public static UrlQuery Where(WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy = null, Database db = null)
        {
            return new UrlQuery(where, orderBy, db);
        }

		public UrlCollection Execute()
		{
			return new UrlCollection(this, true);
		}
    }
}