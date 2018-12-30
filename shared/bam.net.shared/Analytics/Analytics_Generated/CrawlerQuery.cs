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
    public class CrawlerQuery: Query<CrawlerColumns, Crawler>
    { 
		public CrawlerQuery(){}
		public CrawlerQuery(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CrawlerQuery(Func<CrawlerColumns, QueryFilter<CrawlerColumns>> where, OrderBy<CrawlerColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CrawlerQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CrawlerQuery Where(WhereDelegate<CrawlerColumns> where)
        {
            return Where(where, null, null);
        }

        public static CrawlerQuery Where(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy = null, Database db = null)
        {
            return new CrawlerQuery(where, orderBy, db);
        }

		public CrawlerCollection Execute()
		{
			return new CrawlerCollection(this, true);
		}
    }
}