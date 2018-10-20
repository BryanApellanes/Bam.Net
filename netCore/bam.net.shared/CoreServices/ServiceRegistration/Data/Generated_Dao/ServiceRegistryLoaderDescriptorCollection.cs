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
    public class ServiceRegistryLoaderDescriptorCollection: DaoCollection<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor>
    { 
		public ServiceRegistryLoaderDescriptorCollection(){}
		public ServiceRegistryLoaderDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ServiceRegistryLoaderDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ServiceRegistryLoaderDescriptorCollection(Query<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ServiceRegistryLoaderDescriptorCollection(Database db, Query<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor> q, bool load) : base(db, q, load) { }
		public ServiceRegistryLoaderDescriptorCollection(Query<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor> q, bool load) : base(q, load) { }
    }
}