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
    public class UriDataQuery: Query<UriDataColumns, UriData>
    { 
		public UriDataQuery(){}
		public UriDataQuery(WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UriDataQuery(Func<UriDataColumns, QueryFilter<UriDataColumns>> where, OrderBy<UriDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UriDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UriDataQuery Where(WhereDelegate<UriDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static UriDataQuery Where(WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy = null, Database db = null)
        {
            return new UriDataQuery(where, orderBy, db);
        }

		public UriDataCollection Execute()
		{
			return new UriDataCollection(this, true);
		}
    }
}