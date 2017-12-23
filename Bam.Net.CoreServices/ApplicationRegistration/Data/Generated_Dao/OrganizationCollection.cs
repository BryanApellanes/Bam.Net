/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class OrganizationCollection: DaoCollection<OrganizationColumns, Organization>
    { 
		public OrganizationCollection(){}
		public OrganizationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public OrganizationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public OrganizationCollection(Query<OrganizationColumns, Organization> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public OrganizationCollection(Database db, Query<OrganizationColumns, Organization> q, bool load) : base(db, q, load) { }
		public OrganizationCollection(Query<OrganizationColumns, Organization> q, bool load) : base(q, load) { }
    }
}