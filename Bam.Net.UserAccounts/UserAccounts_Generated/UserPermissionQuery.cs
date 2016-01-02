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
    public class UserPermissionQuery: Query<UserPermissionColumns, UserPermission>
    { 
		public UserPermissionQuery(){}
		public UserPermissionQuery(WhereDelegate<UserPermissionColumns> where, OrderBy<UserPermissionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserPermissionQuery(Func<UserPermissionColumns, QueryFilter<UserPermissionColumns>> where, OrderBy<UserPermissionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserPermissionQuery(Delegate where, Database db = null) : base(where, db) { }

		public UserPermissionCollection Execute()
		{
			return new UserPermissionCollection(this, true);
		}
    }
}