/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class PermissionSpecificationQuery: Query<PermissionSpecificationColumns, PermissionSpecification>
    { 
		public PermissionSpecificationQuery(){}
		public PermissionSpecificationQuery(WhereDelegate<PermissionSpecificationColumns> where, OrderBy<PermissionSpecificationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PermissionSpecificationQuery(Func<PermissionSpecificationColumns, QueryFilter<PermissionSpecificationColumns>> where, OrderBy<PermissionSpecificationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PermissionSpecificationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PermissionSpecificationQuery Where(WhereDelegate<PermissionSpecificationColumns> where)
        {
            return Where(where, null, null);
        }

        public static PermissionSpecificationQuery Where(WhereDelegate<PermissionSpecificationColumns> where, OrderBy<PermissionSpecificationColumns> orderBy = null, Database db = null)
        {
            return new PermissionSpecificationQuery(where, orderBy, db);
        }

		public PermissionSpecificationCollection Execute()
		{
			return new PermissionSpecificationCollection(this, true);
		}
    }
}