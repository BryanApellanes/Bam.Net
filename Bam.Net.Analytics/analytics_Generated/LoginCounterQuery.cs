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
    public class LoginCounterQuery: Query<LoginCounterColumns, LoginCounter>
    { 
		public LoginCounterQuery(){}
		public LoginCounterQuery(WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LoginCounterQuery(Func<LoginCounterColumns, QueryFilter<LoginCounterColumns>> where, OrderBy<LoginCounterColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LoginCounterQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LoginCounterQuery Where(WhereDelegate<LoginCounterColumns> where)
        {
            return Where(where, null, null);
        }

        public static LoginCounterQuery Where(WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy = null, Database db = null)
        {
            return new LoginCounterQuery(where, orderBy, db);
        }

		public LoginCounterCollection Execute()
		{
			return new LoginCounterCollection(this, true);
		}
    }
}