/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class OrganizationUserQuery: Query<OrganizationUserColumns, OrganizationUser>
    { 
		public OrganizationUserQuery(){}
		public OrganizationUserQuery(WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public OrganizationUserQuery(Func<OrganizationUserColumns, QueryFilter<OrganizationUserColumns>> where, OrderBy<OrganizationUserColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public OrganizationUserQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static OrganizationUserQuery Where(WhereDelegate<OrganizationUserColumns> where)
        {
            return Where(where, null, null);
        }

        public static OrganizationUserQuery Where(WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy = null, Database db = null)
        {
            return new OrganizationUserQuery(where, orderBy, db);
        }

		public OrganizationUserCollection Execute()
		{
			return new OrganizationUserCollection(this, true);
		}
    }
}