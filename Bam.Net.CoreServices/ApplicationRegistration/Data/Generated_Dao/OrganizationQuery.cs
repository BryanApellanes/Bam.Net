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
    public class OrganizationQuery: Query<OrganizationColumns, Organization>
    { 
		public OrganizationQuery(){}
		public OrganizationQuery(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public OrganizationQuery(Func<OrganizationColumns, QueryFilter<OrganizationColumns>> where, OrderBy<OrganizationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public OrganizationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static OrganizationQuery Where(WhereDelegate<OrganizationColumns> where)
        {
            return Where(where, null, null);
        }

        public static OrganizationQuery Where(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null, Database db = null)
        {
            return new OrganizationQuery(where, orderBy, db);
        }

		public OrganizationCollection Execute()
		{
			return new OrganizationCollection(this, true);
		}
    }
}