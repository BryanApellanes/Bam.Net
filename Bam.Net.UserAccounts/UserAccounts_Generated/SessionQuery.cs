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
    public class SessionQuery: Query<SessionColumns, Session>
    { 
		public SessionQuery(){}
		public SessionQuery(WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SessionQuery(Func<SessionColumns, QueryFilter<SessionColumns>> where, OrderBy<SessionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SessionQuery(Delegate where, Database db = null) : base(where, db) { }

		public SessionCollection Execute()
		{
			return new SessionCollection(this, true);
		}
    }
}