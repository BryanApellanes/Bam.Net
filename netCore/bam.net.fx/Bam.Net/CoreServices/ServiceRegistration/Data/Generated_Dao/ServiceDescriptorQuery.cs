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
    public class ServiceDescriptorQuery: Query<ServiceDescriptorColumns, ServiceDescriptor>
    { 
		public ServiceDescriptorQuery(){}
		public ServiceDescriptorQuery(WhereDelegate<ServiceDescriptorColumns> where, OrderBy<ServiceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceDescriptorQuery(Func<ServiceDescriptorColumns, QueryFilter<ServiceDescriptorColumns>> where, OrderBy<ServiceDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceDescriptorQuery Where(WhereDelegate<ServiceDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceDescriptorQuery Where(WhereDelegate<ServiceDescriptorColumns> where, OrderBy<ServiceDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ServiceDescriptorQuery(where, orderBy, db);
        }

		public ServiceDescriptorCollection Execute()
		{
			return new ServiceDescriptorCollection(this, true);
		}
    }
}