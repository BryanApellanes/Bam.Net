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
    public class UserBehaviorQuery: Query<UserBehaviorColumns, UserBehavior>
    { 
		public UserBehaviorQuery(){}
		public UserBehaviorQuery(WhereDelegate<UserBehaviorColumns> where, OrderBy<UserBehaviorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserBehaviorQuery(Func<UserBehaviorColumns, QueryFilter<UserBehaviorColumns>> where, OrderBy<UserBehaviorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserBehaviorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserBehaviorQuery Where(WhereDelegate<UserBehaviorColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserBehaviorQuery Where(WhereDelegate<UserBehaviorColumns> where, OrderBy<UserBehaviorColumns> orderBy = null, Database db = null)
        {
            return new UserBehaviorQuery(where, orderBy, db);
        }

		public UserBehaviorCollection Execute()
		{
			return new UserBehaviorCollection(this, true);
		}
    }
}