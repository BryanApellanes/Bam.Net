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
    public class AssemblyDescriptorProcessRuntimeDescriptorCollection: DaoCollection<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>
    { 
		public AssemblyDescriptorProcessRuntimeDescriptorCollection(){}
		public AssemblyDescriptorProcessRuntimeDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AssemblyDescriptorProcessRuntimeDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AssemblyDescriptorProcessRuntimeDescriptorCollection(Query<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AssemblyDescriptorProcessRuntimeDescriptorCollection(Database db, Query<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor> q, bool load) : base(db, q, load) { }
		public AssemblyDescriptorProcessRuntimeDescriptorCollection(Query<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor> q, bool load) : base(q, load) { }
    }
}