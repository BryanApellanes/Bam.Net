/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class UserQuery: Query<UserColumns, User>
    { 
		public UserQuery(){}
		public UserQuery(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserQuery(Func<UserColumns, QueryFilter<UserColumns>> where, OrderBy<UserColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserQuery(Delegate where, Database db = null) : base(where, db) { }

		public UserCollection Execute()
		{
			return new UserCollection(this, true);
		}
    }
}