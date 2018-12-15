/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class SessionStateQuery: Query<SessionStateColumns, SessionState>
    { 
		public SessionStateQuery(){}
		public SessionStateQuery(WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SessionStateQuery(Func<SessionStateColumns, QueryFilter<SessionStateColumns>> where, OrderBy<SessionStateColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SessionStateQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SessionStateQuery Where(WhereDelegate<SessionStateColumns> where)
        {
            return Where(where, null, null);
        }

        public static SessionStateQuery Where(WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy = null, Database db = null)
        {
            return new SessionStateQuery(where, orderBy, db);
        }

		public SessionStateCollection Execute()
		{
			return new SessionStateCollection(this, true);
		}
    }
}