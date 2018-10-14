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
    public class ServiceRegistryDescriptorCollection: DaoCollection<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor>
    { 
		public ServiceRegistryDescriptorCollection(){}
		public ServiceRegistryDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ServiceRegistryDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ServiceRegistryDescriptorCollection(Query<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ServiceRegistryDescriptorCollection(Database db, Query<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor> q, bool load) : base(db, q, load) { }
		public ServiceRegistryDescriptorCollection(Query<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor> q, bool load) : base(q, load) { }
    }
}