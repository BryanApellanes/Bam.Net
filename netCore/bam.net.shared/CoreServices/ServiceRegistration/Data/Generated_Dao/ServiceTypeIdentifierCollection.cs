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
    public class ServiceTypeIdentifierCollection: DaoCollection<ServiceTypeIdentifierColumns, ServiceTypeIdentifier>
    { 
		public ServiceTypeIdentifierCollection(){}
		public ServiceTypeIdentifierCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ServiceTypeIdentifierCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ServiceTypeIdentifierCollection(Query<ServiceTypeIdentifierColumns, ServiceTypeIdentifier> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ServiceTypeIdentifierCollection(Database db, Query<ServiceTypeIdentifierColumns, ServiceTypeIdentifier> q, bool load) : base(db, q, load) { }
		public ServiceTypeIdentifierCollection(Query<ServiceTypeIdentifierColumns, ServiceTypeIdentifier> q, bool load) : base(q, load) { }
    }
}