/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.ServiceRegistry.Data.Dao
{
    public class ServiceRegistryLoaderDescriptorPagedQuery: PagedQuery<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor>
    { 
		public ServiceRegistryLoaderDescriptorPagedQuery(ServiceRegistryLoaderDescriptorColumns orderByColumn, ServiceRegistryLoaderDescriptorQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}