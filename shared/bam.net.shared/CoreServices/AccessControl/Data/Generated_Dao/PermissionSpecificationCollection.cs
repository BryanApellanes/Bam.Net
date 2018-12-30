/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class PermissionSpecificationCollection: DaoCollection<PermissionSpecificationColumns, PermissionSpecification>
    { 
		public PermissionSpecificationCollection(){}
		public PermissionSpecificationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PermissionSpecificationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PermissionSpecificationCollection(Query<PermissionSpecificationColumns, PermissionSpecification> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PermissionSpecificationCollection(Database db, Query<PermissionSpecificationColumns, PermissionSpecification> q, bool load) : base(db, q, load) { }
		public PermissionSpecificationCollection(Query<PermissionSpecificationColumns, PermissionSpecification> q, bool load) : base(q, load) { }
    }
}