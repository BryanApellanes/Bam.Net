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
    public class ServiceDescriptorServiceRegistryDescriptorQuery: Query<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor>
    { 
		public ServiceDescriptorServiceRegistryDescriptorQuery(){}
		public ServiceDescriptorServiceRegistryDescriptorQuery(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceDescriptorServiceRegistryDescriptorQuery(Func<ServiceDescriptorServiceRegistryDescriptorColumns, QueryFilter<ServiceDescriptorServiceRegistryDescriptorColumns>> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceDescriptorServiceRegistryDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceDescriptorServiceRegistryDescriptorQuery Where(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceDescriptorServiceRegistryDescriptorQuery Where(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ServiceDescriptorServiceRegistryDescriptorQuery(where, orderBy, db);
        }

		public ServiceDescriptorServiceRegistryDescriptorCollection Execute()
		{
			return new ServiceDescriptorServiceRegistryDescriptorCollection(this, true);
		}
    }
}