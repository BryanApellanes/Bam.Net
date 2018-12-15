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
    public class ServiceRegistryLockCollection: DaoCollection<ServiceRegistryLockColumns, ServiceRegistryLock>
    { 
		public ServiceRegistryLockCollection(){}
		public ServiceRegistryLockCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ServiceRegistryLockCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ServiceRegistryLockCollection(Query<ServiceRegistryLockColumns, ServiceRegistryLock> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ServiceRegistryLockCollection(Database db, Query<ServiceRegistryLockColumns, ServiceRegistryLock> q, bool load) : base(db, q, load) { }
		public ServiceRegistryLockCollection(Query<ServiceRegistryLockColumns, ServiceRegistryLock> q, bool load) : base(q, load) { }
    }
}