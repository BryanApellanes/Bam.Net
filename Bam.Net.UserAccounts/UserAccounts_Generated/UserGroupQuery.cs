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
    public class UserGroupQuery: Query<UserGroupColumns, UserGroup>
    { 
		public UserGroupQuery(){}
		public UserGroupQuery(WhereDelegate<UserGroupColumns> where, OrderBy<UserGroupColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserGroupQuery(Func<UserGroupColumns, QueryFilter<UserGroupColumns>> where, OrderBy<UserGroupColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserGroupQuery(Delegate where, Database db = null) : base(where, db) { }

		public UserGroupCollection Execute()
		{
			return new UserGroupCollection(this, true);
		}
    }
}