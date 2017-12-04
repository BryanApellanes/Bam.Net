/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class HostDomainApplicationCollection: DaoCollection<HostDomainApplicationColumns, HostDomainApplication>
    { 
		public HostDomainApplicationCollection(){}
		public HostDomainApplicationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HostDomainApplicationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HostDomainApplicationCollection(Query<HostDomainApplicationColumns, HostDomainApplication> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HostDomainApplicationCollection(Database db, Query<HostDomainApplicationColumns, HostDomainApplication> q, bool load) : base(db, q, load) { }
		public HostDomainApplicationCollection(Query<HostDomainApplicationColumns, HostDomainApplication> q, bool load) : base(q, load) { }
    }
}