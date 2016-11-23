/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class OrganizationUserCollection: DaoCollection<OrganizationUserColumns, OrganizationUser>
    { 
		public OrganizationUserCollection(){}
		public OrganizationUserCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public OrganizationUserCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public OrganizationUserCollection(Query<OrganizationUserColumns, OrganizationUser> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public OrganizationUserCollection(Database db, Query<OrganizationUserColumns, OrganizationUser> q, bool load) : base(db, q, load) { }
		public OrganizationUserCollection(Query<OrganizationUserColumns, OrganizationUser> q, bool load) : base(q, load) { }
    }
}