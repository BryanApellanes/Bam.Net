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
    public class GroupPermissionQuery: Query<GroupPermissionColumns, GroupPermission>
    { 
		public GroupPermissionQuery(){}
		public GroupPermissionQuery(WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public GroupPermissionQuery(Func<GroupPermissionColumns, QueryFilter<GroupPermissionColumns>> where, OrderBy<GroupPermissionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public GroupPermissionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static GroupPermissionQuery Where(WhereDelegate<GroupPermissionColumns> where)
        {
            return Where(where, null, null);
        }

        public static GroupPermissionQuery Where(WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy = null, Database db = null)
        {
            return new GroupPermissionQuery(where, orderBy, db);
        }

		public GroupPermissionCollection Execute()
		{
			return new GroupPermissionCollection(this, true);
		}
    }
}