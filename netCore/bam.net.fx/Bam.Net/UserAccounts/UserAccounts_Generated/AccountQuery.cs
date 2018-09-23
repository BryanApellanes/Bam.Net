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
    public class AccountQuery: Query<AccountColumns, Account>
    { 
		public AccountQuery(){}
		public AccountQuery(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public AccountQuery(Func<AccountColumns, QueryFilter<AccountColumns>> where, OrderBy<AccountColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public AccountQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static AccountQuery Where(WhereDelegate<AccountColumns> where)
        {
            return Where(where, null, null);
        }

        public static AccountQuery Where(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy = null, Database db = null)
        {
            return new AccountQuery(where, orderBy, db);
        }

		public AccountCollection Execute()
		{
			return new AccountCollection(this, true);
		}
    }
}