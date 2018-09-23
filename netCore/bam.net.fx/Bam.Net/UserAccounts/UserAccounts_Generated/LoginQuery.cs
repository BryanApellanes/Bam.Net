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
    public class LoginQuery: Query<LoginColumns, Login>
    { 
		public LoginQuery(){}
		public LoginQuery(WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LoginQuery(Func<LoginColumns, QueryFilter<LoginColumns>> where, OrderBy<LoginColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LoginQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LoginQuery Where(WhereDelegate<LoginColumns> where)
        {
            return Where(where, null, null);
        }

        public static LoginQuery Where(WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy = null, Database db = null)
        {
            return new LoginQuery(where, orderBy, db);
        }

		public LoginCollection Execute()
		{
			return new LoginCollection(this, true);
		}
    }
}