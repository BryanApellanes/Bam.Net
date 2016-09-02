/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class UserOrganizationQuery: Query<UserOrganizationColumns, UserOrganization>
    { 
		public UserOrganizationQuery(){}
		public UserOrganizationQuery(WhereDelegate<UserOrganizationColumns> where, OrderBy<UserOrganizationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserOrganizationQuery(Func<UserOrganizationColumns, QueryFilter<UserOrganizationColumns>> where, OrderBy<UserOrganizationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserOrganizationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserOrganizationQuery Where(WhereDelegate<UserOrganizationColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserOrganizationQuery Where(WhereDelegate<UserOrganizationColumns> where, OrderBy<UserOrganizationColumns> orderBy = null, Database db = null)
        {
            return new UserOrganizationQuery(where, orderBy, db);
        }

		public UserOrganizationCollection Execute()
		{
			return new UserOrganizationCollection(this, true);
		}
    }
}