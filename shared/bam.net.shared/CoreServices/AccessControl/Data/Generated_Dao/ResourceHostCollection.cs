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
    public class ResourceHostCollection: DaoCollection<ResourceHostColumns, ResourceHost>
    { 
		public ResourceHostCollection(){}
		public ResourceHostCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ResourceHostCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ResourceHostCollection(Query<ResourceHostColumns, ResourceHost> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ResourceHostCollection(Database db, Query<ResourceHostColumns, ResourceHost> q, bool load) : base(db, q, load) { }
		public ResourceHostCollection(Query<ResourceHostColumns, ResourceHost> q, bool load) : base(q, load) { }
    }
}