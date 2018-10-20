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
    public class PermissionQuery: Query<PermissionColumns, Permission>
    { 
		public PermissionQuery(){}
		public PermissionQuery(WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PermissionQuery(Func<PermissionColumns, QueryFilter<PermissionColumns>> where, OrderBy<PermissionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PermissionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PermissionQuery Where(WhereDelegate<PermissionColumns> where)
        {
            return Where(where, null, null);
        }

        public static PermissionQuery Where(WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy = null, Database db = null)
        {
            return new PermissionQuery(where, orderBy, db);
        }

		public PermissionCollection Execute()
		{
			return new PermissionCollection(this, true);
		}
    }
}