/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class UserNameQuery: Query<UserNameColumns, UserName>
    { 
		public UserNameQuery(){}
		public UserNameQuery(WhereDelegate<UserNameColumns> where, OrderBy<UserNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserNameQuery(Func<UserNameColumns, QueryFilter<UserNameColumns>> where, OrderBy<UserNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserNameQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserNameQuery Where(WhereDelegate<UserNameColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserNameQuery Where(WhereDelegate<UserNameColumns> where, OrderBy<UserNameColumns> orderBy = null, Database db = null)
        {
            return new UserNameQuery(where, orderBy, db);
        }

		public UserNameCollection Execute()
		{
			return new UserNameCollection(this, true);
		}
    }
}