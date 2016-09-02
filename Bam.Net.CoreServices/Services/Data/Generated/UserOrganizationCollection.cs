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
    public class UserOrganizationCollection: DaoCollection<UserOrganizationColumns, UserOrganization>
    { 
		public UserOrganizationCollection(){}
		public UserOrganizationCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserOrganizationCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserOrganizationCollection(Query<UserOrganizationColumns, UserOrganization> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserOrganizationCollection(Database db, Query<UserOrganizationColumns, UserOrganization> q, bool load) : base(db, q, load) { }
		public UserOrganizationCollection(Query<UserOrganizationColumns, UserOrganization> q, bool load) : base(q, load) { }
    }
}