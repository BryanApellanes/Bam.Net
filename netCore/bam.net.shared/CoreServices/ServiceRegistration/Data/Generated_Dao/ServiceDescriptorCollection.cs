/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceDescriptorCollection: DaoCollection<ServiceDescriptorColumns, ServiceDescriptor>
    { 
		public ServiceDescriptorCollection(){}
		public ServiceDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ServiceDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ServiceDescriptorCollection(Query<ServiceDescriptorColumns, ServiceDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ServiceDescriptorCollection(Database db, Query<ServiceDescriptorColumns, ServiceDescriptor> q, bool load) : base(db, q, load) { }
		public ServiceDescriptorCollection(Query<ServiceDescriptorColumns, ServiceDescriptor> q, bool load) : base(q, load) { }
    }
}