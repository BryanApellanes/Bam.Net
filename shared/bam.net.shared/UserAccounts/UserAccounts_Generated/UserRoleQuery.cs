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
    public class UserRoleQuery: Query<UserRoleColumns, UserRole>
    { 
		public UserRoleQuery(){}
		public UserRoleQuery(WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserRoleQuery(Func<UserRoleColumns, QueryFilter<UserRoleColumns>> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserRoleQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserRoleQuery Where(WhereDelegate<UserRoleColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserRoleQuery Where(WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null)
        {
            return new UserRoleQuery(where, orderBy, db);
        }

		public UserRoleCollection Execute()
		{
			return new UserRoleCollection(this, true);
		}
    }
}