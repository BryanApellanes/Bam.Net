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
    public class UserHashDataQuery: Query<UserHashDataColumns, UserHashData>
    { 
		public UserHashDataQuery(){}
		public UserHashDataQuery(WhereDelegate<UserHashDataColumns> where, OrderBy<UserHashDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserHashDataQuery(Func<UserHashDataColumns, QueryFilter<UserHashDataColumns>> where, OrderBy<UserHashDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserHashDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserHashDataQuery Where(WhereDelegate<UserHashDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserHashDataQuery Where(WhereDelegate<UserHashDataColumns> where, OrderBy<UserHashDataColumns> orderBy = null, Database db = null)
        {
            return new UserHashDataQuery(where, orderBy, db);
        }

		public UserHashDataCollection Execute()
		{
			return new UserHashDataCollection(this, true);
		}
    }
}