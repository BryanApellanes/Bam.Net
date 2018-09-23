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
    public class DynamicTypePropertyDescriptorCollection: DaoCollection<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor>
    { 
		public DynamicTypePropertyDescriptorCollection(){}
		public DynamicTypePropertyDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DynamicTypePropertyDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DynamicTypePropertyDescriptorCollection(Query<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DynamicTypePropertyDescriptorCollection(Database db, Query<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor> q, bool load) : base(db, q, load) { }
		public DynamicTypePropertyDescriptorCollection(Query<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor> q, bool load) : base(q, load) { }
    }
}