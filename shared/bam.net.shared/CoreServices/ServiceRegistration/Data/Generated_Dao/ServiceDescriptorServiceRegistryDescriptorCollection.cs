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
    public class ServiceDescriptorServiceRegistryDescriptorCollection: DaoCollection<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor>
    { 
		public ServiceDescriptorServiceRegistryDescriptorCollection(){}
		public ServiceDescriptorServiceRegistryDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ServiceDescriptorServiceRegistryDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ServiceDescriptorServiceRegistryDescriptorCollection(Query<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ServiceDescriptorServiceRegistryDescriptorCollection(Database db, Query<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor> q, bool load) : base(db, q, load) { }
		public ServiceDescriptorServiceRegistryDescriptorCollection(Query<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor> q, bool load) : base(q, load) { }
    }
}