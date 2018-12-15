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
    public class ServiceRegistryDescriptorQuery: Query<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor>
    { 
		public ServiceRegistryDescriptorQuery(){}
		public ServiceRegistryDescriptorQuery(WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceRegistryDescriptorQuery(Func<ServiceRegistryDescriptorColumns, QueryFilter<ServiceRegistryDescriptorColumns>> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceRegistryDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceRegistryDescriptorQuery Where(WhereDelegate<ServiceRegistryDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceRegistryDescriptorQuery Where(WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ServiceRegistryDescriptorQuery(where, orderBy, db);
        }

		public ServiceRegistryDescriptorCollection Execute()
		{
			return new ServiceRegistryDescriptorCollection(this, true);
		}
    }
}