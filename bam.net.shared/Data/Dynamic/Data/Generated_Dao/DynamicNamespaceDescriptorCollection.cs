/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DynamicNamespaceDescriptorCollection: DaoCollection<DynamicNamespaceDescriptorColumns, DynamicNamespaceDescriptor>
    { 
		public DynamicNamespaceDescriptorCollection(){}
		public DynamicNamespaceDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DynamicNamespaceDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DynamicNamespaceDescriptorCollection(Query<DynamicNamespaceDescriptorColumns, DynamicNamespaceDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DynamicNamespaceDescriptorCollection(Database db, Query<DynamicNamespaceDescriptorColumns, DynamicNamespaceDescriptor> q, bool load) : base(db, q, load) { }
		public DynamicNamespaceDescriptorCollection(Query<DynamicNamespaceDescriptorColumns, DynamicNamespaceDescriptor> q, bool load) : base(q, load) { }
    }
}