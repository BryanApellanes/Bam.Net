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
    public class HeaderDataQuery: Query<HeaderDataColumns, HeaderData>
    { 
		public HeaderDataQuery(){}
		public HeaderDataQuery(WhereDelegate<HeaderDataColumns> where, OrderBy<HeaderDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HeaderDataQuery(Func<HeaderDataColumns, QueryFilter<HeaderDataColumns>> where, OrderBy<HeaderDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HeaderDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static HeaderDataQuery Where(WhereDelegate<HeaderDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static HeaderDataQuery Where(WhereDelegate<HeaderDataColumns> where, OrderBy<HeaderDataColumns> orderBy = null, Database db = null)
        {
            return new HeaderDataQuery(where, orderBy, db);
        }

		public HeaderDataCollection Execute()
		{
			return new HeaderDataCollection(this, true);
		}
    }
}