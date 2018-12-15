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
    public class AssemblyReferenceDescriptorCollection: DaoCollection<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor>
    { 
		public AssemblyReferenceDescriptorCollection(){}
		public AssemblyReferenceDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AssemblyReferenceDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AssemblyReferenceDescriptorCollection(Query<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AssemblyReferenceDescriptorCollection(Database db, Query<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor> q, bool load) : base(db, q, load) { }
		public AssemblyReferenceDescriptorCollection(Query<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor> q, bool load) : base(q, load) { }
    }
}