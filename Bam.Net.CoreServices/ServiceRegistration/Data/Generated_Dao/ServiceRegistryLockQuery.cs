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
    public class ServiceRegistryLockQuery: Query<ServiceRegistryLockColumns, ServiceRegistryLock>
    { 
		public ServiceRegistryLockQuery(){}
		public ServiceRegistryLockQuery(WhereDelegate<ServiceRegistryLockColumns> where, OrderBy<ServiceRegistryLockColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceRegistryLockQuery(Func<ServiceRegistryLockColumns, QueryFilter<ServiceRegistryLockColumns>> where, OrderBy<ServiceRegistryLockColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceRegistryLockQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceRegistryLockQuery Where(WhereDelegate<ServiceRegistryLockColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceRegistryLockQuery Where(WhereDelegate<ServiceRegistryLockColumns> where, OrderBy<ServiceRegistryLockColumns> orderBy = null, Database db = null)
        {
            return new ServiceRegistryLockQuery(where, orderBy, db);
        }

		public ServiceRegistryLockCollection Execute()
		{
			return new ServiceRegistryLockCollection(this, true);
		}
    }
}