/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.AssemblyManagement.Data.Dao
{
    public class AssemblyReferenceDescriptorAssemblyDescriptorCollection: DaoCollection<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor>
    { 
		public AssemblyReferenceDescriptorAssemblyDescriptorCollection(){}
		public AssemblyReferenceDescriptorAssemblyDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AssemblyReferenceDescriptorAssemblyDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AssemblyReferenceDescriptorAssemblyDescriptorCollection(Query<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AssemblyReferenceDescriptorAssemblyDescriptorCollection(Database db, Query<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor> q, bool load) : base(db, q, load) { }
		public AssemblyReferenceDescriptorAssemblyDescriptorCollection(Query<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor> q, bool load) : base(q, load) { }
    }
}