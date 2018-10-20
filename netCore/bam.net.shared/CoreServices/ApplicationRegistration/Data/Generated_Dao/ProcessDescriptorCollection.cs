/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ProcessDescriptorCollection: DaoCollection<ProcessDescriptorColumns, ProcessDescriptor>
    { 
		public ProcessDescriptorCollection(){}
		public ProcessDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ProcessDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ProcessDescriptorCollection(Query<ProcessDescriptorColumns, ProcessDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ProcessDescriptorCollection(Database db, Query<ProcessDescriptorColumns, ProcessDescriptor> q, bool load) : base(db, q, load) { }
		public ProcessDescriptorCollection(Query<ProcessDescriptorColumns, ProcessDescriptor> q, bool load) : base(q, load) { }
    }
}