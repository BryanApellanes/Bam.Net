/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class UserQuery: Query<UserColumns, User>
    { 
		public UserQuery(){}
		public UserQuery(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserQuery(Func<UserColumns, QueryFilter<UserColumns>> where, OrderBy<UserColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserQuery Where(WhereDelegate<UserColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserQuery Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
        {
            return new UserQuery(where, orderBy, db);
        }

		public UserCollection Execute()
		{
			return new UserCollection(this, true);
		}
    }
}