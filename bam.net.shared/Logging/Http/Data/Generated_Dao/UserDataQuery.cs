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
    public class UserDataQuery: Query<UserDataColumns, UserData>
    { 
		public UserDataQuery(){}
		public UserDataQuery(WhereDelegate<UserDataColumns> where, OrderBy<UserDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserDataQuery(Func<UserDataColumns, QueryFilter<UserDataColumns>> where, OrderBy<UserDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserDataQuery Where(WhereDelegate<UserDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserDataQuery Where(WhereDelegate<UserDataColumns> where, OrderBy<UserDataColumns> orderBy = null, Database db = null)
        {
            return new UserDataQuery(where, orderBy, db);
        }

		public UserDataCollection Execute()
		{
			return new UserDataCollection(this, true);
		}
    }
}