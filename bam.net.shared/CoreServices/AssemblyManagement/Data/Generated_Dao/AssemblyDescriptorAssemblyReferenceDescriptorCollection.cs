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
    public class AssemblyDescriptorAssemblyReferenceDescriptorCollection: DaoCollection<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>
    { 
		public AssemblyDescriptorAssemblyReferenceDescriptorCollection(){}
		public AssemblyDescriptorAssemblyReferenceDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AssemblyDescriptorAssemblyReferenceDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AssemblyDescriptorAssemblyReferenceDescriptorCollection(Query<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AssemblyDescriptorAssemblyReferenceDescriptorCollection(Database db, Query<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor> q, bool load) : base(db, q, load) { }
		public AssemblyDescriptorAssemblyReferenceDescriptorCollection(Query<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor> q, bool load) : base(q, load) { }
    }
}