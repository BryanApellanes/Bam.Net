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
    public class ResourceCollection: DaoCollection<ResourceColumns, Resource>
    { 
		public ResourceCollection(){}
		public ResourceCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ResourceCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ResourceCollection(Query<ResourceColumns, Resource> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ResourceCollection(Database db, Query<ResourceColumns, Resource> q, bool load) : base(db, q, load) { }
		public ResourceCollection(Query<ResourceColumns, Resource> q, bool load) : base(q, load) { }
    }
}