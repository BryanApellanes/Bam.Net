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
    public class ProcessRuntimeDescriptorCollection: DaoCollection<ProcessRuntimeDescriptorColumns, ProcessRuntimeDescriptor>
    { 
		public ProcessRuntimeDescriptorCollection(){}
		public ProcessRuntimeDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ProcessRuntimeDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ProcessRuntimeDescriptorCollection(Query<ProcessRuntimeDescriptorColumns, ProcessRuntimeDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ProcessRuntimeDescriptorCollection(Database db, Query<ProcessRuntimeDescriptorColumns, ProcessRuntimeDescriptor> q, bool load) : base(db, q, load) { }
		public ProcessRuntimeDescriptorCollection(Query<ProcessRuntimeDescriptorColumns, ProcessRuntimeDescriptor> q, bool load) : base(q, load) { }
    }
}