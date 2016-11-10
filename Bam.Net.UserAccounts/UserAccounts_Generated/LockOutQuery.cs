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
    public class LockOutQuery: Query<LockOutColumns, LockOut>
    { 
		public LockOutQuery(){}
		public LockOutQuery(WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LockOutQuery(Func<LockOutColumns, QueryFilter<LockOutColumns>> where, OrderBy<LockOutColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LockOutQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LockOutQuery Where(WhereDelegate<LockOutColumns> where)
        {
            return Where(where, null, null);
        }

        public static LockOutQuery Where(WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy = null, Database db = null)
        {
            return new LockOutQuery(where, orderBy, db);
        }

		public LockOutCollection Execute()
		{
			return new LockOutCollection(this, true);
		}
    }
}