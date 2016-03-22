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
    public class RoleQuery: Query<RoleColumns, Role>
    { 
		public RoleQuery(){}
		public RoleQuery(WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public RoleQuery(Func<RoleColumns, QueryFilter<RoleColumns>> where, OrderBy<RoleColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public RoleQuery(Delegate where, Database db = null) : base(where, db) { }

		public RoleCollection Execute()
		{
			return new RoleCollection(this, true);
		}
    }
}