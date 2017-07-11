/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyDescriptorCollection: DaoCollection<AssemblyDescriptorColumns, AssemblyDescriptor>
    { 
		public AssemblyDescriptorCollection(){}
		public AssemblyDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AssemblyDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AssemblyDescriptorCollection(Query<AssemblyDescriptorColumns, AssemblyDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AssemblyDescriptorCollection(Database db, Query<AssemblyDescriptorColumns, AssemblyDescriptor> q, bool load) : base(db, q, load) { }
		public AssemblyDescriptorCollection(Query<AssemblyDescriptorColumns, AssemblyDescriptor> q, bool load) : base(q, load) { }
    }
}