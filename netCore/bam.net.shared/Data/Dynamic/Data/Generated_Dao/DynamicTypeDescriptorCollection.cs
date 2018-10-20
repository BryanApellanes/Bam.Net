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
    public class DynamicTypeDescriptorCollection: DaoCollection<DynamicTypeDescriptorColumns, DynamicTypeDescriptor>
    { 
		public DynamicTypeDescriptorCollection(){}
		public DynamicTypeDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DynamicTypeDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DynamicTypeDescriptorCollection(Query<DynamicTypeDescriptorColumns, DynamicTypeDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DynamicTypeDescriptorCollection(Database db, Query<DynamicTypeDescriptorColumns, DynamicTypeDescriptor> q, bool load) : base(db, q, load) { }
		public DynamicTypeDescriptorCollection(Query<DynamicTypeDescriptorColumns, DynamicTypeDescriptor> q, bool load) : base(q, load) { }
    }
}