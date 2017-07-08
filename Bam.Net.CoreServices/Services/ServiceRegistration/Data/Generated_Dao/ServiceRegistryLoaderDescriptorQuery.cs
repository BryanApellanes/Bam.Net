/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceRegistryLoaderDescriptorQuery: Query<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor>
    { 
		public ServiceRegistryLoaderDescriptorQuery(){}
		public ServiceRegistryLoaderDescriptorQuery(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceRegistryLoaderDescriptorQuery(Func<ServiceRegistryLoaderDescriptorColumns, QueryFilter<ServiceRegistryLoaderDescriptorColumns>> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceRegistryLoaderDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceRegistryLoaderDescriptorQuery Where(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceRegistryLoaderDescriptorQuery Where(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ServiceRegistryLoaderDescriptorQuery(where, orderBy, db);
        }

		public ServiceRegistryLoaderDescriptorCollection Execute()
		{
			return new ServiceRegistryLoaderDescriptorCollection(this, true);
		}
    }
}