/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ProcessDescriptorCollection: DaoCollection<ProcessDescriptorColumns, ProcessDescriptor>
    { 
		public ProcessDescriptorCollection(){}
		public ProcessDescriptorCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ProcessDescriptorCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ProcessDescriptorCollection(Query<ProcessDescriptorColumns, ProcessDescriptor> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ProcessDescriptorCollection(Database db, Query<ProcessDescriptorColumns, ProcessDescriptor> q, bool load) : base(db, q, load) { }
		public ProcessDescriptorCollection(Query<ProcessDescriptorColumns, ProcessDescriptor> q, bool load) : base(q, load) { }
    }
}