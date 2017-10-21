/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class ObjectDescriptorCollection: DaoCollection<ObjectDescriptorColumns, ObjectDescriptor>
    { 
		public ObjectDescriptorCollection(){}
		public ObjectDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ObjectDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ObjectDescriptorCollection(Query<ObjectDescriptorColumns, ObjectDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ObjectDescriptorCollection(Database db, Query<ObjectDescriptorColumns, ObjectDescriptor> q, bool load) : base(db, q, load) { }
		public ObjectDescriptorCollection(Query<ObjectDescriptorColumns, ObjectDescriptor> q, bool load) : base(q, load) { }
    }
}